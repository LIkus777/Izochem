using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileAnimator : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 5f;
    public float animationDuration = 1f;
    public float minScale = 0.1f;
    public float verticalOffset = 1f;

    public Tilemap[] animatedTilemaps;

    private Dictionary<Tilemap, HashSet<Vector3Int>> animatedTilesByTilemap = new Dictionary<Tilemap, HashSet<Vector3Int>>();
    private Dictionary<Tilemap, HashSet<Vector3Int>> allTilesByTilemap = new Dictionary<Tilemap, HashSet<Vector3Int>>();
    private Dictionary<Tilemap, TilemapRenderer> tilemapRenderers = new Dictionary<Tilemap, TilemapRenderer>();
    private Dictionary<Tilemap, GameObject> tilemapChilds = new Dictionary<Tilemap, GameObject>();

    private void Start()
    {
        CreateTilemaps();
    }

    private void Update()
    {
        AnimateTiles();
    }

    private void CreateTilemaps()
    {
        foreach (var tilemap in animatedTilemaps)
        {
            HashSet<Vector3Int> animatedTiles = new HashSet<Vector3Int>();
            HashSet<Vector3Int> allTiles = new HashSet<Vector3Int>();

            tilemapRenderers[tilemap] = tilemap.GetComponent<TilemapRenderer>();
            tilemapRenderers[tilemap].enabled = false;

            GameObject newTilemapObj = new GameObject("TilemapChild");
            Tilemap newTilemap = newTilemapObj.AddComponent<Tilemap>();
            TilemapRenderer newTilemapRenderer = newTilemapObj.AddComponent<TilemapRenderer>();
            newTilemapRenderer.enabled = true;
            newTilemapRenderer.sortingOrder = tilemapRenderers[tilemap].sortingOrder;
            newTilemapRenderer.sortOrder = tilemapRenderers[tilemap].sortOrder;

            newTilemapObj.transform.SetParent(tilemap.transform);
            newTilemapObj.transform.position = tilemap.transform.position;

            BoundsInt bounds = tilemap.cellBounds;

            foreach (var position in bounds.allPositionsWithin)
            {
                TileBase tile = tilemap.GetTile(position);

                if (tile != null)
                {
                    allTiles.Add(position);
                }
            }

            animatedTilesByTilemap[tilemap] = animatedTiles;
            allTilesByTilemap[tilemap] = allTiles;
            tilemapChilds[tilemap] = newTilemapObj;
        }
    }

    private void AnimateTiles()
    {
        foreach (var tilemap in animatedTilemaps)
        {
            HashSet<Vector3Int> animatedTiles = animatedTilesByTilemap[tilemap];
            HashSet<Vector3Int> allTiles = allTilesByTilemap[tilemap];

            foreach (var tilePos in allTiles)
            {
                Vector3 worldPosition = tilemap.CellToWorld(tilePos);
                float distance = Vector3.Distance(player.position, worldPosition);

                if (distance <= triggerDistance)
                {
                    if (!animatedTiles.Contains(tilePos))
                    {
                        StartCoroutine(AnimateTile(worldPosition, tilePos, tilemap, true));
                        animatedTiles.Add(tilePos);
                    }
                }
                else
                {
                    if (animatedTiles.Contains(tilePos))
                    {
                        StartCoroutine(AnimateTile(worldPosition, tilePos, tilemap, false));
                        animatedTiles.Remove(tilePos);
                    }
                }
            }
        }
    }

    private IEnumerator AnimateTile(Vector3 worldPosition, Vector3Int cellPosition, Tilemap parentTilemap, bool appearing)
    {
        GameObject childTilemapTransform = tilemapChilds[parentTilemap];

        Tilemap childTilemap = childTilemapTransform.GetComponent<Tilemap>();
        TilemapRenderer childRenderer = childTilemap.GetComponent<TilemapRenderer>();

        TileBase tile = parentTilemap.GetTile(cellPosition);
        if (appearing)
        {
            childTilemap.SetTile(cellPosition, tile);
        }

        Vector3 startPosition = worldPosition + new Vector3(0, appearing ? -verticalOffset : 0, 0);
        Vector3 endPosition = worldPosition + new Vector3(0, appearing ? 0 : -verticalOffset, 0);
        float timeElapsed = 0;
        Vector3 startScale = appearing ? new Vector3(minScale, minScale, 1) : Vector3.one;
        Vector3 endScale = appearing ? Vector3.one : new Vector3(minScale, minScale, 1);

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            Vector3 currentPosition = Vector3.Lerp(startPosition, endPosition, t);
            Vector3 currentScale = Vector3.Lerp(startScale, endScale, t);

            childTilemap.SetTransformMatrix(cellPosition, Matrix4x4.TRS(currentPosition - childTilemap.CellToWorld(cellPosition), Quaternion.identity, currentScale));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (!appearing)
        {
            if (Vector3.Distance(player.position, worldPosition) > triggerDistance)
            {
                childTilemap.SetTile(cellPosition, null);
            }
        }

        childTilemap.SetTransformMatrix(cellPosition, Matrix4x4.identity);
    }
}
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject player;
    public PlayerAnimation playerAnimation;


    private void Update()
    {
        if (!playerAnimation.isMirror)
            transform.position = new Vector3(player.transform.position.x + 4f, player.transform.position.y - 1f,
                transform.position.z);
        else
            transform.position = new Vector3(player.transform.position.x - 4f, player.transform.position.y - 1f,
                transform.position.z);
    }
}
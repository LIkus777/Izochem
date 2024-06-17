using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Titles : MonoBehaviour
{
    public GameObject key;
    public VideoPlayer vp;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null && other.CompareTag("Finish")  && key == null)
        {
            StartCoroutine(PlayVideo());
        }
    }

    private IEnumerator PlayVideo()
    {
        vp.Play();
        yield return new WaitForSeconds(54.0f);
        SceneManager.LoadScene(0);
    }
}

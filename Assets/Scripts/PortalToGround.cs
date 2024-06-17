using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToGround : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null && other.CompareTag("Player")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
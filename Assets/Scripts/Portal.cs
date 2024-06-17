using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public GameObject key;
    public static bool tpActive;
    [SerializeField] private Portal toPortal;
    
    private void Start()
    {
        tpActive = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (tpActive && rigidbody2D != null && key == null)
        {
            tpActive = false;
            float magnitude = rigidbody2D.velocity.magnitude;
            rigidbody2D.velocity = Vector3.zero;
            Vector3 direction = toPortal.transform.TransformDirection(Vector3.right) -
                                transform.TransformDirection(Vector3.left);
            other.transform.position = toPortal.transform.position;
            rigidbody2D.AddForce(direction * magnitude, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        else tpActive = true;
    }
}
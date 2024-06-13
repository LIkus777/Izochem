using System;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private Portal toPortal;

    [SerializeField] private GameObject tpEffect;
    // Start is called before the first frame update

    public static bool tpActive;
    private void Start()
    {
        tpActive = true;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (tpActive && rigidbody2D != null)
        {
            tpActive = false;
            float magnitude = rigidbody2D.velocity.magnitude;
            rigidbody2D.velocity = Vector3.zero;
            Vector3 direction = toPortal.transform.TransformDirection(Vector3.right) -
                                transform.TransformDirection(Vector3.left);
            other.transform.position = toPortal.transform.position;
            rigidbody2D.AddForce(direction * magnitude, ForceMode2D.Impulse);
        }
        else tpActive = true;
    }
}
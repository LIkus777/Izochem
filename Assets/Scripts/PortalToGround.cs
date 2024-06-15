using System;
using UnityEngine;

public class PortalToGround : MonoBehaviour
{
    [SerializeField] private GameObject tpEffect;
    // Start is called before the first frame update

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact");
        Rigidbody2D rigidbody2D = other.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            float magnitude = rigidbody2D.velocity.magnitude;
            rigidbody2D.velocity = Vector3.zero;
            //Vector3 position = rigidbody2D.position;
            Vector3 zeroPosition = new Vector3(0f, 0f);
            other.transform.position = zeroPosition;
            rigidbody2D.AddForce(zeroPosition * magnitude, ForceMode2D.Impulse);
        }
    }
}
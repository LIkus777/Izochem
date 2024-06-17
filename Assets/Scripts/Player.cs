using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool facingRight = true;
    private Animator anim;
    private float moveInput;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        // moveInput = Input.GetAxis()
    }
}
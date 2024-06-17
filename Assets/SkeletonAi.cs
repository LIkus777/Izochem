using System.Collections;
using UnityEngine;

public class SkeletonAi : MonoBehaviour
{
    public float speed = 2f;
    public float maxHealth = 90f;
    public float currentHealth;
    public float attackRange = 5f;
    public float attackDamage = 20f;

    public GameObject Key;

    public GameObject bullet;

    public bool Dying;

    public GameObject skeletonSprite;

    public Animator anim;
    private Rigidbody2D rb;

    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        Dying = false;
    }

    private void Update()
    {
        if (Dying) speed = 0.0000000001f;
        Move();
        CheckHealth();
    }


    private void OnCollisionEnter2D(Collision2D other)


    {
        if (other.gameObject.CompareTag("Player")) StartCoroutine(PlayAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("stone"))
        {
            TakeDamage(30);
            StartCoroutine(PlayDamage());
        }
    }

    private IEnumerator PlayAttack()
    {
        anim.SetTrigger("Attak");
        yield return new WaitForSeconds(0.4f);
        anim.ResetTrigger("Attak");
    }

    private IEnumerator PlayDamage()
    {
        anim.SetTrigger("Damage");
        yield return new WaitForSeconds(0.4f);
        anim.ResetTrigger("Damage");
    }

    private void Move()
    {
        var direction = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        if (direction.x > 0)
            skeletonSprite.transform.localScale = new Vector3(-6f, 6f, 1f);
        else
            skeletonSprite.transform.localScale = new Vector3(6f, 6f, 1f);

        if (direction.magnitude < 0.2f) StartCoroutine(PlayAttack());
    }

    private void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Key.SetActive(true);
            Key.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        Dying = true;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
}
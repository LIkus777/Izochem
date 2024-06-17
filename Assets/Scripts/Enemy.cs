using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;

    public float speed;

    public GameObject deathEffect;

    public int damage;

    public float startStopTime;

    public float normalSpeed;

    private Animator anim;

    private Player player;

    private float startTimeBtwAttack;

    private float stopTime;

    private float timeBtwAttack;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0) Destroy(gameObject);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
    }
}
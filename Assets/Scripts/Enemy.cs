using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;

    private float startTimeBtwAttack;

    public int health;

    public float speed;

    public GameObject deathEffect;

    public int damage;

    private float stopTime;

    public float startStopTime;

    public float normalSpeed;

    private Player player;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        
    }
    
}

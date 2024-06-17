using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float attackCooldown = 2f;
    private bool isAttacking = false;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public Animator anim;

    public GameObject bullet;


    public PlayerAnimation playerAnimation;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!playerAnimation.IsAttaking && !playerAnimation.IsThrowing)
            {
                playerAnimation.IsThrowing = true;
                StartCoroutine(playerAnimationViebon());
            }
            else
            {
                if (!playerAnimation.IsThrowing)
                {
                    playerAnimation.IsThrowing = true;
                    if (!playerAnimation.isMirror)
                    {
                    transform.position = new Vector3(transform.position.x + 1.699978f, transform.position.y, transform.position.z);
                    StartCoroutine(PlayAttackAnimation(playerAnimation.isMirror));
                    }
                    else
                    {
                    transform.position = new Vector3(transform.position.x - 1.699978f, transform.position.y, transform.position.z);
                    StartCoroutine(PlayAttackAnimation(playerAnimation.isMirror));
                    }
                }
      
                
            }
        }
        if (Input.GetMouseButton(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }
    private IEnumerator playerAnimationViebon()
    {
        playerAnimation.PlayStoneAnimation(0);
        yield return new WaitForSeconds(2f);
        playerAnimation.IsThrowing = false;
        
    }
    private IEnumerator PlayAttackAnimation(bool isMirror)
    {
        playerAnimation.PlayStoneAnimation(1);
        bullet.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        if(!isMirror)
        {
            transform.position = new Vector3(transform.position.x - 1.699978f, transform.position.y, transform.position.z);
        }
        else{
            transform.position = new Vector3(transform.position.x + 1.699978f, transform.position.y, transform.position.z);
        }
        
        playerAnimation.IsAttaking = false;
        bullet.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
        playerAnimation.IsThrowing = false;
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        anim.SetTrigger("attack");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        if (enemies.Length > 0)
        {
            foreach (Collider2D e in enemies)
            {
                e.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}

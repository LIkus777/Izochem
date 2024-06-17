using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float jumpSpeed = 1.3f;
    [SerializeField] private float jumpDistance = 2.0f; // Новое поле для настройки дальности прыжка
    [SerializeField] AnimationCurve curveY;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 currentPos;
    private Vector2 landingPos;
    private float totalJumpTime; // Время, необходимое для полного прыжка
    float timeElapsed = 0f;
    bool onGround = true;
    bool jump = false;

    bool isStop = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        InputHandler();
    }

    private void FixedUpdate()
    {
        if (jump) {
            JumpHandler();
        } else {
            MovementHandler();
        }
    }

    void JumpHandler() {
        if (onGround) {
            currentPos = rb.position;
            landingPos = currentPos + movement.normalized * jumpDistance; // Используем jumpDistance для вычисления точки приземления
            totalJumpTime = curveY[curveY.length - 1].time / jumpSpeed; // Время прыжка основано на кривой и скорости прыжка
            timeElapsed = 0f;
            onGround = false;
        } else {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed <= totalJumpTime) {
                float normalizedTime = timeElapsed / totalJumpTime;
                currentPos = Vector2.MoveTowards(currentPos, landingPos, Time.fixedDeltaTime * jumpDistance / totalJumpTime);
                rb.MovePosition(new Vector2(currentPos.x, currentPos.y + curveY.Evaluate(normalizedTime) * jumpDistance));
            } else {
                jump = false;
                onGround = true;
            }
        }
    }

    void MovementHandler() {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        Vector2 direction = movement;
        FindObjectOfType<PlayerAnimation>().SetDirection(direction);
    }

    void InputHandler() {
        if (!isStop) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            movement = new Vector2(horizontal, vertical);

            if (Input.GetKeyDown("space")) {
                jump = true;
            }
        }
    }

    public void SetStop(bool stop) {
        isStop = stop;
        movement = new Vector2(0, 0);
    }
}
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public int JumpCount = 2;
    // 점프 높이
    public float jumpHeight = 4;
    // 최고 높이에 도달하는 시간
    public float timeToJumpApex = 0.4f;

    public float DashVelocity = 15f;

    public float DashTime = 0.1f;

    // 공중에서의 가속도 시간
    float accelerationTimeAirborne = 0.2f;
    // 지면에서의 가속도 시간
    float accelerationTimeGrounded = 0.1f;
    // 이동 속도
    float moveSpeed = 6;

    int jumpCount;

    // 실제 적용 받게될 중력
    float gravity;
    // 실제 점프 시 적용되는 속도
    float jumpVelocity;
    // 움직이는 속도
    Vector3 velocity;

    Vector2 dashDirection;
    float dashTimer;

    float velocityXSmoothing;

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        jumpCount = JumpCount;
        dashDirection = Vector2.zero;
        dashTimer = 0f;
        // 움직임 = 초기속도 * 시간 + 가속도 * 시간^2 * 1/2
        // 위 식을 자체변수로 바꾸면
        // 점프높이(jumpHeight) = 중력(gravity) * 걸린시간(timeToJumpApex)^2 * 1/2
        // 중력을 제외한 오른쪽 값들을 좌측으로 이항하면
        // 중력 = (2 * 점프 높이) / (최고 높이에 도달하는 시간의 제곱)
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        // 점프 속도 = 중력 * 최고 높이에 도달하는 시간
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        //print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = 1f;
        }

        if (controller.collisions.above)
            velocity.y = 0;

        if (StayFloor())
        {
            velocity.y = 0;
            jumpCount = 2;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 스페이스바 입력, 바닥에 닿아있을때 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.S))
            {
                controller.DownJump();
            }
            else if (jumpCount > 0)
            {
                velocity.y = jumpVelocity;
                jumpCount--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dashTimer = DashTime;
        }

        float targetVelocityX = input.x * moveSpeed;
        // 부드러운 감속과 가속을 위한 SmoothDamp 함수
        // Mathf.SmoothDamp(현재값, 목표값, ref 속도, 시간)
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            velocity = dashDirection.normalized * DashVelocity;
            controller.Dash(velocity * Time.deltaTime);
        }
        else
            controller.Move(velocity * Time.deltaTime);
    }

    bool StayFloor()
    {
        return controller.collisions.below || controller.collisions.descendingSlope || controller.collisions.climbingSlope;
    }
}
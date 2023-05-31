using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : Character
{
    // 대쉬 속도
    public float DashVelocity = 15f;
    // 대쉬 시간
    public float DashTime = 0.1f;

    #region private field
    // 공중에서의 가속도 시간
    private float accelerationTimeAirborne = 0.2f;
    // 지면에서의 가속도 시간
    private float accelerationTimeGrounded = 0.1f;
    // 이동 속도
    private float moveSpeed = 6;
    // 실제 점프 횟수를 카운트하는 변수
    private int jumpCount;
    // 실제 적용 받게될 중력
    private float gravity;
    // 실제 점프 시 적용되는 속도
    private float jumpVelocity;
    // 움직이는 속도
    private Vector3 velocity;
    // 마우스 위치를 통해 대쉬 방향을 받을 벡터
    private Vector2 dashDirection;
    // 대쉬 시간을 체크하는 타이머
    private float dashTimer;
    // Mathf.Smooth 함수에서 연산용으로 쓰이는 변수
    private float velocityXSmoothing;
    // 컨트롤러
    private Controller2D controller;
    #endregion

    void Start()
    {
        controller = GetComponent<Controller2D>();
        PhysicsInit();
    }

    void Update()
    {
        VerticalCollision();
        Jump();
        VelocityInput();
        InputGroup();
        Movement();
    }

    private void PhysicsInit()
    {
        jumpCount = Status.JumpCount;
        dashDirection = Vector2.zero;
        dashTimer = 0f;
        // 움직임 = 초기속도 * 시간 + 가속도 * 시간^2 * 1/2
        // 위 식을 자체변수로 바꾸면
        // 점프높이(jumpHeight) = 중력(gravity) * 걸린시간(timeToJumpApex)^2 * 1/2
        // 중력을 제외한 오른쪽 값들을 좌측으로 이항하면
        // 중력 = (2 * 점프 높이) / (최고 높이에 도달하는 시간의 제곱)
        gravity = -(2 * Status.JumpHeight) / Mathf.Pow(Status.TimeToJumpApex, 2);

        // 점프 속도 = 중력 * 최고 높이에 도달하는 시간
        jumpVelocity = Mathf.Abs(gravity) * Status.TimeToJumpApex;
    }

    private void VerticalCollision()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.below)
                jumpCount = 2;
            velocity.y = 0;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 하단 점프
            if (Input.GetKey(KeyCode.S))
            {
                controller.DownJump();
            }
            // 일반 점프
            else if (jumpCount > 0)
            {
                velocity.y = jumpVelocity;
                jumpCount--;
            }
        }
    }

    private void VelocityInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        float targetVelocityX = input.x * moveSpeed;
        // 부드러운 감속과 가속을 위한 SmoothDamp 함수
        // Mathf.SmoothDamp(현재값, 목표값, ref 속도, 시간)
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    private void InputGroup()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dashTimer = DashTime;
        }
    }

    private void Movement()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            velocity = dashDirection.normalized * DashVelocity;
            controller.Dash(velocity * Time.deltaTime);
        }
        else
            controller.Move(velocity * Time.deltaTime);
    }
}
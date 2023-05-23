using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    // 점프 높이
    public float jumpHeight = 4;
    // 최고 높이에 도달하는 시간
    public float timeToJumpApex = 0.4f;
    // 공중에서의 가속도 시간
    float accelerationTimeAirborne = 0.2f;
    // 지면에서의 가속도 시간
    float accelerationTimeGrounded = 0.1f;
    // 이동 속도
    float moveSpeed = 6;

    // 실제 적용 받게될 중력
    float gravity;
    // 실제 점프 시 적용되는 속도
    float jumpVelocity;
    // 움직이는 속도
    Vector3 velocity;

    float velocityXSmoothing;

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        // 움직임 = 초기속도 * 시간 + 가속도 * 시간^2 * 1/2
        // 위 식을 자체변수로 바꾸면
        // 점프높이(jumpHeight) = 중력(gravity) * 걸린시간(timeToJumpApex)^2 * 1/2
        // 중력을 제외한 오른쪽 값들을 좌측으로 이항하면
        // 중력 = (2 * 점프 높이) / (최고 높이에 도달하는 시간의 제곱)
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);

        // 점프 속도 = 중력 * 최고 높이에 도달하는 시간
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        // 위쪽, 아레쪽 충돌시 속도를 0으로 초기화
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // 스페이스바 입력, 바닥에 닿아있을때 점프
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        // 부드러운 감속과 가속을 위한 SmoothDamp 함수
        // Mathf.SmoothDamp(현재값, 목표값, ref 속도, 시간)
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
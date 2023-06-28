using UnityEngine;

public enum PlayerState { Idle = 0, Run, Jump, Dash, Die }

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : Character
{
    // 대쉬 속도
    public float DashVelocity = 15f;
    // 대쉬 시간
    public float DashTime = 0.1f;
    // 플레이어의 상태
    public PlayerState State = PlayerState.Idle;

    public SpriteRenderer SpriteRender;


    #region private field
    // 실제 점프 시 적용되는 속도
    public float JumpVelocity;

    private float gravity;
    #endregion

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        PhysicsInit();
    }

    void Update()
    {
        InputGroup();
    }

    private void PhysicsInit()
    {
        // 움직임 = 초기속도 * 시간 + 가속도 * 시간^2 * 1/2
        // 위 식을 자체변수로 바꾸면
        // 점프높이(jumpHeight) = 중력(gravity) * 걸린시간(timeToJumpApex)^2 * 1/2
        // 중력을 제외한 오른쪽 값들을 좌측으로 이항하면
        // 중력 = (2 * 점프 높이) / (최고 높이에 도달하는 시간의 제곱)
        gravity = (2 * Status.JumpHeight) / Mathf.Pow(Status.TimeToJumpApex, 2);
        Rigidbody.gravityScale = gravity * 10f / 98f;
        // 점프 속도 = 중력 * 최고 높이에 도달하는 시간
        JumpVelocity = Mathf.Abs(gravity) * Status.TimeToJumpApex;
    }

    private void InputGroup()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Attack
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Skill 1
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Skill 2
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Skill 3
        }
    }
}
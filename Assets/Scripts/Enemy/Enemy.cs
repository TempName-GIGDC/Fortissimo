using UnityEngine;

public abstract class Enemy : Character
{
    private float gravity;

    public float JumpVelocity;

    public Player player;

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PhysicsInit();
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
}
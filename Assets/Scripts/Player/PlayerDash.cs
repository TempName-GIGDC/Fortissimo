using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Player player;
    private float _dashTimer = 0f;              // 대쉬 지속 시간을 재는 타이머
    private float _dashCooldownTimer = 0f;      // 대쉬 쿨타임을 재는 타이머
    private Vector2 dashVelocity;              // 최종 대쉬 속도값을 받을 벡터
    private Vector2 dashDirection;
    public float DashCooldown;          // 대쉬 쿨타임
    Coroutine g;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Time.timeScale == 0.1f)
            return;

        // 대쉬 쿨타임 감소
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.unscaledDeltaTime;
        }

        // 마우스 우클릭 누르면 대쉬
        if (Input.GetKeyDown(KeyCode.Mouse1) && _dashCooldownTimer <= 0 && player.State != PlayerState.Die)
        {
            DashInit();
        }
    }

    private void FixedUpdate()
    {
        // 대쉬 상태인 경우
        if (player.State == PlayerState.Dash)
        {
            // 대쉬 움직임
            DashMovement();

            // 시간이 다 지나면 Idle 상태로 전환
            _dashTimer -= Time.unscaledDeltaTime;
            if (_dashTimer <= 0)
            {
                player.State = PlayerState.Idle;
            }
        }
    }
    /// <summary>
    /// 대쉬할 때 필요한 변수들을 초기화 해주는 함수입니다.
    /// </summary>
    private void DashInit()
    {
        player.State = PlayerState.Dash;
        _dashTimer = player.DashTime;
        _dashCooldownTimer = DashCooldown;
        dashDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    /// <summary>
    /// 대쉬 움직임이 구현된 함수입니다.
    /// </summary>
    private void DashMovement()
    {
        dashVelocity = dashDirection.normalized * player.DashVelocity;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, player.DashVelocity, 1 << 6);
        if (hit)
        {
            if (g == null)
                g = StartCoroutine(IgnoreCollsion(hit));
        }
        player.Rigidbody.velocity = dashVelocity;
    }

    IEnumerator IgnoreCollsion(RaycastHit2D hit)
    {
        PlatformEffector2D getGround = hit.transform.GetComponent<PlatformEffector2D>();
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        yield return new WaitForSecondsRealtime(0.4f);
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        g = null;
    }
}

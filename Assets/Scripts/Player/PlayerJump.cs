using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Player player;         // 플레이어 정보를 받아오기 위한 객체
    private Collider2D isGrounded;       // 땅에 발을 딛는지 확인하는 용도
    private PlatformEffector2D getGround;
    private int jumpCnt = 2;
    private Vector2 groundCheck;   // 땅을 체크하기 위한 범위의 좌표

    public LayerMask WhatIsGround;  // 땅의 레이어를 받아옴
    public float CheckSizeX;        // 체크박스의 크기를 조절 (가로)
    public float CheckSizeY;        // 체크박스의 크기를 조절 (세로)

    private void Start()
    {
        player = GetComponent<Player>();

    }

    private void Update()
    {
        if (Time.timeScale == 0.1f)
            return;

        // 땅에 발 딛고 있는지 체크
        groundCheck = new Vector2(GetComponent<BoxCollider2D>().bounds.center.x, GetComponent<BoxCollider2D>().bounds.min.y);
        isGrounded = Physics2D.OverlapBox(groundCheck, new Vector2(CheckSizeX, CheckSizeY), 0f, WhatIsGround);

        if (isGrounded)
        {
            jumpCnt = 2;
        }
        // 체크한 값으로 플레이어 상태 전환
        JumpStateSelector();

        // 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 하단 점프
            if (Input.GetKey(KeyCode.S) && player.State != PlayerState.Die)
            {
                // 지면에 있을 시 밑층으로 내려가기
                if (isGrounded && isGrounded.CompareTag("Floor"))
                {
                    jumpCnt--;
                    StartCoroutine(DownJump());
                }
            }
            // 땅에 발 딛고 있으며 경직된 상태가 아닐 때 혹은 대쉬 중 일때 작동
            else if (isGrounded && (player.State != PlayerState.Die || player.State == PlayerState.Dash))
            {
                jumpCnt--;
                player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x, 0);
                player.Rigidbody.velocity = new Vector2(0, player.JumpVelocity);
            }
            else if (player.State == PlayerState.Jump && jumpCnt > 0)
            {
                jumpCnt--;
                player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x, 0);
                player.Rigidbody.velocity = new Vector2(0, player.JumpVelocity);
            }
        }
    }

    /// <summary>
    /// 하단 점프 구현부 입니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator DownJump()
    {
        getGround = isGrounded.GetComponent<PlatformEffector2D>();
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
        yield return new WaitForSecondsRealtime(0.4f);
        getGround.colliderMask = getGround.colliderMask ^ 1 << 3;
    }

    /// <summary>
    /// 플레이어의 점프 상태를 전환하는 함수입니다.
    /// </summary>
    private void JumpStateSelector()
    {
        // 점프 후 땅에 다시 착지할 때
        if (isGrounded && player.State == PlayerState.Jump)
        {
            player.State = PlayerState.Idle;
        }

        // 공중에 있으면서 플레이어가 경직된 상태가 아닐 때
        else if (!isGrounded && player.State != PlayerState.Die && player.State != PlayerState.Dash)
        {
            player.State = PlayerState.Jump;
        }
    }
    private void OnDrawGizmos()
    {
        // 체크 구역 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(groundCheck, new Vector2(CheckSizeX, CheckSizeY));
    }
}

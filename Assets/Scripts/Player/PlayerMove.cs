using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMove : MonoBehaviour
{
    private Vector2 movement;  // 플레이어를 움직이는 벡터
    private float horizontal;  // 입력받은 값의 방향을 정함
    private Player player;     // 플레이어 정보를 받아오기 위한 객체

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.State != PlayerState.Die)
            horizontal = Input.GetAxisRaw("Horizontal");
        else
            horizontal = 0;

        // 플레이어 상태 전환
        MoveStateSelector();
        // 플레이어 방향 전환
        DirectionSelector();

        if (player.State != PlayerState.Dash)
        {
            // 움직임
            Movement();
        }
    }

    /// <summary>
    /// 플레이어의 이동상태 전환을 판단하는 함수입니다.
    /// </summary>
    private void MoveStateSelector()
    {
        if (!Input.anyKey && player.State == PlayerState.Run)
            player.State = PlayerState.Idle;

        else if (horizontal != 0 && player.State == PlayerState.Idle)
        {
            player.State = PlayerState.Run;
        }
    }

    /// <summary>
    /// 플레이어가 보고있는 방향을 판단하는 함수입니다.
    /// </summary>
    private void DirectionSelector()
    {
        if (horizontal > 0)
        {
            player.Direction = CharacterDirection.Right;
            player.SpriteRender.flipX = false;
        }
        else if (horizontal < 0)
        {
            player.Direction = CharacterDirection.Left;
            player.SpriteRender.flipX = true;
        }
    }

    /// <summary>
    /// 움직임을 구현한 함수입니다.
    /// </summary>
    private void Movement()
    {
        movement = new Vector2(horizontal, 0f);
        movement.Normalize();
        player.Rigidbody.velocity = new Vector2(movement.x * player.Status.Speed, player.Rigidbody.velocity.y);
    }
}

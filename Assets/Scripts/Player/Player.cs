using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCostume
{
    None = 0,
    Defualt
}

public class Player : Character
{
    public PlayerCostume Costume;
    protected override void Start()
    {
        base.Start();
        Type = CharacterType.Player;
    }
    // 맞기, 공격, 대쉬, 점프 애니메이션 x
    protected override void Update()
    {
        base.Update();
        Movement();
        Debug.Log(Controller.isGrounded);
    }

    private void Movement()
    {
        Direction = new Vector3(Input.GetAxis("Horizontal") * Status.Speed, Direction.y, 0);
        Controller.Move(Direction * Time.deltaTime);
    }
}

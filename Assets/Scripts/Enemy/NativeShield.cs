using UnityEngine;

public enum ShieldPattern
{
    Idle = 0, Check, Run, NormalAttack, Stun, Dead
}

public class NativeShield : Enemy
{
    public float CheckTime = 5f;
    public float RunTime = 5f;

    private ShieldPattern pattern;

    private float directionX;
    private float directionY;

    private Bounds collisionArea;
    private Bounds VerticalArea;
    private Bounds HorizontalArea;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pattern = ShieldPattern.Idle;

        collisionArea.max = new Vector2(-5f, 2f);
        collisionArea.min = new Vector2(5f, 0f);

        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (pattern)
        {
            case ShieldPattern.Idle:
                Idle();
                break;
            case ShieldPattern.Check:
                Check();
                break;
            case ShieldPattern.NormalAttack:
                NormalAttack();
                break;
            case ShieldPattern.Stun:
                Stun();
                break;
            case ShieldPattern.Dead:
                Dead();
                break;
            default:
                Debug.LogError("NativePunch Pattern Error");
                break;
        }
    }

    private void Idle()
    {
        if (AreaCheck(collisionArea))
        {
            pattern = ShieldPattern.Check;
        }
    }

    private void Check()
    {
        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();

        if (AreaCheck(VerticalArea))
        {
            pattern = ShieldPattern.NormalAttack;
            return;
        }

        Status.Speed = 1f;

        // Walk
    }

    private void NormalAttack()
    {

    }

    private void Stun()
    {

    }

    private void Dead()
    {

    }

    public bool AreaCheck(Bounds area)
    {
        return Physics2D.OverlapArea(transform.position - area.max, transform.position - area.min, 1 << 3);
    }
}

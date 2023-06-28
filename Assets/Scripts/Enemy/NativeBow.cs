using UnityEngine;

public enum BowPattern
{
    Idle = 0, Check, Aiming, NormalAttack, NearAttack, DashAttack, Stun, Dead
}

public class NativeBow : Enemy
{
    public float CheckTime = 5f;
    public float RunTime = 5f;

    private BowPattern pattern;

    private float directionX;

    private float timer;

    private Bounds collisionArea;
    private Bounds VerticalArea;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pattern = BowPattern.Idle;

        timer = 0;

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
            case BowPattern.Idle:
                Idle();
                break;
            case BowPattern.Check:
                Check();
                break;
            case BowPattern.Aiming:
                Aiming();
                break;
            case BowPattern.NormalAttack:
                NormalAttack();
                break;
            case BowPattern.NearAttack:
                NearAttack();
                break;
            case BowPattern.DashAttack:
                DashAttack();
                break;
            case BowPattern.Stun:
                Stun();
                break;
            case BowPattern.Dead:
                Dead();
                break;
            default:
                Debug.LogError("NativePunch Pattern Error");
                break;
        }
    }

    private void Idle()
    {
        float distanceX = Mathf.Abs(player.transform.position.x - transform.position.x);
        float distanceY = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2)) < 10f)
        {
            pattern = BowPattern.Check;
        }
    }

    private void Check()
    {
        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();

        if (AreaCheck(VerticalArea))
        {
            pattern = BowPattern.NearAttack;
            return;
        }

        pattern = BowPattern.Aiming;
        // Walk
    }

    private void Aiming()
    {
        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();
        timer += Time.deltaTime;
        if (timer >= RunTime)
        {
            timer = 0;
            if (AreaCheck(VerticalArea))
            {

                pattern = BowPattern.DashAttack;
                return;
            }
            pattern = BowPattern.NormalAttack;
        }

        // Run
    }

    private void NormalAttack()
    {

    }

    private void NearAttack()
    {

    }

    private void DashAttack()
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

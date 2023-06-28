using UnityEngine;

public enum PunchPattern
{
    Idle = 0, Check, Run, NormalAttack, NearAttack, DashAttack, ChargeAttack, Stun, Dead
}

public class NativePunch : Enemy
{
    public float CheckTime = 5f;
    public float RunTime = 5f;

    private PunchPattern pattern;

    private float directionX;
    private float directionY;

    private float timer;

    private Bounds collisionArea;
    private Bounds VerticalArea;
    private Bounds HorizontalArea;

    private float rand;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pattern = PunchPattern.Idle;

        timer = 0;

        collisionArea.max = new Vector2(-5f, 2f);
        collisionArea.min = new Vector2(5f, 0f);

        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();

        HorizontalArea.max = new Vector2();
        HorizontalArea.min = new Vector2();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (pattern)
        {
            case PunchPattern.Idle:
                Idle();
                break;
            case PunchPattern.Check:
                Check();
                break;
            case PunchPattern.Run:
                Run();
                break;
            case PunchPattern.NormalAttack:
                NormalAttack();
                break;
            case PunchPattern.NearAttack:
                NearAttack();
                break;
            case PunchPattern.DashAttack:
                DashAttack();
                break;
            case PunchPattern.ChargeAttack:
                ChargeAttack();
                break;
            case PunchPattern.Stun:
                Stun();
                break;
            case PunchPattern.Dead:
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
            pattern = PunchPattern.Check;
        }
    }

    private void Check()
    {
        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();
        timer += Time.deltaTime;
        if (timer >= CheckTime)
        {
            timer = 0;
            pattern = PunchPattern.Run;
            return;
        }

        if (AreaCheck(VerticalArea))
        {
            timer = 0;
            pattern = PunchPattern.NormalAttack;
            return;
        }

        if (AreaCheck(HorizontalArea))
        {
            timer = 0;
            pattern = PunchPattern.NearAttack;
            return;
        }

        Status.Speed = 1f;

        // Walk
    }

    private void Run()
    {
        VerticalArea.max = new Vector2();
        VerticalArea.min = new Vector2();
        timer += Time.deltaTime;
        if (timer >= RunTime)
        {
            timer = 0;
            pattern = PunchPattern.Check;
            return;
        }

        if (AreaCheck(VerticalArea))
        {
            timer = 0;
            rand = Random.Range(0f, 10f);
            if (rand < 5f)
                pattern = PunchPattern.DashAttack;
            else
                pattern = PunchPattern.ChargeAttack;

            return;
        }

        if (AreaCheck(HorizontalArea))
        {
            timer = 0;
            pattern = PunchPattern.NearAttack;
            return;
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

    private void ChargeAttack()
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

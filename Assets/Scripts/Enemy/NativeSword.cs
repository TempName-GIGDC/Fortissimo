using UnityEngine;

public enum SwordPattern
{
    Idle = 0, Check, Run, NormalAttack, NearAttack, DashAttack, ChargeAttack, Stun, Dead
}

public class NativeSword : Enemy
{
    public float CheckTime = 5f;
    public float RunTime = 5f;

    private SwordPattern pattern;

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
        pattern = SwordPattern.Idle;

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
            case SwordPattern.Idle:
                Idle();
                break;
            case SwordPattern.Check:
                Check();
                break;
            case SwordPattern.Run:
                Run();
                break;
            case SwordPattern.NormalAttack:
                NormalAttack();
                break;
            case SwordPattern.NearAttack:
                NearAttack();
                break;
            case SwordPattern.DashAttack:
                DashAttack();
                break;
            case SwordPattern.ChargeAttack:
                ChargeAttack();
                break;
            case SwordPattern.Stun:
                Stun();
                break;
            case SwordPattern.Dead:
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
            pattern = SwordPattern.Check;
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
            pattern = SwordPattern.Run;
            return;
        }

        if (AreaCheck(VerticalArea))
        {
            timer = 0;
            rand = Random.Range(0f, 10f);
            if (rand < 7f)
                pattern = SwordPattern.NormalAttack;
            else
                pattern = SwordPattern.NearAttack;

            return;
        }

        if (AreaCheck(HorizontalArea))
        {
            timer = 0;
            pattern = SwordPattern.ChargeAttack;
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
            pattern = SwordPattern.Check;
            return;
        }

        if (AreaCheck(VerticalArea))
        {
            timer = 0;
            pattern = SwordPattern.DashAttack;
            return;
        }

        if (AreaCheck(HorizontalArea))
        {
            timer = 0;
            pattern = SwordPattern.ChargeAttack;
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

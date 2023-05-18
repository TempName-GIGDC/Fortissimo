using System;
using UnityEngine;

public enum CharacterType
{
    None = 0,
    Player,
    Enemy
}
[Serializable]
public class CharacterStatus
{
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float Jump { get; set; }
    [field: SerializeField] public float Defense { get; set; }
    [field: SerializeField] public float Pierce { get; set; }
    [field: SerializeField] public float GravityAccel { get; set; }
}
public abstract class Character : MonoBehaviour
{
    public Vector3 Direction;
    public CharacterStatus Status;
    public CharacterType Type;
    public CharacterController Controller;

    protected virtual void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        Gravity();
    }
    public void Attack(Character target)
    {
        target.Hit(Status.Damage, Status.Pierce);
    }
    public void Hit(float damage, float pierce)
    {
        Status.Hp -= damage * (1 - (Status.Defense - pierce));
    }
    public void Heal(float recovery)
    {
        Status.Hp += recovery;
    }
    private void Gravity()
    {
        if (!Controller.isGrounded)
            Direction.y -= Status.GravityAccel;
    }
}
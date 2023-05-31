using System;
using UnityEngine;

public enum CharacterType
{
    None = 0,
    Player,
    Enemy
}

public enum CharacterState 
{ 
    Idle = 0,
    Walk,
    Dash,
    Run,
    Jump
}

public enum CharacterDeBuff
{
    Normal = 0,
    bleeding,
    Stun,
    KnockBack
}

[Serializable]
public struct CharacterStatus
{
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public int JumpCount { get; set; }
    [field: SerializeField] public float JumpHeight { get; set; }
    [field: SerializeField] public float TimeToJumpApex { get; set; }
    [field: SerializeField] public float Defense { get; set; }
    [field: SerializeField] public float Pierce { get; set; }
}
public abstract class Character : MonoBehaviour
{
    public Vector3 Direction;
    public CharacterStatus Status;
    public CharacterType Type;

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
}
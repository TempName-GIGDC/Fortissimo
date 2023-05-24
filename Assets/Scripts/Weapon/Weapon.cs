using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    //기존 공격력
    [SerializeField] private int exsDamage;
    //기존 공격속도
    [SerializeField] private float exsAttackSpeed;
    //기존 사거리
    [SerializeField] private Bounds exsRange;
    //기존 치명타 확률
    [SerializeField] private float exsCriticalHitChance;
    //기존 데미지
    [SerializeField] private int exsCriticalHitDamage;
    //기존 관통율
    [SerializeField] private float exsPenetrate;
    //기존 속성
    [SerializeField] private WeaponType exsProperty;

    public int Damage
    {
        get => exsDamage;
        set => exsDamage = value;
    }

    public float AttackSpeed
    {
        get => exsAttackSpeed;
        set => exsAttackSpeed = value;
    }

    //사거리가 아직 안정해져서 보류
    public Bounds Range
    {
        get => exsRange;
        set => exsRange = value;
    }

    public float CriticalHitChance
    {
        get => exsCriticalHitChance;
        set => exsCriticalHitChance = value;
    }

    public int CriticalHitDamage
    {
        get => exsCriticalHitDamage;
        set => exsCriticalHitDamage = value;
    }

    public float Penetrate
    {
        get => exsPenetrate;
        set => exsPenetrate = value;
    }

    public WeaponType WType
    {
        get => exsProperty;
        set => exsProperty = value;
    }
}

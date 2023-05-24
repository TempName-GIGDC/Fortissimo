using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Shock,
    Penetrate,
    Cut
}

public interface IWeapon
{
    /// <summary>
    /// 공격력
    /// </summary>
    int Damage { get; set; }

    /// <summary>
    /// 공격속도
    /// </summary>
    float AttackSpeed { get; set; }

    /// <summary>
    /// 사거리
    /// </summary>
    Bounds Range { get; set; }

    /// <summary>
    /// 치명타 확률
    /// </summary>
    float CriticalHitChance { get; set; }

    /// <summary>
    /// 데미지
    /// </summary>
    int CriticalHitDamage { get; set; }

    /// <summary>
    /// 관통율
    /// </summary>
    float Penetrate { get; set; }

    /// <summary>
    /// 무기타입
    /// </summary>
    WeaponType WType { get; set; }
}

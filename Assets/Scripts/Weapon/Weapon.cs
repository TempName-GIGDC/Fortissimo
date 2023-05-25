using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponStruct
{
    /// <summary>
    /// 공격력
    /// </summary>
    public int Damage;
    /// <summary>
    /// 공격속도
    /// </summary>
    public float AttackSpeed;
    /// <summary>
    /// 사거리
    /// </summary>
    public Bounds Range;
    public Vector2 PointA;
    public Vector2 PointB;
    /// <summary>
    /// 치명타 확률
    /// </summary>
    public float CriticalHitChance;
    /// <summary>
    /// 치명타 데미지
    /// </summary>
    public int CriticalHitDamage;
    /// <summary>
    /// 관통율
    /// </summary>
    public float Penetrate;
    /// <summary>
    /// 충격 데미지 퍼센트
    /// </summary>
    public float Shock;
    /// <summary>
    /// 관통 데미지 퍼센트
    /// </summary>
    public float Pierce;
    /// <summary>
    /// 베기 데미지 퍼센트
    /// </summary>
    public float Cut;
}

public abstract class Weapon : MonoBehaviour
{
}

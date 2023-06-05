using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponInfo
{
    /// <summary>
    /// 레이어 마스크 변수
    /// </summary>
    public LayerMask LayerName;
    /// <summary>
    /// 공격력
    /// </summary>
    public int Damage;
    /// <summary>
    /// 공격속도
    /// </summary>
    public float AttackSpeed;
    /// <summary>
    /// 사거리 조절 변수
    /// </summary>
    public Vector2 Range;
    /// <summary>
    /// 사거리 위치 변수
    /// </summary>
    public Transform RangeP;
    /// <summary>
    /// 치명타 확률
    /// </summary>
    public int CriticalHitChance;
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


public class WeaponSystem : MonoBehaviour
{
    public List<WeaponInfo> weaponInfos;

    [ContextMenu("To Json Data")]
    void WeaponStatSave()
    {
        string json = JsonUtility.ToJson(weaponInfos);

        string FileName = "WeaponInfo";
        string path = Application.dataPath + "/" + FileName + ".Json";

        File.WriteAllText(path, json);
    }
}

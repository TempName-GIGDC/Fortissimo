using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/WeaponData", order = int.MaxValue)]
public class WeaponData : ScriptableObject
{
    public LayerMask LayerName; //레이어 마스크 변수(적이 사거리내에 들어왔는지 확인하기 위함)

    public string WeaponName; //무기 이름 변수
    
    public int Damage; //공격력 변수
    
    public float AttackSpeed; //공격 속도 변수
    
    public Vector2 Range; //사거리 조절 변수
    
    public int CriticalHitChance; //치명타 확률 변수
    
    public int CriticalHitDamage; //치명타 데미지 변수
    
    public float Penetrate; //관통율 변수
    
    public float Shock; // 충격 데미지 변수

    public float Pierce; //관통 데미지 변수
   
    public float Cut; //베기 데미지 변수
}
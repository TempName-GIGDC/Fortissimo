using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Damage: " + DamageSystem.CalculateDamage(weaponInfo.Damage, weaponInfo.CriticalHitChance, weaponInfo.CriticalHitDamage));
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(weaponInfo.RangeP.position, weaponInfo.Range, 0, weaponInfo.LayerName);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Enemyname: " + collider.name);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(weaponInfo.RangeP.position, weaponInfo.Range);
    }
}

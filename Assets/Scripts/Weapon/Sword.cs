using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private WeaponStruct weaponStruct;
    void Start()
    {
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Damage: " + DamageSystem.CalculateDamage(weaponStruct.Damage, weaponStruct.CriticalHitChance, weaponStruct.CriticalHitDamage));
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(weaponStruct.RangeP.position, weaponStruct.Range, 0, weaponStruct.LayerName);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Enemyname: " + collider.name);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(weaponStruct.RangeP.position, weaponStruct.Range);
    }
}

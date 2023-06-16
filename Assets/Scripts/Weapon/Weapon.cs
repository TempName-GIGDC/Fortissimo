using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public GameObject WeaponObject;
    public Transform WeaponTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Damage: " + DamageSystem.CalculateDamage(weaponData.Damage, weaponData.CriticalHitChance, weaponData.CriticalHitDamage));
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(WeaponTransform.position, weaponData.Range, 0, weaponData.LayerName);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Enemyname: " + collider.name);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(WeaponTransform.position, weaponData.Range);
    }
}

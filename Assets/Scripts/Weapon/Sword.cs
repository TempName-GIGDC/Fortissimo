using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private WeaponStruct weaponStruct;

    LayerMask enemyLayer;

    void Update()
    {

        Collider2D[] colliders = Physics2D.OverlapAreaAll(weaponStruct.PointA, weaponStruct.PointB, enemyLayer);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Enemyname: " + collider.gameObject.name);
        }
        
    }
    public void InputRangeCheck()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(weaponStruct.Range.center, weaponStruct.Range.size);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Weapon[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        weapons = FindObjectsOfType<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            WeaponData weaponData = weapon.weaponData;

            LayerMask layerName = weaponData.LayerName;

            string weaponName = weaponData.WeaponName;

            int damage = weaponData.Damage;

            float attackSpedd = weaponData.AttackSpeed;

            Vector2 range = weaponData.Range;

            float criticalHitChance = weaponData.CriticalHitChance;

            float criticalHitDamage = weaponData.CriticalHitDamage;

            float shock = weaponData.Shock;

            float pierce = weaponData.Pierce;

            float cut = weaponData.Cut;

            GameObject weaponObject = weapon.WeaponObject;
            Transform weaponTransform = weapon.WeaponTransform;

            Debug.Log("WeaponName: " + weaponName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

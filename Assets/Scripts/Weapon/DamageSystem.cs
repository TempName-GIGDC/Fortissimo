using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageSystem
{
    public static int CalculateDamage(int baseDamage, int CriticalHitChance, int CriticalHitDamage)
    {
        int damage = baseDamage;

        if (Random.Range(1, 100) <= CriticalHitChance)
        {
            damage *= CriticalHitDamage;
        }

        return damage;
    }
}

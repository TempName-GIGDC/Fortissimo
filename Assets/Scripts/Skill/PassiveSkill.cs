using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveSkill
{
    public string name;
    public string info;
    public int tier;
    public float hp;
    public float damage;
    public float speed;
    public float attackspeed;
    public float defence;
    public float pierce;
    
    public PassiveSkill(string _name, string _info, int _tier, float _hp, float _damage, float _speed, float _attackspeed, float _defence, float _pierce)
    {
        this.name = _name;
        this.info = _info;
        this.tier = _tier;
        this.hp = _hp;
        this.damage = _damage;
        this.speed = _speed;
        this.attackspeed = _attackspeed;
        this.defence = _defence;
        this.pierce = _pierce;
    }

    public void Show()
    {
        Debug.Log(this.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillSlot : MonoBehaviour
{
    [SerializeField]
    public PassiveSkill[] PassiveSlot = new PassiveSkill[4];
    public Player p;
    public PassiveSkillRead pt;

    private void Start()
    {
        PassiveSlot[0] = pt.PassiveSkillData["ªÔ¿ß¿œ√º"];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillEquip(PassiveSlot[0]);
        }
    }

    void SkillEquip(PassiveSkill MySkill)
    {
        p.Status.Hp += MySkill.hp;
        p.Status.Damage += MySkill.damage;
        p.Status.Speed += MySkill.speed;
        p.Status.AttackSpeed += MySkill.attackspeed;
        p.Status.Defense += MySkill.defence;
        p.Status.Pierce += MySkill.pierce;
    }
    void SkillUnEquip(PassiveSkill MySkill)
    {
        p.Status.Hp -= MySkill.hp;
        p.Status.Damage -= MySkill.damage;
        p.Status.Speed -= MySkill.speed;
        p.Status.AttackSpeed -= MySkill.attackspeed;
        p.Status.Defense -= MySkill.defence;
        p.Status.Pierce -= MySkill.pierce;
    }
}

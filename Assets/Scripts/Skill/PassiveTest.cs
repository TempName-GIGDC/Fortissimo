using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class PassiveTest : MonoBehaviour
{
    public List<Dictionary<string, object>> PassiveSkillCsv;
    public Dictionary<string, PassiveSkill> PassiveSkillData = new Dictionary<string, PassiveSkill>();
    void Awake()
    {
        PassiveSkillCsv = CSVReader.Read("PassiveSkillData");
        for (int i = 0; i < PassiveSkillCsv.Count; i++)
        {
            string name = PassiveSkillCsv[i]["name"].ToString();
            string info = PassiveSkillCsv[i]["info"].ToString();
            float hp = Convert.ToSingle(PassiveSkillCsv[i]["hp"]);
            float damage = Convert.ToSingle(PassiveSkillCsv[i]["damage"]);
            float speed = Convert.ToSingle(PassiveSkillCsv[i]["speed"]);
            float attackspeed = Convert.ToSingle(PassiveSkillCsv[i]["attackspeed"]);
            float defence = Convert.ToSingle(PassiveSkillCsv[i]["defence"]);
            float pierce = Convert.ToSingle(PassiveSkillCsv[i]["pierce"]);

            PassiveSkillData.Add(PassiveSkillCsv[i]["name"].ToString(), new PassiveSkill(name, info, hp, damage, speed, attackspeed, defence, pierce));
        }
    }
}

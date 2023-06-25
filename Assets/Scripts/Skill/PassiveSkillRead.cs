using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class PassiveSkillRead : MonoBehaviour
{
    public List<Dictionary<string, object>> PassiveSkillCsv;
    public Dictionary<string, PassiveSkill> PassiveSkillData = new Dictionary<string, PassiveSkill>();
    public Dictionary<string, Melody> PassiveMelody = new Dictionary<string, Melody>();
    void Awake()
    {
        PassiveSkillCsv = CSVReader.Read("PassiveSkillData");
        for (int i = 0; i < PassiveSkillCsv.Count; i++)
        {
            string name = PassiveSkillCsv[i]["name"].ToString();
            string info = PassiveSkillCsv[i]["info"].ToString();
            int tier = Convert.ToInt32(PassiveSkillCsv[i]["tier"]);
            float hp = Convert.ToSingle(PassiveSkillCsv[i]["hp"]);
            float damage = Convert.ToSingle(PassiveSkillCsv[i]["damage"]);
            float speed = Convert.ToSingle(PassiveSkillCsv[i]["speed"]);
            float attackspeed = Convert.ToSingle(PassiveSkillCsv[i]["attackspeed"]);
            float defence = Convert.ToSingle(PassiveSkillCsv[i]["defence"]);
            float pierce = Convert.ToSingle(PassiveSkillCsv[i]["pierce"]);

            PassiveSkillData.Add(name, new PassiveSkill(name, info, tier, hp, damage, speed, attackspeed, defence, pierce));
            PassiveMelody.Add(name, new Melody(name, PassiveSkillData[name]));
        }
    }
}

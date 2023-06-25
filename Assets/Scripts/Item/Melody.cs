using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Melody
{
    public string name;
    public PassiveSkill skill;


    public Melody(string _name, PassiveSkill _skill)
    {
        this.name = _name;
        this.skill = _skill;
    }

    public void Show()
    {
        Debug.Log(this.name);
    }
}

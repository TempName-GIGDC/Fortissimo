using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestSkill
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string info;

    public TestSkill(string _name, string _info)
    {
        this.name = _name;
        this.info = _info;
    }

    public void Show()
    {
        Debug.Log(this.name);
        Debug.Log(this.info);
    }
}

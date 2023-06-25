using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private PassiveSkillRead PassiveRead;
    public List<Melody> _melody;
    public int Gold;
    public int Pom;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            getMelody(PassiveRead.PassiveMelody["»ïÀ§ÀÏÃ¼"]);
        }
    }

    public void getGold(int amount)
    {
        Gold += amount;
    }

    public void getMelody(Melody myMelody)
    {
        if(_melody.Count > 0)
        {
            for (int i = 0; i < _melody.Count; i++)
            {
                if (_melody[i].name == myMelody.name)
                {
                    switch (myMelody.skill.tier)
                    {
                        case 1:
                            Pom += 10;
                            break;
                        case 2:
                            Pom += 50;
                            break;
                        case 3:
                            Pom += 100;
                            break;
                    }
                }
                else
                {
                    _melody.Add(myMelody);
                }
            }
        }
        else
        {
            _melody.Add(myMelody);
        }
    }

    public void MelodySort(string str, string order)
    {
        List<Melody> sortedMelody = new List<Melody>();
        switch (str)
        {
            case "Name":
                if(order == "Descending")
                {
                    sortedMelody = _melody.OrderByDescending(x => x.name).ToList();
                }
                else
                {
                    sortedMelody = _melody.OrderBy(x => x.name).ToList();
                }
                break;
            case "Tier":
                if (order == "Descending")
                {
                    sortedMelody = _melody.OrderByDescending(x => x.skill.tier).ToList();
                }
                else
                {
                    sortedMelody = _melody.OrderBy(x => x.skill.tier).ToList();
                }
                break;
        }
        _melody = sortedMelody;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInv : MonoBehaviour
{
    [SerializeField]
    private int wInvSize = 20;
    [SerializeField]
    private Weapon[] _weapons;
    [SerializeField]
    private int wInvLast = -1;
    private void Awake()
    {
        _weapons = new Weapon[wInvSize];
    }

    public void getWeapon(Weapon gWeapon)
    {
        wInvLast++;
        if (wInvLast >= wInvSize)
        {
            Debug.Log("더 이상 획득할 수 없습니다.");
            wInvLast = wInvSize - 1;
        }
        else
        {
            _weapons[wInvLast] = gWeapon;
        }
    }

    public void dropWeapon(int dropCursor)
    {
        if (wInvLast < 0)
        {
            Debug.Log("인벤토리가 비어있습니다.");
        }
        else
        {
            if (dropCursor == wInvSize - 1)
            {
                _weapons[dropCursor] = null;
            }
            else
            {
                for (int i = dropCursor; i < wInvLast - 2; i++)
                {
                    _weapons[i] = _weapons[i + 1];
                }
                _weapons[wInvLast] = null;
            }
            wInvLast--;
        }
    }
}

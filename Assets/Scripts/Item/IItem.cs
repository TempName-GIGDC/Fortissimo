using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    /// <summary>
    /// 아이템 사용확인
    /// </summary>
    bool ItemUse { get; set; }

    /// <summary>
    /// 던전 출입
    /// </summary>
    bool DungeonInOut { get; set; }
}


using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    public Dictionary<int, string> questDatas = new Dictionary<int, string>()
        { {1001, "Hit npc until his HP becomes lower or equal than 50."} };
    public Dictionary<int, bool> CompletedQuestCheck = new Dictionary<int, bool>();

    public bool CheckIfCompleteQuest_1001(ref int npcHp)
    {
        if (npcHp <= 50)
        {
            return true;
        }
        
        return false;
    }
}

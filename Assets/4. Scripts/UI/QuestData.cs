using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    public Dictionary<int, string> questDatas = new Dictionary<int, string>()
    {
        { 1001, "Take down the boss monster." }
    };
    
    public Dictionary<int, bool> CompletedQuestCheck = new Dictionary<int, bool>();

    public Dictionary<int, string> questScripts = new Dictionary<int, string>()
    {
        { 1001, "Thank you for saving me. I saw the giant monster kidnapped someone.\n" +
                "I guess that someone is who your looking for.\n" +
                "Why don't you just go to deal with him?\n" }
    };

    public bool CheckIfCompleteQuest_1001(ref int bossHp)
    {
        if (bossHp <= 0)
        {
            return true;
        }
        
        return false;
    }
}

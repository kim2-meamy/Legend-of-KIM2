using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, string> questContents;
    public QuestData questData;
    
    private TextMeshProUGUI playerQuestList;
    private Boss boss;
    private NpcStats npcStats;
    private GameObject endUI;

    void Awake()
    {
        questData = GetComponent<QuestData>();
        questContents = new Dictionary<int, string>();
        playerQuestList = GameObject.Find("PlayerQuestList").GetComponentInChildren<TextMeshProUGUI>();
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        npcStats = GameObject.FindGameObjectWithTag("Npc").GetComponent<NpcStats>();
        endUI = GameObject.FindGameObjectWithTag("End");
        endUI.SetActive(false);
    }

    private void Update()
    {
        if (questContents.ContainsKey(1001))
        {
            if (questData.CheckIfCompleteQuest_1001(ref boss.bossData.health))
            {
                QuestComplete(1001, ref npcStats.questCount);
                endUI.SetActive(true);
            }
        }
    }

    public void RegisterQuest(int npcID, ref int questCount)
    {
        ++questCount;

        var key = npcID * 1000 + questCount;
        var value = questData.questDatas[(npcID * 1000) + questCount];
        questContents.Add(key, value);
    }

    public void QuestComplete(int questID, ref int questCount)
    {
        questContents.Remove(questID);
        questData.CompletedQuestCheck[questID] = true;
        playerQuestList.text = playerQuestList.text.Replace(questData.questDatas[questID], "");
        questCount -= 1;
    }
}

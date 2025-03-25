using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, string> questContents;
    public QuestData questData;
    private TextMeshProUGUI playerQuestList;

    void Awake()
    {
        questData = GetComponent<QuestData>();
        questContents = new Dictionary<int, string>();
        playerQuestList = GameObject.Find("PlayerQuestList").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (questContents.ContainsKey(1001))
        {
        }
    }

    public void RegisterQuest(int npcID, ref int questCount)
    {
        questContents.Add((npcID * 1000) + (++questCount), questData.questDatas[(npcID * 1000) + (questCount)]);
    }

    public void QuestComplete(int questID, ref int questCount)
    {
        questContents.Remove(questID);
        questData.CompletedQuestCheck[questID] = true;
        Debug.Log($"Quest {questID} completed!");
        playerQuestList.text = playerQuestList.text.Replace(questData.questDatas[questID], "");
        questCount -= 1;
    }
}

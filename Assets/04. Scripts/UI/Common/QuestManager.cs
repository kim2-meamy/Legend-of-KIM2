using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    private UnityEvent<PlayerUIManager> registeredQuests;

    private PlayerUIManager playerUIManager;

    public QuestData InstQuestData { get; private set; }
    public NpcConversationData InstNpcConversationData { get; private set; }

    public void AddQuest(int questId)
    {
        registeredQuests.AddListener(InstQuestData.QuestDataList[questId].Contents);
        
        CheckClearQuests();
    }
    
    public void RemoveQuest(int questId)
    {
        registeredQuests.RemoveListener(InstQuestData.QuestDataList[questId].Contents);
    }
    
    public void CheckClearQuests()
    {
        registeredQuests.Invoke(playerUIManager);
    }

    private void Awake()
    {
        playerUIManager = GetComponent<PlayerUIManager>();
        
        registeredQuests = new UnityEvent<PlayerUIManager>();
        InstQuestData = new QuestData();
        InstNpcConversationData = new NpcConversationData();
    }

    private void Update()
    {
        CheckClearQuests();
    }
}

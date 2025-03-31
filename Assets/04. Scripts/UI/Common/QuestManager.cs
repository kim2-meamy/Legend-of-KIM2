using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    private UnityEvent<PlayerUIManager> registeredQuests;

    private PlayerUIManager playerUIManager;
    private TextMeshProUGUI playerQuestListContents;

    public QuestData QuestData { get; private set; }
    public NpcConversationData NpcConversationData { get; private set; }

    public void AddQuest(int questId)
    {
        registeredQuests.AddListener(QuestData.QuestDataList[questId].Contents);
        
        playerQuestListContents.text = QuestData.QuestDataList[questId].Name + "\n"
            + QuestData.QuestDataList[questId].Description;
    }
    
    public void RemoveQuest(int questId)
    {
        registeredQuests.RemoveListener(QuestData.QuestDataList[questId].Contents);
        
        string questValue = QuestData.QuestDataList[questId].Name + "\n" +
                      QuestData.QuestDataList[questId].Description;
        playerQuestListContents.text = playerQuestListContents.text.Replace(questValue, "");
    }

    private void Awake()
    {
        playerUIManager = GetComponent<PlayerUIManager>();
        playerQuestListContents = playerUIManager.QuestList.GetComponentInChildren<TextMeshProUGUI>();
        
        registeredQuests = new UnityEvent<PlayerUIManager>();
        QuestData = new QuestData();
        NpcConversationData = new NpcConversationData();
    }

    private void Update()
    {
        CheckClearQuests();
    }
    
    private void CheckClearQuests()
    {
        registeredQuests.Invoke(playerUIManager);
    }
}

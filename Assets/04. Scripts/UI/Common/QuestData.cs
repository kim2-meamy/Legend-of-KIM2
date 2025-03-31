using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public Dictionary<int, QuestForm> QuestDataList { get; }

    public QuestData()
    {
        QuestDataList = new Dictionary<int, QuestForm>();
        QuestDataList.Add(QuestId.QuestIdList["FirstStep"], new QuestForm(FirstStep,
            "First step", "Talk to Jaewoo for 5 time!"));
    }

    private void FirstStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FirstStep"];
        
        if (playerUIManager.ContactedNpcStats == null)
        {
            return;
        }

        if (playerUIManager.ContactedNpcStats.Id != 0)
        {
            return;
        }
        
        if (!playerUIManager.ContactedNpcStats.DoConversation && Input.GetKeyDown(KeyCode.E))
        {
            playerUIManager.QuestManager.QuestData.QuestDataList[questId].AddCounter();
        }

        if (playerUIManager.QuestManager.QuestData.QuestDataList[questId].Counter == 5)
        {
            playerUIManager.QuestManager.QuestData.QuestDataList[questId].ClearQuest();
        }
    }
}

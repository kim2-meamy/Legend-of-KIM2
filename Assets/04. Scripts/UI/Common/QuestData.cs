using System.Collections.Generic;
using System.Linq;

public class QuestData
{
    public Dictionary<int, QuestForm> QuestDataList { get; }

    public QuestData()
    {
        QuestDataList = new Dictionary<int, QuestForm>();
        QuestDataList.Add(QuestId.QuestIdList["FirstStep"], new QuestForm(FirstStep,
            "첫 번째 단서", ": 쿠쿠 대왕은 마을의 가장 높은 성에 있다."));
    }

    private void FirstStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FirstStep"];
        
        playerUIManager.QuestManager.NpcConversationData.
            NpcConversationDataList[NpcProfile.NpcIdList["재우"].Id].Children.First().GetAward();
        playerUIManager.ContactedNpcStats.SubtractQuestCount();
        playerUIManager.QuestManager.RemoveQuest(questId);
    }
}

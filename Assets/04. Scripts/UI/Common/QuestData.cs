using System;
using System.Collections.Generic;
using System.Linq;

public class QuestData
{
    public Dictionary<int, QuestForm> QuestDataList { get; }

    public QuestData()
    {
        QuestDataList = new Dictionary<int, QuestForm>();
        QuestDataList.Add(QuestId.QuestIdList["FirstStep"], new QuestForm(FirstStep,
            "난폭해진 슬라임", String.Empty, 
            "쿠쿠 대왕의 존재", "쿠쿠 대왕은 마을의 가장 높은 성에 있다."));
        QuestDataList.Add(QuestId.QuestIdList["SecondStep"], new QuestForm(SecondStep,
            "우는 아이 도와주기", "우는 아이에게 언덕 위의 집에 있는 두루마리 서신을 가져다 주자!",
            "우는 아이의 정체", "우는 아이는 사람이 아닌, 선령이었다."));
        QuestDataList.Add(QuestId.QuestIdList["GetPaper"], new QuestForm(GetPaper,
            "두루마리 서신", String.Empty,
            "서신의 내용", "이 글을 읽고 있다면, 너는 이미 진실에 가까워졌겠지. 하지만 그 진실이 반드시 너에게 정의를 의미하는 것은 아닐 것이다.\n" +
                      "쿠쿠섬은 오래전부터 하나의 균형 속에서 존재해왔다. 인간과 몬스터가 함께 살아가는 평화로운 땅. 그러나 그것은 한순간에 깨질 수 있는 거짓된 평화였다.\n" +
                      "이 섬의 심장부에는 ‘재앙’이 봉인되어 있다. 태초부터 존재했던 어둠.\n" +
                      "그것은 섬을 삼키려 했고, 섬을 지키기 위해 우리는 선택을 해야 했다.\n" +
                      "쿠쿠마왕은 스스로 어둠을 짊어지기로 했다.\n" +
                      "그는 봉인을 유지하기 위해 스스로 괴물이 되었다.\n" +
                      "그는 저주받은 왕이 되어, 이 섬의 몬스터들을 통제하고 봉인의 균열을 막고 있었다.\n" +
                      "그 희생이 없다면, 이 섬은 이미 멸망했을 것이다.\n" +
                      "하지만 이제 균형이 무너지고 있다.\n" +
                      "네가 쿠쿠마왕을 쓰러뜨린다면, 봉인은 완전히 해제될 것이다.\n" +
                      "그때, 네 여동생뿐만 아니라 쿠쿠섬 전체가 사라질 수도 있다.\n" +
                      "너에게 선택의 시간이 다가오고 있다.\n" +
                      "눈앞의 ‘악’을 없앨 것인가,\n" +
                      "혹은 진정한 ‘악’을 막기 위해 새로운 희생을 받아들일 것인가.\n" +
                      "부디, 이 진실을 가슴에 새기고 마지막 순간에 올바른 선택을 하기를.\n" +
                      "이 기록을 남기는 자,\n" +
                      "쿠쿠섬의 수호자이자, 희생자였던 자가.\n"));
        QuestDataList.Add(QuestId.QuestIdList["ThirdStep"], new QuestForm(ThirdStep,
            "선령 따라가기 1", String.Empty,
            "선령", "선령으로 변한 아이를 따라가면 무언가를 발견할 수 있을지도?"));
        QuestDataList.Add(QuestId.QuestIdList["FourthStep"], new QuestForm(FourthStep,
            "선령 따라가기 2", string.Empty,
            "아버지와 아이", "아버지와 아이는 하나의 선령으로 합쳐서 어딘가로 나아가고 있다.\n" +
                       "따라가보자."));
        QuestDataList.Add(QuestId.QuestIdList["FinalStep"], new QuestForm(FinalStep,
            "성불한 선령", string.Empty,
            "성불", "선령들의 도움으로 쿠쿠대왕에게 가는 길이 열렸다."));
        QuestDataList.Add(QuestId.QuestIdList["BossFight"], new QuestForm(BossFight,
            "최후의 전투", "쿠쿠 대왕을 처치하자!",
            String.Empty, String.Empty));
    }

    private void FirstStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FirstStep"];
        
        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["재우"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }

    private void SecondStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["SecondStep"];

        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["GetPaper"]].DidGetAward)
        {
            return;
        }
        
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();

        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].DidGetAward)
        {
            return;
        }
        
        playerUIManager.InstQuestManager.InstNpcConversationData.NpcId1ClearFirstQuest();
        playerUIManager.ContactedNpcDataRootNode =
            playerUIManager.InstQuestManager.
                InstNpcConversationData.NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이"].Id];
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
    
    private void GetPaper(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["GetPaper"];
        
        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["두루마리 서신"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
    
    private void ThirdStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["ThirdStep"];
        
        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
    
    private void FourthStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FourthStep"];

        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이의 아버지"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.InstNpcConversationData.NpcId3ClearFirstQuest();
        playerUIManager.ContactedNpcDataRootNode =
            playerUIManager.InstQuestManager.
                InstNpcConversationData.NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이의 아버지"].Id];
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
    
    private void FinalStep(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FinalStep"];
        
        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이의 아버지"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
    
    private void BossFight(PlayerUIManager playerUIManager)
    {
        int questId = QuestId.QuestIdList["FinalStep"];

        if (!playerUIManager.IsGameClear)
        {
            return;
        }
        
        playerUIManager.InstQuestManager.InstNpcConversationData.
            NpcConversationDataList[NpcProfile.NpcProfileList["쿠쿠 대왕"].Id].Children.First().GetAward();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].ClearQuest();
        playerUIManager.InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
        
        playerUIManager.InstQuestManager.RemoveQuest(questId);
    }
}

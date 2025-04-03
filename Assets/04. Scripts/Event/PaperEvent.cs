using UnityEngine;

public class PaperEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private GameObject paper;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    private void Awake()
    {
        paper.SetActive(false);
    }
    
    private void Update()
    {
        if (playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["SecondStep"]].DidAccept)
        {
            paper.SetActive(true);
        }

        if (playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["GetPaper"]].DidGetAward)
        {
            playerUIManager.Conversation.SetActive(false);
            playerUIManager.AskForConversation.SetActive(false);
            playerUIManager.ContactedNpcStats = null;
            paper.SetActive(false);
            eventObject.SetActive(false);
        }
    }
}

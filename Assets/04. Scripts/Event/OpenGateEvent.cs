using System;
using UnityEngine;

public class OpenGateEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject father;
    [SerializeField]
    private GameObject gateRock1;
    [SerializeField]
    private GameObject gateRock2;
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    private void Update()
    {
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["FinalStep"]].DidGetAward)
        {
            return;
        }
        
        father.SetActive(false);
        playerUIManager.Conversation.SetActive(false);
        playerUIManager.AskForConversation.SetActive(false);
        playerUIManager.ContactedNpcStats = null;
        
        gateRock1.SetActive(false);
        gateRock2.SetActive(false);
        eventObject.SetActive(false);
    }
}

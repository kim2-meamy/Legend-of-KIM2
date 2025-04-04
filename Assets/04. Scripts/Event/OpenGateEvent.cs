using UnityEngine;
using UnityEngine.Playables;

public class OpenGateEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject father;
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    [SerializeField]
    private PlayableDirector playableDirector;
    
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
        
        playableDirector.Play();
        eventObject.SetActive(false);
        
    }
}

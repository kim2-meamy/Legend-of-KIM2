using UnityEngine;

public class BossEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject player;

    private void Update()
    {
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["FinalStep"]].DidGetAward)
        {
            return;
        }
        
        boss.GetComponent<Boss>().target = null;
        
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["BossFight"]].DidAccept)
        {
            return;
        }

        playerUIManager.ContactedNpcStats = null;
        playerUIManager.AskForConversation.SetActive(false);
        boss.GetComponent<NpcStats>().enabled = false;
        boss.GetComponent<Boss>().target = player.transform;
    }
}

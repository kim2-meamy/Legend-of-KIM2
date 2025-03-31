using UnityEngine;

public class NpcStats : MonoBehaviour
{
    [SerializeField]
    private GameObject npcObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;

    public int Id { get; private set; }
    public int QuestCount { get; private set; }

    public bool DoConversation { get; set; }
    public bool CanConversation { get; private set; }

    public void SubtractQuestCount()
    {
        QuestCount--;
    }

    private void Awake()
    {
        Id = NpcProfile.NpcIdList[npcObject.name].Id;
        QuestCount = NpcProfile.NpcIdList[npcObject.name].QuestCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanConversation = true;
            playerUIManager.ContactedNpcStats = this;
            playerUIManager.ActivateUI(playerUIManager.AskForConversation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanConversation = false;
            DoConversation = false;
            playerUIManager.DeactivateUI(playerUIManager.AskForConversation);
            playerUIManager.DeactivateUI(playerUIManager.Conversation);
            playerUIManager.ContactedNpcStats = null;
        }
    }
}

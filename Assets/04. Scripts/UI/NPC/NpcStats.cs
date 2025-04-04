using UnityEngine;

public class NpcStats : MonoBehaviour
{
    [SerializeField]
    private GameObject npcObject;
    public GameObject NpcObject => npcObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;

    public int Id { get; private set; }

    public bool DoConversation { get; set; }
    public bool CanConversation { get; set; }

    private void Awake()
    {
        Id = NpcProfile.NpcProfileList[NpcObject.name].Id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanConversation = true;
            playerUIManager.ContactedNpcStats = this;
            playerUIManager.AskForConversation.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanConversation = false;
            DoConversation = false;
            playerUIManager.AskForConversation.SetActive(false);
            playerUIManager.Conversation.SetActive(false);
            playerUIManager.DestroyOptionImage();
            playerUIManager.ContactedNpcStats = null;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class SlimeQuest : MonoBehaviour
{
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    [SerializeField]
    private List<GameObject> list;
    [SerializeField]
    private GameObject player;
    
    void OnTriggerEnter(Collider other)
    {
        foreach(GameObject obj in list)
        {
            obj.GetComponent<Enemy>().target = player.transform;
        }
    }

    private void Update()
    {
        if (playerUIManager.ContactedNpcStats == null)
        {
            return;
        }
        
        playerUIManager.ContactedNpcStats.CanConversation = false;
        playerUIManager.AskForConversation.SetActive(false);
        
        list.RemoveAll(item => item == null);
        
        if (list.Count == 0)
        {
            playerUIManager.ContactedNpcStats.CanConversation = true;
            playerUIManager.AskForConversation.SetActive(true);
            eventObject.SetActive(false);
        }
    }
}

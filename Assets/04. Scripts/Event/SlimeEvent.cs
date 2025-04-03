using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeEvent : MonoBehaviour
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
            if (obj.IsDestroyed() || obj == null)
            {
                continue;
            }
            obj.GetComponent<Enemy>().target = player.transform;
        }
    }

    private void Update()
    {
        if (playerUIManager.ContactedNpcStats == null)
        {
            return;
        }

        if (playerUIManager.ContactedNpcStats.Id == NpcProfile.NpcProfileList["재우"].Id)
        {
            playerUIManager.ContactedNpcStats.CanConversation = false;
            playerUIManager.AskForConversation.SetActive(false);
        }
        
        list.RemoveAll(item => item == null);
        
        if (list.Count == 0)
        {
            if (playerUIManager.ContactedNpcStats != null)
            {
                playerUIManager.ContactedNpcStats.CanConversation = true;
                playerUIManager.AskForConversation.SetActive(true);
            }
            
            eventObject.SetActive(false);
        }
    }
}

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
    private bool isSlimesKilled = false;

    [SerializeField]
    private GameObject boundary;
    
    void OnTriggerEnter(Collider other)
    {
        foreach(GameObject obj in list)
        {
            if (obj.IsDestroyed() || obj == null)
            {
                continue;
            }
            obj.GetComponent<Enemy>().target = playerUIManager.Player.transform;
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
            if (!isSlimesKilled)
            {
                playerUIManager.ContactedNpcStats.CanConversation = false;
                playerUIManager.AskForConversation.SetActive(false);
            }
        }
        
        list.RemoveAll(item => item == null);
        
        if (list.Count == 0)
        {
            if (playerUIManager.ContactedNpcStats != null)
            {
                if (!isSlimesKilled)
                {
                    playerUIManager.ContactedNpcStats.CanConversation = true;
                    playerUIManager.AskForConversation.SetActive(true);
                }
                
                isSlimesKilled = true;
            }
        }

        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["FirstStep"]].DidGetAward)
        {
            return;
        }
        
        boundary.SetActive(false);
        eventObject.SetActive(false);
    }
}

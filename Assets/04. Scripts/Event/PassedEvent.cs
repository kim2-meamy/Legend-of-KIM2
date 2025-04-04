using UnityEngine;
using UnityEngine.AI;

public class PassedEvent : MonoBehaviour
{
    private const float WaitDistance = 6f;
    private const float ArriveDistance = 5f;
    
    [SerializeField]
    private GameObject father;
    private NavMeshAgent fatherNavMeshAgent;
    [SerializeField]
    private GameObject boy;
    
    [SerializeField]
    private GameObject eventDestination;
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    private void Update()
    {
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["FourthStep"]].DidAccept)
        {
            return;
        }
        
        boy.SetActive(false);
        
        var fatherSphereCollider = father.GetComponent<SphereCollider>();
        fatherSphereCollider.enabled = false;
        
        playerUIManager.ContactedNpcStats = null;
        
        playerUIManager.Conversation.SetActive(false);
        playerUIManager.AskForConversation.SetActive(false);
        
        fatherNavMeshAgent = father.GetComponent<NavMeshAgent>();
        fatherNavMeshAgent.SetDestination(eventDestination.transform.position);

        if (Vector3.Distance(playerUIManager.Player.transform.position, father.transform.position) > WaitDistance)
        {
            fatherNavMeshAgent.isStopped = true;
        }
        else
        {
            fatherNavMeshAgent.isStopped = false;
        }

        if (Vector3.Distance(father.transform.position, eventDestination.transform.position) < ArriveDistance)
        {
            fatherSphereCollider.enabled = true;
            eventObject.SetActive(false);
            
        }
    }
}

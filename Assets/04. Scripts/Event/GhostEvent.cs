using UnityEngine;
using UnityEngine.AI;

public class GhostEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject boy;
    private NavMeshAgent boyNavMeshAgent;
    
    [SerializeField]
    private GameObject boyHumanForm;
    [SerializeField]
    private GameObject boyGhostForm;
    [SerializeField]
    private GameObject father;
    [SerializeField]
    private GameObject eventObject;
    [SerializeField]
    private PlayerUIManager playerUIManager;
    
    private void Update()
    {
        if (!playerUIManager.InstQuestManager.InstQuestData.QuestDataList[QuestId.QuestIdList["ThirdStep"]].DidAccept)
        {
            return;
        }
        
        var boySphereCollider = boy.GetComponent<SphereCollider>();
        boySphereCollider.enabled = false;
        var fatherSphereCollider = father.GetComponent<SphereCollider>();
        fatherSphereCollider.enabled = false;
        
        playerUIManager.ContactedNpcStats = null;
        
        boyHumanForm.SetActive(false);
        boyGhostForm.SetActive(true);
        father.SetActive(true);
        
        playerUIManager.Conversation.SetActive(false);
        playerUIManager.AskForConversation.SetActive(false);
        
        boyNavMeshAgent = boy.GetComponent<NavMeshAgent>();
        boyNavMeshAgent.SetDestination(father.transform.position);

        if (Vector3.Distance(playerUIManager.Player.transform.position, boy.transform.position) > 6f)
        {
            boyNavMeshAgent.isStopped = true;
        }
        else
        {
            boyNavMeshAgent.isStopped = false;
        }

        if (Vector3.Distance(boy.transform.position, father.transform.position) < 5f)
        {
            fatherSphereCollider.enabled = true;
            eventObject.SetActive(false);
        }
    }
}

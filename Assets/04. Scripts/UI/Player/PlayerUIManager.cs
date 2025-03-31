using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public NpcStats ContactedNpcStats { get; set; }
    
    public QuestManager QuestManager { get; private set; }

    [SerializeField]
    private GameObject player;
    public GameObject Player => player;
    [SerializeField]
    private GameObject healthBar;
    
    [SerializeField]
    private GameObject askForConversation;
    public GameObject AskForConversation => askForConversation;

    [SerializeField]
    private GameObject conversation; 
    public GameObject Conversation => conversation;
    private TextMeshProUGUI conversationText;
    private Button questAcceptButton;
    
    [SerializeField]
    private GameObject questList;
    public GameObject QuestList => questList;
    private bool isQuestListOpen = false;
    
    [SerializeField]
    private GameObject gamePause;
    private bool isGamePause = false;
    
    [SerializeField]
    private GameObject gameClear;
    
    private TreeNode contactedNpcDataRootNode;
    private TreeNode contactedNpcDataCurrentNode;

    public void ActivateUI(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    public void DeactivateUI(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }

    private void Awake()
    {
        QuestManager = GetComponent<QuestManager>();
        conversationText = GetComponentInChildren<TextMeshProUGUI>();
        
        // Hp 게이지 제외 모두 비활성화
        DeactivateUI(askForConversation);
        DeactivateUI(conversation);
        DeactivateUI(questList);
        DeactivateUI(gamePause);
        DeactivateUI(gameClear);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnConversation();
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnSelectPrev();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnSelectFirst();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnSelectSecond();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnSelectThird();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!ContactedNpcStats)
            {
                return;
            }
            
            OnSelectFourth();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnQuestList();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnGamePause();
        }
    }

    private void OnConversation()
    {
        if (!ContactedNpcStats.CanConversation)
            return;
        
        if (!ContactedNpcStats.DoConversation)
        {
            ContactedNpcStats.DoConversation = true;
            ActivateUI(conversation);
            contactedNpcDataRootNode = QuestManager.NpcConversationData.NpcConversationDataList[ContactedNpcStats.Id];
            contactedNpcDataCurrentNode = contactedNpcDataRootNode;
            conversationText.text = contactedNpcDataCurrentNode.Contents;
            DeactivateUI(askForConversation);
            
            // 말을 걸면 npc가 플레이어를 바라보게 함
            ContactedNpcStats.transform.LookAt(player.transform);
        }
        else
        {
            ContactedNpcStats.DoConversation = false;
            ActivateUI(askForConversation);
            contactedNpcDataRootNode = null;
            contactedNpcDataCurrentNode = null;
            DeactivateUI(conversation);
        }
    }
    
    private void OnSelectPrev()
    {
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }
        
        if (ContactedNpcStats.DoConversation)
        {
            if (contactedNpcDataCurrentNode.Parent == null)
            {
                return;
            }
            
            contactedNpcDataCurrentNode = contactedNpcDataCurrentNode.Parent;
            conversationText.text = contactedNpcDataCurrentNode.Contents;
        }
    }

    private void OnSelectFirst()
    {
        const int index = 0;
        
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }

        try
        {
            if (!ContactedNpcStats.DoConversation)
            {
                return;
            }

            if (contactedNpcDataCurrentNode.IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + contactedNpcDataCurrentNode.QuestId;
                
                if (QuestManager.QuestData.QuestDataList[questId].DidClear)
                {
                    QuestManager.RemoveQuest(questId);
                    ContactedNpcStats.SubtractQuestCount();
                    
                    contactedNpcDataCurrentNode.GetAward();
                    contactedNpcDataCurrentNode = contactedNpcDataRootNode;
                    conversationText.text = contactedNpcDataCurrentNode.Contents;

                    return;
                }
            }
                
            if (contactedNpcDataCurrentNode.IsQuestRegister)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + contactedNpcDataCurrentNode.QuestId;
                
                QuestManager.AddQuest(questId);
                contactedNpcDataCurrentNode = contactedNpcDataRootNode;
            }
            else
            {
                if (contactedNpcDataCurrentNode.Children[index].DidGetAward)
                {
                    return;   
                }
                
                contactedNpcDataCurrentNode = contactedNpcDataCurrentNode.Children[index];
            }
            
            conversationText.text = contactedNpcDataCurrentNode.Contents;
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }
    
    private void OnSelectSecond()
    {
        const int index = 1;
        
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }

        try
        {
            if (!ContactedNpcStats.DoConversation)
            {
                return;
            }
            
            if (contactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
                
            contactedNpcDataCurrentNode = contactedNpcDataCurrentNode.Children[index];
            conversationText.text = contactedNpcDataCurrentNode.Contents;
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }
    
    private void OnSelectThird()
    {
        const int index = 2;
        
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }

        try
        {
            if (!ContactedNpcStats.DoConversation)
            {
                return;
            }
            
            if (contactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
                
            contactedNpcDataCurrentNode = contactedNpcDataCurrentNode.Children[index];
            conversationText.text = contactedNpcDataCurrentNode.Contents;
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }
    
    private void OnSelectFourth()
    {
        const int index = 3;
        
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }

        try
        {
            if (ContactedNpcStats.DoConversation)
            {
                return;
            }
            
            if (contactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
                
            contactedNpcDataCurrentNode = contactedNpcDataCurrentNode.Children[index];
            conversationText.text = contactedNpcDataCurrentNode.Contents;
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }

    private void OnQuestList()
    {
        if (!isQuestListOpen)
        {
            ActivateUI(questList);
            isQuestListOpen = true;            
        }
        else
        {
            DeactivateUI(questList);
            isQuestListOpen = false;
        }
    }

    private void OnGamePause()
    {
        if (!isGamePause)
        {
            ActivateUI(gamePause);
            Time.timeScale = 0f;
            isGamePause = true;
        }
        else
        {
            DeactivateUI(gamePause);
            Time.timeScale = 1f;
            isGamePause = false;
        }
    }
}

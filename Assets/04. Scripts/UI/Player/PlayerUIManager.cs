using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

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
    private RectTransform conversationRectTransform;

    [SerializeField]
    private GameObject defaultOptionImage;
    [SerializeField]
    private GameObject nonAcceptedQuestOptionImage;
    [SerializeField]
    private GameObject acceptedQuestOptionImage;
    [SerializeField]
    private GameObject clearedQuestOptionImage;
    private GameObject selectedOptionImage;
    private List<GameObject> instOptionImageList;
    
    [SerializeField]
    private GameObject questList;
    public GameObject QuestList => questList;
    private bool isQuestListOpen = false;
    
    [SerializeField]
    private GameObject gamePause;
    private bool isGamePause = false;
    
    [SerializeField]
    private GameObject gameClear;
    
    public TreeNode ContactedNpcDataRootNode { get; private set; }
    public TreeNode ContactedNpcDataCurrentNode { get; private set; }

    public void ActivateUI(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    public void DeactivateUI(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }

    private void CreateOptionImage(TreeNode node)
    {
        const float DeltaY = 220f;
        
        float startX = 790f;
        float startY = 330f;
        
        for (int i = 0; i < node.Children?.Count; i++)
        {
            if (node.Children[i].DidGetAward)
            {
                continue;
            }
            
            if (node.Children[i].IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + node.Children[i].QuestId;
                if (QuestManager.QuestData.QuestDataList[questId].DidAccept)
                {
                    if (QuestManager.QuestData.QuestDataList[questId].DidClear)
                    {
                        selectedOptionImage = clearedQuestOptionImage;
                    }
                    else
                    {
                        selectedOptionImage = acceptedQuestOptionImage;
                    }
                }
                else
                {
                    selectedOptionImage = nonAcceptedQuestOptionImage;
                }
            }
            else
            {
                selectedOptionImage = defaultOptionImage;
            }
            
            Vector3 spawnLocation = new Vector3(startX, startY, 0f);
            var optionImageInstance = Instantiate(selectedOptionImage,
                spawnLocation, new Quaternion(0f, 0f, 0f, 0f));
            optionImageInstance.transform.SetParent(conversationRectTransform.transform, false);
            startY -= DeltaY;
            
            instOptionImageList.Add(optionImageInstance);
            
            var optionText = optionImageInstance.GetComponentInChildren<TextMeshProUGUI>();
            optionText.text = $"{i + 1}) " + node.Children[i].PlayerAnswer;
        }
    }

    private void DestroyOptionImage()
    {
        for (int i = 0; i < instOptionImageList.Count; i++)
        {
            Destroy(instOptionImageList[i]);
        }
    }

    private void Awake()
    {
        QuestManager = GetComponent<QuestManager>();
        conversationText = GetComponentInChildren<TextMeshProUGUI>();
        conversationRectTransform = conversation.GetComponent<RectTransform>();
        instOptionImageList = new List<GameObject>();
        
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
            ContactedNpcDataRootNode = QuestManager.NpcConversationData.NpcConversationDataList[ContactedNpcStats.Id];
            ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            DeactivateUI(askForConversation);
            CreateOptionImage(ContactedNpcDataCurrentNode);
            
            // 말을 걸면 npc가 플레이어를 바라보게 함
            ContactedNpcStats.transform.LookAt(player.transform);
        }
        else
        {
            DestroyOptionImage();
            ContactedNpcStats.DoConversation = false;
            ActivateUI(askForConversation);
            ContactedNpcDataRootNode = null;
            ContactedNpcDataCurrentNode = null;
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
            if (ContactedNpcDataCurrentNode.Parent == null)
            {
                return;
            }
            
            DestroyOptionImage();
            ContactedNpcDataCurrentNode = ContactedNpcDataCurrentNode.Parent;
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            CreateOptionImage(ContactedNpcDataCurrentNode);
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

            if (ContactedNpcDataCurrentNode.IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + ContactedNpcDataCurrentNode.QuestId;
                
                if (QuestManager.QuestData.QuestDataList[questId].DidClear)
                {
                    QuestManager.RemoveQuest(questId);
                    ContactedNpcStats.SubtractQuestCount();
                    
                    ContactedNpcDataCurrentNode.GetAward();
                    DestroyOptionImage();
                    
                    ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                    conversationText.text = ContactedNpcDataCurrentNode.Contents;
                    CreateOptionImage(ContactedNpcDataCurrentNode);

                    return;
                }
            }
                
            if (ContactedNpcDataCurrentNode.IsQuestRegister)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + ContactedNpcDataCurrentNode.QuestId;
                
                QuestManager.AddQuest(questId);
                QuestManager.QuestData.QuestDataList[questId].AcceptQuest();
                DestroyOptionImage();
                ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
            }
            else
            {
                if (ContactedNpcDataCurrentNode.Children[index].DidGetAward)
                {
                    return;   
                }
                
                DestroyOptionImage();
                ContactedNpcDataCurrentNode = ContactedNpcDataCurrentNode.Children[index];
            }
            
            
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            CreateOptionImage(ContactedNpcDataCurrentNode);
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
            
            if (ContactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
            
            DestroyOptionImage();
            ContactedNpcDataCurrentNode = ContactedNpcDataCurrentNode.Children[index];
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            CreateOptionImage(ContactedNpcDataCurrentNode);
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
            
            if (ContactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
            
            DestroyOptionImage();
            ContactedNpcDataCurrentNode = ContactedNpcDataCurrentNode.Children[index];
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            CreateOptionImage(ContactedNpcDataCurrentNode);
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
            
            if (ContactedNpcDataCurrentNode.Children[index].DidGetAward)
            {
                return;
            }
            
            DestroyOptionImage();
            ContactedNpcDataCurrentNode = ContactedNpcDataCurrentNode.Children[index];
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            CreateOptionImage(ContactedNpcDataCurrentNode);
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

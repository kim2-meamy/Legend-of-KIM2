using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public NpcStats ContactedNpcStats { get; set; }
    
    public QuestManager InstQuestManager { get; private set; }

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
    [SerializeField]
    private TextMeshProUGUI conversationText;
    [SerializeField]
    private TextMeshProUGUI npcNameText;
    private RectTransform conversationRectTransform;

    [SerializeField]
    private GameObject defaultOptionImage;
    [SerializeField]
    private GameObject prevOptionImage;
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
    [SerializeField]
    private GameObject questButton;
    [SerializeField]
    private RectTransform questScrollViewContentRectTransform;
    public GameObject QuestList => questList;
    private List<GameObject> instQuestButtonList;
    private bool isQuestListOpen = false;
    
    [SerializeField]
    private GameObject questDescription;
    [SerializeField]
    private TextMeshProUGUI questDescriptionName;
    [SerializeField]
    private TextMeshProUGUI questDescriptionContents;
    private bool isQuestDescriptionOpen = false;
    
    [SerializeField]
    private GameObject gamePause;
    private bool isGamePause = false;
    
    [SerializeField]
    private GameObject gameClear;
    public bool IsGameClear { get; set; }
    
    public TreeNode ContactedNpcDataRootNode { get; set; }
    public TreeNode ContactedNpcDataCurrentNode { get; set; }
    
    public void DestroyOptionImage()
    {
        for (int i = 0; i < instOptionImageList.Count; i++)
        {
            Destroy(instOptionImageList[i]);
        }
    }

    private void CreateOptionImage(TreeNode node)
    {
        const float DeltaY = 110f;
        const float StartX = 450f;
        const float StartY = 230f;
        
        float nextX = 450f;
        float nextY = 230f;

        Vector3 spawnLocation;
        GameObject optionImageInstance;
        TextMeshProUGUI optionText;
        
        for (int i = node.Children.Count - 1; i >= 0; i--)
        {
            if (node.Children[i].DidGetAward)
            {
                continue;
            }
            
            if (node.Children[i].IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + node.Children[i].QuestId;
                if (InstQuestManager.InstQuestData.QuestDataList[questId].DidAccept)
                {
                    if (InstQuestManager.InstQuestData.QuestDataList[questId].DidClear)
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

            for (int j = node.Children[i].PlayerAnswer.Count - 1; j >= 0 ; j--)
            {
                spawnLocation = new Vector3(nextX, nextY, 0f);
                    
                optionImageInstance = Instantiate(selectedOptionImage,
                    spawnLocation, Quaternion.identity);
                optionImageInstance.transform.SetParent(conversationRectTransform.transform, false);
            
                instOptionImageList.Add(optionImageInstance);
                    
                nextY += DeltaY;
                optionText = optionImageInstance.GetComponentInChildren<TextMeshProUGUI>();
                
                if (j == 0)
                {
                    optionText.text = $"{i + 1}) " + node.Children[i].PlayerAnswer[j];
                }
                else
                {
                    optionText.text = node.Children[i].PlayerAnswer[j];
                }
            }
        }
        
        if (node == ContactedNpcDataRootNode)
        {
            return;
        }
                    
        const float InitDeltaX = 1100f;
                    
        spawnLocation = new Vector3(StartX - InitDeltaX, StartY, 0f);
                    
        optionImageInstance = Instantiate(prevOptionImage,
            spawnLocation, Quaternion.identity);
        optionImageInstance.transform.SetParent(conversationRectTransform.transform, false);
            
        instOptionImageList.Add(optionImageInstance);
                    
        optionText = optionImageInstance.GetComponentInChildren<TextMeshProUGUI>();
        optionText.text = NpcConversationData.PrevOptionImageText;
    }
    
    private void CreateQuestButton(int questId)
    {
        var spawnLocation = Vector3.zero;
                    
        var questButtonInstance = Instantiate(questButton,
            spawnLocation, Quaternion.identity);
        questButtonInstance.transform.SetParent(questScrollViewContentRectTransform.transform, false);
        
        var questDescriptionData = questButtonInstance.GetComponentInChildren<QuestDescriptionData>();
        var questButtonValue = questButtonInstance.GetComponent<Button>();
        
        questDescriptionData.QuestId = questId;
        questButtonValue.onClick.AddListener(() =>
        {
            if (!isQuestDescriptionOpen)
            {
                questDescription.SetActive(true);
                isQuestDescriptionOpen = true;

                if (!InstQuestManager.InstQuestData.QuestDataList[questId].DidGetAward)
                {
                    questDescriptionName.text = InstQuestManager.InstQuestData.QuestDataList[questId].Name;
                    questDescriptionContents.text = InstQuestManager.InstQuestData.QuestDataList[questId].Description;

                    if (InstQuestManager.InstQuestData.QuestDataList[questId].DidClear)
                    {
                        questDescriptionContents.text += "\n( 완료! )";
                    }
                }
                else
                {
                    questDescriptionName.text = InstQuestManager.InstQuestData.QuestDataList[questId].ClueName;
                    questDescriptionContents.text =
                        InstQuestManager.InstQuestData.QuestDataList[questId].ClueDescription;
                }
            }
            else
            {
                if (!InstQuestManager.InstQuestData.QuestDataList[questId].DidGetAward)
                {
                    if (InstQuestManager.InstQuestData.QuestDataList[questId].Name != questDescriptionName.text)
                    {
                        questDescriptionName.text = InstQuestManager.InstQuestData.QuestDataList[questId].Name;
                        questDescriptionContents.text =
                            InstQuestManager.InstQuestData.QuestDataList[questId].Description;
                    }
                    else
                    {
                        questDescription.SetActive(false);
                        isQuestDescriptionOpen = false;
                    }
                }
                else
                {
                    if (InstQuestManager.InstQuestData.QuestDataList[questId].ClueName != questDescriptionName.text)
                    {
                        questDescriptionName.text = InstQuestManager.InstQuestData.QuestDataList[questId].ClueName;
                        questDescriptionContents.text =
                            InstQuestManager.InstQuestData.QuestDataList[questId].ClueDescription;
                    }
                    else
                    {
                        questDescription.SetActive(false);
                        isQuestDescriptionOpen = false;
                    }
                }
            }
        });
        
        instQuestButtonList.Add(questButtonInstance);
        
        var buttonText = questButtonInstance.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = InstQuestManager.InstQuestData.QuestDataList[questId].Name;
    }

    private void Awake()
    {
        InstQuestManager = GetComponent<QuestManager>();
        conversationRectTransform = conversation.GetComponent<RectTransform>();
        instOptionImageList = new List<GameObject>();
        instQuestButtonList = new List<GameObject>();
        
        // Hp 게이지 제외 모두 비활성화
        askForConversation.SetActive(false);
        conversation.SetActive(false);
        questList.SetActive(false);
        questDescription.SetActive(false);
        gamePause.SetActive(false);
        gameClear.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnGamePause();
        }

        if (isGamePause)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnQuestList();
        }
        
        if (Time.timeScale == 0f)
        {
            return;
        }
        
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
    }

    private void OnConversation()
    {
        if (!ContactedNpcStats.CanConversation)
        {
            return;
        }
        
        if (!ContactedNpcStats.DoConversation)
        {
            ContactedNpcStats.DoConversation = true;
            conversation.SetActive(true);
            ContactedNpcDataRootNode = InstQuestManager.InstNpcConversationData.
                NpcConversationDataList[ContactedNpcStats.Id];
            ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
            conversationText.text = ContactedNpcDataCurrentNode.Contents;
            npcNameText.text = ContactedNpcStats.NpcObject.name;
            askForConversation.SetActive(false);
            CreateOptionImage(ContactedNpcDataCurrentNode);

            if (ContactedNpcStats.Id == NpcProfile.NpcProfileList["두루마리 서신"].Id)
            {
                return;
            }
            
            // 말을 걸면 npc가 플레이어를 바라보게 함
            ContactedNpcStats.transform.LookAt(player.transform);
        }
        else
        {
            DestroyOptionImage();
            ContactedNpcStats.DoConversation = false;
            askForConversation.SetActive(true);
            ContactedNpcDataRootNode = null;
            ContactedNpcDataCurrentNode = null;
            conversation.SetActive(false);
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
                
            if (ContactedNpcDataCurrentNode.IsQuestRegister)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount + ContactedNpcDataCurrentNode.QuestId;
                
                for (int i = 0; i < instQuestButtonList?.Count; i++)
                {
                    var buttonQuestId = instQuestButtonList[i].GetComponent<QuestDescriptionData>();

                    if (buttonQuestId.QuestId == questId)
                    {
                        DestroyOptionImage();
                        
                        ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                        conversationText.text = ContactedNpcDataCurrentNode.Contents;
                        CreateOptionImage(ContactedNpcDataCurrentNode);
                        
                        return;
                    }
                }

                CreateQuestButton(questId);
                InstQuestManager.AddQuest(questId);
                InstQuestManager.InstQuestData.QuestDataList[questId].AcceptQuest();
                DestroyOptionImage();
                ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                
                Conversation.SetActive(false);
                ContactedNpcStats.DoConversation = false;
                AskForConversation.SetActive(true);
                ContactedNpcStats.CanConversation = true;
            }
            else
            {
                if (ContactedNpcDataCurrentNode.Children[index].DidGetAward)
                {
                    return;   
                }
                
                if (ContactedNpcDataCurrentNode.Children[index].IsQuestEntry)
                {
                    int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount +
                                  ContactedNpcDataCurrentNode.Children[index].QuestId;
                
                    if (InstQuestManager.InstQuestData.QuestDataList[questId].DidClear)
                    {
                        ContactedNpcDataCurrentNode.GetAward();
                        InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
                        DestroyOptionImage();
                    
                        InstQuestManager.CheckClearQuests();
                        ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                        conversationText.text = ContactedNpcDataCurrentNode.Contents;
                        CreateOptionImage(ContactedNpcDataCurrentNode);

                        return;
                    }
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
            
            if (ContactedNpcDataCurrentNode.Children[index].IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount +
                              ContactedNpcDataCurrentNode.Children[index].QuestId;
                
                if (InstQuestManager.InstQuestData.QuestDataList[questId].DidClear)
                {
                    ContactedNpcDataCurrentNode.GetAward();
                    InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
                    DestroyOptionImage();
                    
                    InstQuestManager.CheckClearQuests();
                    ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                    conversationText.text = ContactedNpcDataCurrentNode.Contents;
                    CreateOptionImage(ContactedNpcDataCurrentNode);

                    return;
                }
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
            
            if (ContactedNpcDataCurrentNode.Children[index].IsQuestEntry)
            {
                int questId = ContactedNpcStats.Id * NpcProfile.MaxQuestCount +
                              ContactedNpcDataCurrentNode.Children[index].QuestId;
                
                if (InstQuestManager.InstQuestData.QuestDataList[questId].DidClear)
                {
                    ContactedNpcDataCurrentNode.GetAward();
                    InstQuestManager.InstQuestData.QuestDataList[questId].GetAward();
                    DestroyOptionImage();
                    
                    InstQuestManager.CheckClearQuests();
                    ContactedNpcDataCurrentNode = ContactedNpcDataRootNode;
                    conversationText.text = ContactedNpcDataCurrentNode.Contents;
                    CreateOptionImage(ContactedNpcDataCurrentNode);

                    return;
                }
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
            questList.SetActive(true);
            isQuestListOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

            for (int i = 0; i < instQuestButtonList?.Count; i++)
            {
                var questButtonValue = instQuestButtonList[i].GetComponent<QuestDescriptionData>();
                
                if (InstQuestManager.InstQuestData.QuestDataList[questButtonValue.QuestId].DidGetAward)
                {
                    var questButtonText = instQuestButtonList[i].GetComponentInChildren<TextMeshProUGUI>();
                    questButtonText.color = Color.yellow;
                }
                else if (InstQuestManager.InstQuestData.QuestDataList[questButtonValue.QuestId].DidClear)
                {
                    var questButtonText = instQuestButtonList[i].GetComponentInChildren<TextMeshProUGUI>();
                    questButtonText.color = Color.green;
                }
            }
        }
        else
        {
            questList.SetActive(false);
            isQuestListOpen = false;
            questDescription.SetActive(false);
            isQuestDescriptionOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    private void OnGamePause()
    {
        if (!isGamePause)
        {
            gamePause.SetActive(true);
            Time.timeScale = 0f;
            isGamePause = true;
        }
        else
        {
            gamePause.SetActive(false);
            isGamePause = false;
            
            if (!isQuestListOpen)
            {
                Time.timeScale = 1f;
            }
        }
    }
}

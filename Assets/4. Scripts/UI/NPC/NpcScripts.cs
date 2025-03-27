using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NpcScripts : MonoBehaviour
{
    public InputAction moveAction;

    private NpcStats status;
    private DefaultScript defaultScript;
    private QuestManager questManager;
    private Talkable talkState;
    
    private GameObject scriptObject;
    private GameObject player;
    private GameObject questContent;
    
    private Button questButton;
    private TextMeshProUGUI playerQuestList;
    private TextMeshProUGUI testText;
    
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        
        talkState = GetComponent<Talkable>();
        scriptObject = GameObject.FindGameObjectWithTag("Script");
        
        playerQuestList = GameObject.Find("PlayerQuestList").GetComponentInChildren<TextMeshProUGUI>();
        testText = scriptObject.GetComponentInChildren<TextMeshProUGUI>();
        status = GetComponent<NpcStats>();
        
        defaultScript = new DefaultScript();
        defaultScript.next = scriptObject.GetComponentsInChildren<Button>()[0];
        defaultScript.prev = scriptObject.GetComponentsInChildren<Button>()[1];
        testText.text = questManager.questData.questScripts[status.Id * 1000 + (status.questCount + 1)];
        testText.pageToDisplay = 1;
        defaultScript.next.onClick.AddListener(OnNextPage);
        defaultScript.prev.onClick.AddListener(OnPrevPage);

        questContent = GameObject.Find("Quest");
        questButton = questContent.GetComponentInChildren<Button>();
        questButton.onClick.AddListener(OnRegisterQuest);
        
        scriptObject.SetActive(false);
        
        moveAction = InputSystem.actions.FindAction("Talk");
        moveAction.performed += OnOpen;
    }
     
    private void OnOpen(InputAction.CallbackContext context)
    {
        if (!talkState.canOpenScript)
            return;
        
        if (talkState.isOpenScript == false)
        {
            talkState.isOpenScript = true;
            scriptObject.SetActive(true);
            defaultScript.prev.enabled = false;
            transform.forward = (player.transform.position - transform.position).normalized;
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
        else
        {
            talkState.isOpenScript = false;
            scriptObject.SetActive(false);
        }
    }

    private void OnNextPage()
    {
        testText.pageToDisplay += 1;
        defaultScript.prev.enabled = true;
    }
    
    private void OnPrevPage()
    {
        if (testText.pageToDisplay == 1)
        {
            testText.pageToDisplay = 1;
            defaultScript.prev.enabled = false;
        }
        else
        {
            testText.pageToDisplay -= 1;   
        }
    }

    private void OnRegisterQuest()
    {
        questContent.SetActive(false);
        questManager.RegisterQuest(status.Id, ref status.questCount);
        playerQuestList.text += (questManager.questData.questDatas[status.Id * 1000 + status.questCount] + "\n" + "\n");
        testText.text = status.defaultScriptContents;
        testText.pageToDisplay = 1;
    }
}

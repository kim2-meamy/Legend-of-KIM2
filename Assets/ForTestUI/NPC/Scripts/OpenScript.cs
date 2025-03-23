using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenScript : MonoBehaviour
{
    private GameObject scrollView;
    private Talkable talkState;
    private InputAction moveAction;
    private GameObject player;
    
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        talkState = GetComponent<Talkable>();
        scrollView = GameObject.FindGameObjectWithTag("Script");
        scrollView.SetActive(false);
        
        moveAction = InputSystem.actions.FindAction("Talk");
        moveAction.performed += onOpen;
    }
    
    private void onOpen(InputAction.CallbackContext context)
    {
        if (talkState.canOpenScript == true)
        {
            if (talkState.isOpenScript == false)
            {
                talkState.isOpenScript = true;
                scrollView.SetActive(true);
                transform.forward = (player.transform.position - transform.position).normalized;
            }
            else
            {
                talkState.isOpenScript = false;
                scrollView.SetActive(false);
            }   
        }
    }
}

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlimeQuest : MonoBehaviour
{
    public List<GameObject> list;
    public GameObject player;
    public GameObject playerUIText;
    
    void OnTriggerEnter()
    {
        foreach(GameObject obj in list)
        {
            obj.GetComponent<Enemy>().target = player.transform;
        }
        Debug.Log("여기", playerUIText);
    }

    private void Update()
    {
        list.RemoveAll(item => item == null);;
        
        if (list.Count == 0)
        {
            playerUIText.SetActive(true);
        }
    }
}

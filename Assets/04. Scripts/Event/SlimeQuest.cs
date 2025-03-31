using System.Collections.Generic;
using UnityEngine;

public class SlimeQuest : MonoBehaviour
{
    public List<GameObject> list;
    public GameObject player;
    public GameObject playerUIText;
    
    void OnTriggerEnter(Collider other)
    {
        foreach(GameObject obj in list)
        {
            obj.GetComponent<Enemy>().target = player.transform;
        }
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

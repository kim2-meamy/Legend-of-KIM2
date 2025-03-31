using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject player;

    private CharacterController controller;

    private void Awake()
    {
        controller = player.GetComponent<CharacterController>();
    }
    
    public void Respawn()
    {
        int spawnIndex = 0;
        Transform spawnPoint = spawnPoints[spawnIndex];
        
        if (controller != null)
        {
            controller.enabled = false;
        }
        
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
        
        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}
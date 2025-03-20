using UnityEngine;

public class aa : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            // Player.Damaged(stats.damage);
            Debug.Log("Player Damaged");
        }
    }
}

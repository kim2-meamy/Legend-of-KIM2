using UnityEngine;

public class test : MonoBehaviour
{
    public Enemy enemy;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enemy.TakeDamage(10);
        }

    }
}

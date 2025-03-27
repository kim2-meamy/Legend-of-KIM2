using UnityEngine;

public class test : MonoBehaviour
{
    public Boss boss;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            boss.TakeDamage(10);
        }

    }
}

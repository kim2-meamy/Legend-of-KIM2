using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    private GameObject endUI;
    
    void Awake()
    {
        endUI = GameObject.FindGameObjectWithTag("End");
    }

    void OnReturn()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

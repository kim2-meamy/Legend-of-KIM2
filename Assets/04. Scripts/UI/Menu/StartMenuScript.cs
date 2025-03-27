using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private Button startButton, quitButton;
    
    private const string SceneName = "Medieval Cute Built In";

    private void Start()
    {
        startButton.onClick.AddListener(OnStart);
        quitButton.onClick.AddListener(OnQuit);
    }

    void OnStart()
    {
        SceneManager.LoadScene(SceneName);
    }

    void OnQuit()
    {
        Application.Quit();
    }
}

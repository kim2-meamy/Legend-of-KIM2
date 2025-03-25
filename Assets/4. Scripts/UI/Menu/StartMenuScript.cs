using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    private Button startButton, quitButton;
    void Start()
    {
        Button[] buttons = gameObject.GetComponentsInChildren<Button>();
        startButton = buttons[0];
        quitButton = buttons[1];
        
        startButton.onClick.AddListener(OnStart);
        quitButton.onClick.AddListener(OnQuit);
    }

    void OnStart()
    {
        SceneManager.LoadScene("Medieval Cute Built In");
    }

    void OnQuit()
    {
        Application.Quit();
    }
}

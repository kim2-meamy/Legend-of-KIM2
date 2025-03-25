using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    private GameObject endUI;
    private Button returnButton;
    void Awake()
    {
        endUI = GameObject.FindGameObjectWithTag("End");
        returnButton = endUI.GetComponentInChildren<Button>();
        returnButton.onClick.AddListener(OnReturn);
    }

    void OnReturn()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

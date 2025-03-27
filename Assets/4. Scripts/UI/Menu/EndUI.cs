using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public GameObject endUI;
    private Button returnButton;

    void Awake()
    {
        returnButton = endUI.GetComponentInChildren<Button>();
        returnButton.onClick.AddListener(OnReturn);
    }

    void OnReturn()
    {
        SceneManager.LoadScene("StartMenu");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuButton;

    private void Awake()
    {
        mainMenuButton.GetComponent<Button>().onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

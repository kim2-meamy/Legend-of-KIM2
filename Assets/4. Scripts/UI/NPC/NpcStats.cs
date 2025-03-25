using UnityEngine;
using UnityEngine.InputSystem;

public class NpcStats : MonoBehaviour
{
    public int Id = 1;
    public int questCount = 0;
    public string Name = "test";
    public int Hp = 100;
    public string defaultScriptContents = "111111111111111111111111111111111111111111111111111111111111111" +
                                          "222222222222222222222222222222222222222222222222222222222222222" +
                                          "333333333333333333333333333333333333333333333333333333333333333333" +
                                          "444444444444444444444444444444444444444444444444444444444444444444";
    private InputAction action;

    void Awake()
    {
        action = InputSystem.actions.FindAction("Hit");
        action.performed += OnAttacked;
    }

    void OnAttacked(InputAction.CallbackContext context)
    {
        Hp -= 10;
    }
}

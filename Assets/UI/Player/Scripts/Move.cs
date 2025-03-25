using UnityEngine;
using UnityEngine.InputSystem;
// task 1. InputSystem event 기반으로 써보기
// task 2. PlayerInput component 써보기

public class Move : MonoBehaviour
{
    private Rigidbody rigidBody;
    private InputAction moveAction;
    private float moveSpeed = 10.0f;
    
    void Start()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
        
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.performed += context => { };
    }
    
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        if (movement.sqrMagnitude <= 0.01f)
        {
            return;
        }
        
        rigidBody.MovePosition(transform.position + (moveSpeed * Time.deltaTime * movement));
    }
}

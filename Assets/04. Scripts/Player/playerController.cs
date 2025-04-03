using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDodging;
    public bool isDie;
    
    public PlayerManager playerManager;
    
    [Header("Stat")]
    public int hp = 100;
    public int damage = 10;

    [Header("References")] 
    private CharacterController controller; 
  
    [SerializeField] private Transform cameraReference; 
    [SerializeField] private Animator animator;
    
    [Header("Movement Settings")] 
    public float walkSpeed = 1f;
    [SerializeField] private float sprintspeed = 2f;
    [SerializeField] private float sprintTrasitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private AnimatorToHash animatorToHash;
    
    private float verticalVelocity;
    private float speed;
    
    [Header("Input")]
    private float moveInput;
    private float turnInput;
    
    
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
#if UNITY_EDITOR
        var gameWindow = UnityEditor.EditorWindow.GetWindow(typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.GameView"));
        gameWindow.Focus();
        gameWindow.SendEvent(new Event
        {
            button = 0,
            clickCount = 1,
            type = EventType.MouseDown,
            mousePosition = gameWindow.rootVisualElement.contentRect.center
        });
#endif
        
        controller = GetComponent<CharacterController>(); 
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
        animatorToHash = new AnimatorToHash();    
    }

    private void Update() 
    {
        InputManagement(); 
        
        Movement(); 
        Dodging(); 
        Attack();
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(3f);
        playerManager.Respawn();
        hp = 100;
        isDie = false;
    }
    
    private void Die()
    {
        animator.SetTrigger(animatorToHash.animDie);
        AudioManager.Instance.PlaySFX(SFXType.PlayerDie, 0.7f);
        StartCoroutine(DieCoroutine());
    }

    private void Attack()
    {
        animator.SetBool(animatorToHash.animAttack, Input.GetMouseButtonDown(0));
    }

    private void Dodging()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger(animatorToHash.animDodging);
        }
    }

    public void Hit(int damage)
    {
        if(isDodging)
            return;

        if (isDie)
            return;
        
        animator.SetTrigger(animatorToHash.animHit);
        AudioManager.Instance.PlaySFX(SFXType.PlayerHit, 0.5f);
        
        hp -= damage;
        if (hp <= 0)
        {
            Die();
            isDie = true;
        }
    }
    
    
    private void Movement() 
    {
        GroundMovement(); 
        Turn(); 
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        
        move = cameraReference.transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            speed = Mathf.Lerp(speed, sprintspeed, sprintTrasitSpeed * Time.deltaTime); 
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTrasitSpeed * Time.deltaTime); 
        }
        
        move *= speed; 
        
        move.y = VerticalForceCalculation(); 
        
        controller.Move(move * Time.deltaTime);

        animator.SetFloat(animatorToHash.animMoveSpeed, speed * Mathf.Max(Mathf.Abs(moveInput), Mathf.Abs(turnInput)));
    }

    private void Turn()
    {
        
        if(Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0) 
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
          
        
            currentLookDirection.y = 0;
            currentLookDirection.Normalize(); 

            if (currentLookDirection != Vector3.zero) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection); 
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed); 
            }
        }
    }

    private float VerticalForceCalculation()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        bool isGrounded = Physics.Raycast(rayOrigin, Vector3.down, 0.3f);
    
        if (isGrounded && verticalVelocity <= 0) 
        {
            verticalVelocity = -1f; 
            animator.SetBool(animatorToHash.animGrounded, true); 
        
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2f); 
                animator.SetTrigger(animatorToHash.animJump); 
                AudioManager.Instance.PlaySFX(SFXType.PlayerJump, 0.25f, 0.1f);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; 
            animator.SetBool(animatorToHash.animGrounded, false); 
        }

        return verticalVelocity; 
    }
        
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical"); 
        turnInput = Input.GetAxis("Horizontal"); 
    }
}

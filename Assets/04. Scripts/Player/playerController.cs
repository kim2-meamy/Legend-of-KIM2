using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDodging;
    
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

    private float verticalVelocity; 
    private float speed; 
    
    [Header("Animation")]  
    private int animMoveSpeed; 
    private int animJump;
    private int animGrounded;
    private int animDodging;
    private int animAttack;
    private int animHit;
    private int animDie;
    
    [Header("Input")] 
    private float moveInput; 
    private float turnInput; 
    
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>(); 
        SetupAnimator();
       
    }

    private void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        
    }

    private void Update() 
    {
        InputManagement(); 
        
        Movement(); 
        Dodging(); 
        Attack(); 
    
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }

    }

    private void Die()
    {
        animator.SetTrigger(animDie);
    }

    private void Attack()
    {
        animator.SetBool(animAttack, Input.GetMouseButton(0));

        //if (Input.GetMouseButtonDown(0))
        //{
        //    // meleeArea.enabled = false;
        //    StartCoroutine((AttackCoroutine()));
        //}
    }

    // public void AttackStart()
    // {
    //     meleeArea.enabled = true;
    //     alreadyAttack = true;
    // }
    //
    // public void AttackEnd()
    // {
    //     meleeArea.enabled = false;
    //     alreadyAttack = false;
    // }
    //
    private void Dodging()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger(animDodging);
        }
    }
//회피중이면
//hit 안되게
    public void Hit(int damage)
    {
        if(isDodging)
            return;
        
        animator.SetTrigger(animHit);
        hp -= damage;
        if (hp <= 0)
        {
            Die();
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
        
 
        animator.SetFloat(animMoveSpeed, speed * Mathf.Max(Mathf.Abs(moveInput), Mathf.Abs(turnInput)));
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
        if (controller.isGrounded) 
        {
            verticalVelocity = -1f; 
            animator.SetBool(animGrounded, true); 
            
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2f); 
                animator.SetTrigger(animJump); 
            }
        }
        else
        {
            
            verticalVelocity -= gravity * Time.deltaTime; 
            animator.SetBool(animGrounded, false); 
        }

        return verticalVelocity; 
    }

    private void SetupAnimator()
    {
        animMoveSpeed = Animator.StringToHash("moveSpeed");
        animJump = Animator.StringToHash("Jump");
        animGrounded = Animator.StringToHash("Grounded");
        animDodging = Animator.StringToHash("Dodging");
        animAttack = Animator.StringToHash(("Attack"));
        animHit = Animator.StringToHash("Hit");
        animDie = Animator.StringToHash("Hp");
    }
        
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical"); 
        turnInput = Input.GetAxis("Horizontal"); 
    }
 
}

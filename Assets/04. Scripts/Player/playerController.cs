using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isDodging;
    public Collider meleeArea;
    
    [Header("Stat")]
    public int hp = 100;
    public int damage = 10;

    [Header("References")] 
    private CharacterController controller; 
  
    [SerializeField] private Transform cameraReference; 
    [SerializeField] private Animator animator;
    
    [Header("Movement Settings")] 
    [SerializeField] private float walkSpeed = 1f;
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
        controller = GetComponent<CharacterController>(); 
        SetupAnimator();
       
    }

    private void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        
        controller = GetComponent<CharacterController>(); // 게임 개체에 연결된 캐릭터 컨트롤러 구성 요소를 가져오고 컨트롤러 변수에 할당
        animatorToHash = new AnimatorToHash();    
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
        animator.SetTrigger(animatorToHash.animDie);
    }

    private void Attack()
    {
        animator.SetBool(animatorToHash.animAttack, Input.GetMouseButton(0));
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
       
        animator.SetTrigger(animatorToHash.animHit);
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
        if (controller.isGrounded) 
        {
            verticalVelocity = -1f; 
            animator.SetBool(animatorToHash.animGrounded, true); // 캐릭터가 땅에 있을 때 grounded 부울을 true 로 설정
            
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2f); 
                animator.SetTrigger(animatorToHash.animJump); //플레이어가 점프를 하면 
            }
        }
        else
        {
            
            verticalVelocity -= gravity * Time.deltaTime; 
            animator.SetBool(animatorToHash.animGrounded, false); // 땅에 있지 않으면 false
        }

        return verticalVelocity; 
    }
        
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical"); 
        turnInput = Input.GetAxis("Horizontal"); 
    }
}

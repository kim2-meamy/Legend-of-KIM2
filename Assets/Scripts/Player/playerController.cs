using CartoonFX;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject damageEffect;
    public Collider collider;

    [Header("References")] 
    private CharacterController controller; // 캐릭터 컨트롤러에 대한 개인 변수를 정의 // 후에 캐릭터 컨트롤러 메서드 호출 가능
    [SerializeField] private Transform camera; // 카메라가 방향을 결정하는 데 사용되는 기본 카메라를 참조
    [SerializeField] private Animator animator;
    //private Rigidbody rg; // 선언
  
    

    [Header("Movement Settings")] 
    [SerializeField] private float walkSpeed = 5f; // 캐릭터의 이동 속도를 제어
    [SerializeField] private float sprintspeed = 10f;//캐릭터가 스프린트 하는 속도 
    [SerializeField] private float sprintTrasitSpeed = 5f; // 캐릭터가 달리는 속도로 얼마나 빨리 전환되는지를 결정하는 속도 
    [SerializeField] private float turningSpeed = 2f; // 카메라와 일치하도록 플레이어가 회전하는 속도 제어
    [SerializeField] private float gravity = 9.81f; // 중력이라는 새 float변수를 추가하여 사용자 지정 중력 구현 // 지구의 중력을 모방
    [SerializeField] private float jumpHeight = 2f; // 캐릭터가 얼마나 높이 점프할 지 결정
    [SerializeField] private float delay = 0.5f;

    private float verticalVelocity; // 수직 속도 변수 -> 캐릭터의 수직 이동 속도 추적 ( 중력에 필요 )
    private float speed; //이동 함수에서 현재 속도 값을 저장하기 위한 speed 변수 만듬
    
    [Header("Animation")]  //
    private int animMoveSpeed; 
    private int animJump;
    private int animGrounded;
    //회피 매개변수에 대한 정수 변수를 만들기
    private int animDodging;
    //공격모션
    private int animAttack;
    //피격모션
    private int animHit;
    //다이 모션
    private int animDie;
    
    
    [Header("Input")] // 입력값
    private float moveInput; // 플레이어의 앞뒤 이동
    private float turnInput; // 플레이어 회전값
    
    [Header("Stat")]
    private int hp =100;
    public int damage = 10;
    //나중에 값을 받아오면 hp=0이 되면 animDie실행되게 만들기

    [HideInInspector]
    public bool alreadyAttack = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>(); // 게임 개체에 연결된 캐릭터 컨트롤러 구성 요소를 가져오고 컨트롤러 변수에 할당
        SetupAnimator();
        
        //마우스
        Cursor.visible = false; // 마우스 커서를 숨김
        Cursor.lockState = CursorLockMode.Locked; // 마우스를 화면 중앙에 고정
    }

    private void Update() //입력이 매 프레임마다 확인되고 업데이트
    {
        InputManagement(); // update 메서드 내에서 입력 관리 함수를 호출 -> 캐릭터 움직임에 대한 응답적이고매끄러운 제어 가능
        
        Movement(); // 움직임 함수 호출 //이 안에 GroundMovement / Turn 함수 => 중복된 함수를 호출하는 것 같지만 이렇게 하면 업데이트와 유지 관리가 쉽다
        Dodging(); // 업데이트에서 이 함수를 호출
        Attack(); // attack 함수가 매 프레임마다 업데이트
        //Hit();
        //Die();
        
        //마우스 ESC키를 누르면 마우스를 다시 보이게 하고 잠금 해제 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 마우스 이동 가능 
        }
        
    }
    //시간 누적은 Time.delta
    //attack delay 시간 추가..
    

    private void FixedUpdate()
    {
        
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
        //    // collider.enabled = false;
        //    StartCoroutine((AttackCoroutine()));
        //}
    }

    public void AttackStart()
    {
        collider.enabled = true;
        alreadyAttack = true;
    }

    public void AttackEnd()
    {
        collider.enabled = false;
        alreadyAttack = false;
    }

    //IEnumerator AttackCoroutine()
    //{
    //    collider.enabled = true;
    //    yield return new WaitForSeconds(0.533f);
    //    collider.enabled = false;
    //}

    // private void AttackDelay()
    // {
    //     if (attackDelay>delay)
    //     {
    //         StartCoroutine(AttackDelayCoroutine());
    //     }
    //
    //     attackDelay = delay * Time.deltaTime;
    //     IEnumerator AttackDelayCoroutine()
    //     {
    //         yield return WaitForSeconds(attackDelay);
    //     }
    // }

    private void Dodging()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger(animDodging);
            StartCoroutine((DodgeCoroutine()));
        }
    }

    IEnumerator DodgeCoroutine()
    {
        walkSpeed = 20f;
        yield return new WaitForSeconds(0.5f);
        walkSpeed = 5f;
        
    }

    public void Hit(int damage)
    {
        animator.SetTrigger(animHit);
        damageEffect.GetComponent<CFXR_ParticleText>().UpdateText("-" + damage.ToString());
        damageEffect.GetComponent<ParticleSystem>().Play();
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    
    
    private void Movement() // Movement함수를 만들고 이 안에서 GroundMovement함수를 호출 -> 이 후 update로 가서 movement함수호출
    {
        GroundMovement(); // 지상 움직힘 함수 호출 -> update 메서드 내에서 함수 호출
        Turn(); // 이동 함수 내에서 회전 함수를 호출 
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);//옆으로 이동 turn 앞뒤로 이동은 moveInput
        
        move = camera.transform.TransformDirection(move);// 카메라의 로컬 축과 일치하도록 변경 
        
        //transform을 사용해서 move 벡터3를 플레이어의 로컬 축으로 변환 
        // 카메라 방향이 글로벌 방향 + 플레이어가 현재 바라보는 위치에 올바르게 정렬되도록 보장 //=> 이 후 turn();을 Movemnet();안에 추가
        // move = transform.TrasnformDirection(move); 삭제하기 -> 카메라의 방향은 이제 카메라 기준의 입력이 아닌 캐릭터의 속도에서 파생되기 때문에 //
        
        //4) 이전에 플레이어의 로컬 축으로 이동 입력을 변환한 지상 이동 함수는 카메라의 로컬축과 일치하도록 변경
        // move = transform.TrasnformDirection(move); ->move = camera.transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift)) // 왼쪽 shift키가 눌렸는지 확인 //13:00 // 왼쪽 shift 키는 달리기, 바꾸고 싶으면 유니티 설정에서 변경 
        {
            speed = Mathf.Lerp(speed, sprintspeed, sprintTrasitSpeed * Time.deltaTime); // 12:29
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTrasitSpeed * Time.deltaTime); //  키가 눌려있는 동안 속도가 관되게 증가// 선형보간 
        }
        
        move *= speed; //*=walkSpeed; //속도는 move 백터에 보행 속도를 곱하기
        
        move.y = VerticalForceCalculation(); // 이동을 수직 속도로 설정  //(처음에는 0으로 설정. 캐릭터가 위아래로 이동하지 않아야 하기 때문에)
        
        controller.Move(move * Time.deltaTime); // 백터에 보행 속도를 곱한 후 캐릭터를 움직이기 위한 컨트롤러 사용 * 이동 벡터를 시간에 곱함 // 델타 시간을 사용해 움직임이 부드럽고 프레임 속도에 독립적이도록 함
        
        //Animations
        //speed 변수를 사용하여 캐릭터가 달리고 있는지 걷는지 확인 -> 이 속도를 moveinput or turninput의 최대값에 곱함
        animator.SetFloat(animMoveSpeed, speed * Mathf.Max(Mathf.Abs(moveInput), Mathf.Abs(turnInput)));
    }

    private void Turn()
    {
        // 플레이어가 움직이는지 확인하는 if문으로 래핑 -> 정지해 있을 때 캐릭터의 회전을 방지
        if(Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0) // 움직임이 있을 때만 실행하도록 
        {
            Vector3 currentLookDirection = controller.velocity.normalized;
            //2)캐릭터의 시선 방향을 플레이어가 실제로 향하고 있는 위치로 다시 정의
            // 1)회전 함수의 현재 시선 방향을 camera.forward;에서 캐릭터의 정규화된 속도로 바꾸기 ->캐릭터가 현재 이동하고 있는 방향을 나타냄
        
            currentLookDirection.y = 0; // y값을 0으로 맞춰서 수평을 이루도록 함 

            currentLookDirection.Normalize(); //3) 캐릭터의 정규화된 속도로 표현 charator's normalized velocity

            if (currentLookDirection != Vector3.zero) // 캐릭터가 움직이는지 확인
            {
                Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection); // 카메라의 현재 look방향과 일치하는 새로운 회전 생성 -> 플레이어가 이 새로운 방향을 향하도록 부드럽게 회전
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed); // 부드러운 전환을 위해 회전 속도 적용
            }
        }
    }

    private float VerticalForceCalculation() // 중력 적용을 처리 10:42
    {
        if (controller.isGrounded) // 캐릭터 컨트롤러가 접지되었는지 확인
        {
            verticalVelocity = -1; // 캐릭터 속도를 작은 음수값으로 설정해서 캐릭터가 유지되도록 함
            animator.SetBool(animGrounded, true); // 캐릭터가 땅에 있을 때 grounded 부울을 true 로 설정
            
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2); // 점프 높이의 제곱근에 중력 * 2 // 원하는 점프 높이에 도달하는 데 필요한 초기 속도 게산 
                animator.SetTrigger(animJump); //플레이어가 점프를 하면 
            }
        }
        else
        {
            //사소한 부정확성으로 인해 떠다니지 않는 경우, 땅에 닿지 않았다면 캐릭터가 떨어지거나 점프하고 있다는 것을 의미하므로
            verticalVelocity -= gravity * Time.deltaTime; // 중력 값에 시간을 곱한 값을 뺴야함 // 낙하를 시뮬레이션한 후 
            animator.SetBool(animGrounded, false); // 땅에 있지 않으면 false
        }

        return verticalVelocity; // 수직 속도에 대한 델타 시간을 사용해 낙하를 시뮬레이션한 다음 수직속도를 반환하고 
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(
                new Vector3(controller.center.x, controller.center.y - controller.height, controller.center.z),
                -controller.transform.up, out RaycastHit hit, 2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
 
    private IEnumerator StartJump()
    {
        yield return new WaitForSeconds(0.5f);

        verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
    }

    private void SetupAnimator()
    {
        animMoveSpeed = Animator.StringToHash("moveSpeed");
        animJump = Animator.StringToHash("Jump");
        animGrounded = Animator.StringToHash("Grounded");
        //설정 애니메이터 함수에서 해시ID를 설정
        animDodging = Animator.StringToHash("Dodging");
        animAttack = Animator.StringToHash(("Attack"));
        animHit = Animator.StringToHash("Hit");
        animDie = Animator.StringToHash("Hp");
    }
        
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical"); // w 및 s키의 수직 입력을 추적
        turnInput = Input.GetAxis("Horizontal"); // a 및 d 키의 수평 입력을 캡처
    }


}

using System;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;

public class playerConstroll : MonoBehaviour
{
    [Header("References")] 
    private CharacterController controller; // 캐릭터 컨트롤러에 대한 개인 변수를 정의 // 후에 캐릭터 컨트롤러 메서드 호출 가능
    [SerializeField] private Transform camera; // 카메라가 방향을 결정하는 데 사용되는 기본 카메라를 참조
    

    [Header("Movement Settings")] 
    [SerializeField] private float walkSpeed = 5f; // 캐릭터의 이동 속도를 제어
    [SerializeField] private float sprintspeed = 10f;//캐릭터가 스프린트 하는 속도 
    [SerializeField] private float sprintTrasitSpeed = 5f; // 캐릭터가 달리는 속도로 얼마나 빨리 전환되는지를 결정하는 속도 
    [SerializeField] private float turningSpeed = 2f; // 카메라와 일치하도록 플레이이가 회전하는 속도 제어 
    [SerializeField] private float gravity = 0; //9.81f; // 중력이라는 새 float변수를 추가하여 사용자 지정 중력 구현 // 지구의 중력을 모방
    [SerializeField] private float jumpHeight = 2f; // 캐릭터가 얼마나 높이 점프할 지 결정

    private float verticalVelocity; // 숙직 속도 변수 -> 캐릭터의 수직 이동 속도 추적
    private float speed; //이동 함수에서 현재 속도 값을 저장하기 위한 speed 변수 만듬
    
    [Header("Input")] 
    private float moveInput; // 플레이어의 앞뒤 이동
    private float turnInput; // 플레이어 회전값
    
    private void Start()
    {
        controller = GetComponent<CharacterController>(); // 게임 개체에 연결된 캐릭터 컨트롤러 구성 요소를 가져오고 컨트롤러 변수에 할당
    }

    private void Update()
    {
        InputManagement(); // update 메서드 내에서 입력 관리 함수를 호출
        Movement(); // 움직임 함수 호출
    }

    private void Movement()
    {
        GroundMovement();
        Turn();
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = camera.transform.TransformDirection(move);// 카메라의 로컬 축과 일치하도록 변경 // 글로벌 방향 + 플레이어가 현재 바라보는 위치에 정렬되도록 보장
        // -> 카메라의 방향은 이제 카메라 기준의 입력이 아닌 속도에서 파생되기 때문에 //

        if (Input.GetKey(KeyCode.LeftShift)) // 왼쪽 shift키가 눌렸는지 확인 //13:00 // 왼쪽 shift 키는 달리기, 바꾸고 싶으면 유니티 설정에서 변경 
        {
            speed = Mathf.Lerp(speed, sprintspeed, sprintTrasitSpeed * Time.deltaTime); // 12:29
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTrasitSpeed * Time.deltaTime); //  키가 눌려있는 동안 속도가 관되게 증가
        }
        
        move *= speed;
        
        move.y = VerticalForceCalculation(); // 이동을 수직 속도로 설정 
        
        controller.Move(move * Time.deltaTime); // 백터에 보행 속도를 곱한 후 캐릭터를 움직이기 위한 컨트롤러 사용 * 이동 벡터를 시간에 곱함 // 델타 시간을 사용해 움지김이 부드럽고 프레임 속도에 독립적이도록 함
    }

    private void Turn()
    {
        if(Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0) // 플레이어가 움직이는지 확인하는 if문으로 래핑 -> 정지해 있을 때 캐릭터의 회전을 방지
        {
        Vector3 currentLookDirection = controller.velocity.normalized; // 시선 방향을 캐릭터가 실제로 향하고 있는 위치로 다시 정의// 캐릭터의 시선을 정규화된 속도로 바꿈 // 캐릭터가 현재 이동하고 있는 방향 //camera.forward; 
        currentLookDirection.y = 0; // y값을 0으로 맞춰서 수평을 이루도록 함 

        currentLookDirection.Normalize(); //캐릭터의 정규화된 속도로 표현 

        Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection); // 카메라의 현재 look방향과 일치하는 새로운 회전 생성 -> 플레이어가 이 새로운 방향을 향하도록 부드럽게 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed); // 부드러운 전환을 위해 회전 속도 적용
        }
    }

    private float VerticalForceCalculation() // 중력 적용을 처리
    {
        if (controller.isGrounded) // 캐릭터 컨트롤러가 접지되었는지 확인
        {
            verticalVelocity = 0;  //-1f; // 캐릭터 속도를 작은 음수값으로 설정해서 캐릭터가 유지되도록 함
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2); // 점프 높이의 제곱근에 중력 * 2 // 원하는 점프 높이에 도달하는 데 필요한 초기 속도 게산 
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime; // 중력 값에 시간을 곱한 값을 뺴야함 //10:42//
        }

        return verticalVelocity; // 수직 속도에 대한 델타 시간을 사용해 낙하를 시뮬레이션한 다음 수직속도를 반환하고 
    } 
        
    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical"); // w 및 s키의 수직 입력을 추적
        turnInput = Input.GetAxis("Horizontal"); // a 및 d 키의 수평 입력을 캡처
    }

}

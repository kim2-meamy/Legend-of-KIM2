using UnityEngine;

public class AutoView : MonoBehaviour
{
    private GameObject mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    private void Update()
    {
        // 카메라가 보고 있는 방향으로 world space UI의 rotation 값 변경
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
    }
}

using UnityEngine;

public class AutoView : MonoBehaviour
{
    private GameObject mainCamera;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    
    void Update()
    {
        // transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
        //     mainCamera.transform.rotation * Vector3.up);
        //
        // transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        transform.forward = -(mainCamera.transform.position - transform.position).normalized;
    }
}

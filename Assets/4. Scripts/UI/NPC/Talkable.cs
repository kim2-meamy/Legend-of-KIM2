using System.Linq;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SphereCollider sphereCollider;
    public TextMeshProUGUI text;
    public bool isOpenScript = false;
    public bool canOpenScript = false;

    private void ShowUI()
    {
        text.enabled = true;
    }
    
    void Start()
    {
        if (sphereCollider == null)
        {
            sphereCollider = GetComponent<SphereCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        canOpenScript = false;
        text.enabled = false;
        
        Collider[] collider = Physics.OverlapSphere(transform.position, sphereCollider.radius);
        foreach (var v in collider)
        {
            if (v.CompareTag("Player") == true)
            {
                canOpenScript = true;
                
                if (isOpenScript == false)
                {
                    ShowUI();
                }
                else
                {
                    text.enabled = false;
                }
            }
        }
    }
}

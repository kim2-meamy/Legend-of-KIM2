using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // �θ� ������Ʈ�� �ִ� PlayerController�� ĳ���մϴ�.
    private PlayerController parentController;

    private void Awake()
    {
        parentController = GetComponentInParent<PlayerController>();
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��Ǵ� �Լ�
    public void AttackStart()
    {
        if (parentController != null)
        {
            parentController.AttackStart();
        }
    }

    public void AttackEnd()
    {
        if (parentController != null)
        {
            parentController.AttackEnd();
        }
    }
}

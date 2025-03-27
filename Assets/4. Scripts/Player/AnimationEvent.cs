using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // 부모 오브젝트에 있는 playerController를 캐시합니다.
    private playerController parentController;

    private void Awake()
    {
        parentController = GetComponentInParent<playerController>();
    }

    // 애니메이션 이벤트로 호출되는 함수
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

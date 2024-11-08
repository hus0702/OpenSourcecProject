using System.Collections.Generic;
using UnityEngine;

public class ElectricPlatform : DangerousObject
{
    [SerializeField] private List<Animator> platformAnimators;

    public override void OnKeyInserted()
    {
        foreach(Animator animator in platformAnimators)
        {
            animator.SetBool("isInactive", true);
        } // 이게 제일 단순하긴 한데 만약 이걸로 동기화 문제가 발생한다면 개별 플랫폼마다 스크립트를 달아야 함.

        base.OnKeyInserted();
    }
}

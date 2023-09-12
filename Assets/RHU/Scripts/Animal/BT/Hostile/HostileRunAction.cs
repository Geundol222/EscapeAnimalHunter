using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileRunAction : ActionNode
{
    private float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    private bool IsTracking { get { return owner.isTracking; } }
    private bool IsWary { set { owner.isWary = value; } }

    public HostileRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (TrackingTime <= 15f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
                !IsTracking)
                animator.SetInteger("RandomRun", RandomRun());
            else
                animator.SetInteger("RandomRun", 0);

            TrackingTime += Time.deltaTime;
            
            return NodeState.Success;
        }

        TrackingTime = 0;                                       // 15f ���� Player �� ã������ IsWary = false
        animator.SetBool("IsWary", IsWary = false);

        return NodeState.Failure;
    }

    private int RandomRun()
    {
        RandomAction(8);

        if (random <= 1)
            return 1;
        else if (2 <= random && random <= 3)
            return 2;

        return 0;                               // Animator���� 1 == Left Run, 2 == Right Run, �������� run ��� ���
    }
}

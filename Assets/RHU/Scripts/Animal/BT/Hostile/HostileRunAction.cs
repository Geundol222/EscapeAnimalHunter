using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileRunAction : ActionNode
{
    private float WaryTime { get { return owner.waryTime; } set { owner.waryTime = value; } }
    private bool IsTracking { get { return owner.isTracking; } }
    private bool IsWary { set { owner.isWary = value; } }

    public HostileRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (WaryTime <= 15f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
                !IsTracking)
                animator.SetInteger("RandomRun", RandomRun());
            else
                animator.SetInteger("RandomRun", 0);

            WaryTime += Time.deltaTime;
            
            return NodeState.Success;
        }

        WaryTime = 0;                                       // 15f 동안 Player 못 찾았을때 IsWary = false
        animator.SetBool("IsWary", IsWary = false);

        return NodeState.Failure;
    }

    private int RandomRun()
    {
        RandomAction(6);

        if (random <= 1)
            return 1;
        else if (2 == random || random == 3)
            return 2;

        return 0;                               // Animator에서 1 == Left Run, 2 == Right Run, 나머지는 run 계속 재생
    }
}

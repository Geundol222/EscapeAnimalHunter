using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAction : ActionNode
{
    private float WaryTime { get { return owner.waryTime; } set { owner.waryTime = value; } }
    private bool IsTracking { get { return owner.isTracking; } }
    private bool IsWary { set { owner.isWary = value; } }

    public RunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (WaryTime <= 20f)
        {
            if (curAnimationCheck("Run") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f &&
                !IsTracking)
                animator.SetInteger("RandomRun", RandomAction(6));
            else
                animator.SetInteger("RandomRun", 0);

            WaryTime += Time.deltaTime;
            
            return NodeState.Success;
        }

        WaryTime = 0;
        animator.SetBool("IsWary", IsWary = false);

        return NodeState.Failure;
    }
}

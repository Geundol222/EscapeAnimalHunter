using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetAwayRunAction : ActionNode
{
    private float WaryTime { get { return owner.waryTime; } set { owner.waryTime = value; } }
    private bool IsWary { set { owner.isWary = value; } }

    public GetAwayRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        animator.SetInteger("RandomRun", 0);

        if (WaryTime <= 20)
            WaryTime += Time.deltaTime;
        else
        {
            WaryTime = 0;
            animator.SetBool("IsWary", IsWary = false);

            return NodeState.Failure;
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            animator.SetInteger("RandomRun", RandomAction(6));

        return NodeState.Success;
    }
}

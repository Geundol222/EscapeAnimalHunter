using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetAwayAction : ActionNode
{
    private float WaryTime { get { return owner.waryTime; } set { owner.waryTime = value; } }
    private bool IsWary { get { return owner.isWary; } set { owner.isWary = value; } }

    public GetAwayAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if(IsWary)
        {
            if (WaryTime >= 20)
            {
                IsWary = false;
                WaryTime = 0;

                return NodeState.Failure;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                animator.SetInteger("RandomRun", RandomAction(5));
            else
                animator.SetBool("IsWary", IsWary);

            WaryTime += Time.deltaTime;

            return NodeState.Running;
        }

        WaryTime = 0;

        return NodeState.Failure;
    }
}

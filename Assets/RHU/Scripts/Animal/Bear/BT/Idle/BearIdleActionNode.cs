using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleActionNode : ActionNode
{
    public BearIdleActionNode(Bear owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle State") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            RandomAction(10);

            if (random == 0)
                animator.SetTrigger("IsDig");
            else if (random == 1)
                animator.SetTrigger("IsLook");
            else if (random == 2)
                animator.SetTrigger("IsSmell");
            else if (3 <= random && random <= 5)
            {
                animator.SetTrigger("IsWalk");

                return NodeState.Failure;
            }

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

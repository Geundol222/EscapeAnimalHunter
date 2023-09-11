using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActionNode : ActionNode
{
    public IdleActionNode(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (RandomAction(10) >= 3)
            animator.SetInteger("RandomIdle", random);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle State") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {

        }

        return NodeState.Success;
    }
}

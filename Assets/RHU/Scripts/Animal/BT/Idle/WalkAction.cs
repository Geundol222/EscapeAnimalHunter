using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : ActionNode
{
    public WalkAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Bear") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            animator.SetInteger("RandomWalk", RandomAction(6));

        return NodeState.Success;
    }
}

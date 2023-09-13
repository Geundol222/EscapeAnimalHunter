using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCondition : ActionNode
{
    public IdleCondition(Animal owner) : base(owner)
    {
    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            return NodeState.Success;

        return NodeState.Failure;
    }

    private bool CurAnimationName(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class BearHitAction : ActionNode
{
    public bool isHit;

    public BearHitAction(Animator animator, bool isHit) : base(animator)
    {
        this.isHit = isHit;
    }

    public override NodeState Evaluate()
    {
        if (isHit)
        {
            animator.SetTrigger("isHit");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

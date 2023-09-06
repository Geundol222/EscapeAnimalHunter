using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearLookAction : ActionNode
{
    public BearLookAction(Animator animator) : base(animator)
    {

    }

    public override NodeState Evaluate()
    {
        if (RandomAction())
        {
            animator.SetTrigger("IsLook");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

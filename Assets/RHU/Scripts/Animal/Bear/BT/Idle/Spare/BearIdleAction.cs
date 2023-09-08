using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleAction : ActionNode
{
    public BearIdleAction(Animal owner) : base(owner)
    {
        
    }

    public override NodeState Evaluate()
    {
        //if (RandomAction())
        //{
        //    animator.SetTrigger("IsIdle");

        //    return NodeState.Success;
        //}

        //return NodeState.Failure;

        animator.SetTrigger("IsIdle");

        return NodeState.Success;
    }
}

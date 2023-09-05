using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.RandomAction())
        {
            btBase.animator.SetTrigger("IsIdle");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

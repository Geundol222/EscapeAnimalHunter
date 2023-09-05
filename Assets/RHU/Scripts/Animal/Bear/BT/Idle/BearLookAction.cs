using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearLookAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.RandomAction())
        {
            btBase.animator.SetTrigger("IsLook");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

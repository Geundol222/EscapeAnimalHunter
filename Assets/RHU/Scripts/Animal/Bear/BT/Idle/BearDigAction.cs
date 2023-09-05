using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDigAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.RandomAction())
        {
            btBase.animator.SetTrigger("IsDig");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

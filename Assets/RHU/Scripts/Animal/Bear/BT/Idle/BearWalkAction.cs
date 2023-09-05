using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearWalkAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.RandomAction())
        {
            btBase.animator.SetTrigger("IsWalk");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

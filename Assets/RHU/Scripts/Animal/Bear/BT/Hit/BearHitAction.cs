using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class BearHitAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.isHit)
        {
            btBase.animator.SetTrigger("isHit");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSmellAction : ActionNode
{
    public override NodeState Evaluate()
    {
        if (btBase.RandomAction())
        {
            btBase.animator.SetTrigger("IsSmell");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearWalkAction : ActionNode
{
    public BearWalkAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (RandomAction())
        {
            animator.SetTrigger("IsWalk");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

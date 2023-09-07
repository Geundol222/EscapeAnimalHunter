using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearLookAction : ActionNode
{
    public BearLookAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        //if (RandomAction())
        //{
        //    animator.SetTrigger("IsLook");

        //    return NodeState.Success;
        //}

        return NodeState.Failure;
    }
}

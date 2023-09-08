using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDigAction : ActionNode
{
    public BearDigAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        //if (RandomAction())
        //{
        //    animator.SetTrigger("IsDig");
        //    return NodeState.Success;
        //}

        return NodeState.Failure;
    }
}

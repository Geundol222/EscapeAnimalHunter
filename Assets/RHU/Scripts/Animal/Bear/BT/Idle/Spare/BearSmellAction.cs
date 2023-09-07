using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSmellAction : ActionNode
{
    public BearSmellAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        //if (RandomAction())
        //{
        //    animator.SetTrigger("IsSmell");
        //    return NodeState.Success;
        //}
        return NodeState.Failure;
    }
}

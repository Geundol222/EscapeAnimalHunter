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
        if (RandomAction())
        {
            Debug.Log("smell is Success");
            animator.SetTrigger("IsSmell");
            return NodeState.Success;
        }
        Debug.Log("smell is Failure");
        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDigAction : ActionNode
{
    public BearDigAction(Animator animator) : base(animator)
    {

    }

    public override NodeState Evaluate()
    {
        if (RandomAction())
        {
            Debug.Log("dig is Success");
            animator.SetTrigger("IsDig");
            return NodeState.Success;
        }
        Debug.Log("dig is Success");
        return NodeState.Failure;
    }
}

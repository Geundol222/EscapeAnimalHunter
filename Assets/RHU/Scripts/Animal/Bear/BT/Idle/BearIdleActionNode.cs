using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleActionNode : ActionNode
{
    public BearIdleActionNode(Animator animator) : base(animator)
    {

    }

    public override NodeState Evaluate()
    {
        RandomAction();

        switch (random)
        {
            case 0:
                animator.SetTrigger("IsDig");
                break;
            case 1:
                animator.SetTrigger("IsLook");
                break;
            case 2:
                animator.SetTrigger("IsSmell");
                break;
            case 3 | 4 | 5:
                animator.SetTrigger("IsWalk");
                break;
            default:
                break;
        }

        return NodeState.Running;
    }

}

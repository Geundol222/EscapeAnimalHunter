using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleActionNode : ActionNode
{
    public BearIdleActionNode(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        RandomAction();

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
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

                default:        // Idle Animation Ãâ·Â
                    break;
            }

            return NodeState.Running;
        }

        return NodeState.Failure;
    }

}

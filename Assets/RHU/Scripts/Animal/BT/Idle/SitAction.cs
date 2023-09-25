using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAction : ActionNode
{
    private bool IsSit { get { return owner.isSit; } set { owner.isSit = value; } }

    public SitAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (!IsSit)
            animator.SetBool("IsSit", IsSit = true);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f || animator.IsInTransition(0))
        {
            RandomAction(10);

            if (random == 9)
                animator.SetBool("IsSit", IsSit = false);
            else if (random == 1)
                animator.SetInteger("RandomIdle", random);
        }

        return NodeState.Success;
    }
}

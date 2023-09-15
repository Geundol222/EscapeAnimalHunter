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

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            RandomAction(4);

            if (random == 0)
                animator.SetBool("IsSit", IsSit = false);
        }

        return NodeState.Success;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDieAction : ActionNode
{
    private bool IsHit { get { return owner.isHit; } }
    private bool IsDie { get { return owner.isDie; } set { owner.isDie = value; } }

    public BearDieAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHit)
        {
            if (IsDie)
                return NodeState.Running;

            animator.SetBool("IsDie", IsDie = true);

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

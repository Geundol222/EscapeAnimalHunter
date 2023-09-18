using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAction : ActionNode
{
    protected bool IsHit { get { return owner.isHit; } }
    protected bool IsDie { get { return owner.isDie; } set { owner.isDie = value; } }

    public DieAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHit)
        {
            if (IsDie)
                return NodeState.Running;

            if (curAnimationCheck("Run") &&
                curAnimationCheck("Jump Run") &&
                curAnimationCheck("Run Left") &&
                curAnimationCheck("Run Right")
                )
                animator.SetInteger("RandomDie", 2);
            else
                animator.SetInteger("RandomDie", RandomAction(2));

            animator.SetTrigger("IsDie");
            IsDie = true;

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Run_Moose") &&
                animator.GetCurrentAnimatorStateInfo(0).IsName("Run Left_Moose") &&
                animator.GetCurrentAnimatorStateInfo(0).IsName("Run Right_Moose")
                )
                animator.SetInteger("RandomDie", 2);
            else
                animator.SetInteger("RandomDie", RandomAction(2));

            IsDie = true;

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

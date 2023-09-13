using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RunToDieAction : DieAction
{
    private bool IsWary { get { return owner.isWary; } }

    public RunToDieAction(Animal owner) : base(owner)
    {
    }

    public override NodeState Evaluate()
    {
        if (IsHit)
        {
            if (IsDie)
                return NodeState.Running;

            if (!IsWary)
                animator.SetInteger("RandomDie", RandomAction(1));
                
            animator.SetBool("IsDie", IsDie = true);

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

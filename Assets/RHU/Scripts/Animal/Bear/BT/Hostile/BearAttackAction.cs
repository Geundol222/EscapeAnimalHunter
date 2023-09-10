using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackAction : ActionNode
{
    private FieldOfView fieldOfView { get { return owner.fieldOfView; } }

    public BearAttackAction(Bear owner) : base(owner)
    {
        
    }

    public override NodeState Evaluate()
    {
        if (fieldOfView.AttackFOV())
        {
            animator.SetTrigger("IsAttack");

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Bear"))
                animator.SetInteger("RandomAttack", RandomAction(4));

            return NodeState.Success;
        }
        
        return NodeState.Failure;
    }
}

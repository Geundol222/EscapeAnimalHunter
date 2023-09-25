using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWhileSitAction : ActionNode
{
    private int CurHp { get { return owner.curHp ; } }
    private bool IsHit { get { return owner.isHit; } }
    private bool IsWary { set { owner.isWary = value; } }
    private bool IsSit { get { return owner.isSit; } set { owner.isSit = value; } }

    public HitWhileSitAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHit && IsSit && CurHp >= 1)
        {
            animator.SetTrigger("IsHit");
            animator.SetBool("IsWary", IsWary = true);
            animator.SetBool("IsSit", IsSit = false);

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

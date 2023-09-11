using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAction : ActionNode
{
    private int CurHp { get { return owner.curHp; } }
    private bool IsHit { get { return owner.isHit; } }
    private bool IsWary { set { owner.isWary = value; } }

    public HitAction(Animal owner) : base(owner)
    {
        
    }

    public override NodeState Evaluate()
    {
        if (IsHit && CurHp >= 1)
        {
            animator.SetTrigger("IsHit");
            animator.SetBool("IsWary", IsWary = true);

            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}

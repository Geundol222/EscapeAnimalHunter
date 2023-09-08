using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHitAction : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public bool IsHit { get { return owner.isHit; } set { owner.isHit = value; } }
    public bool IsHostile { get { return owner.isHostile; } set { owner.isHostile = value; } }

    public BearHitAction(Animal owner) : base(owner)
    {
        
    }

    public override NodeState Evaluate()
    {
        if (IsHit && CurHp >= 1)
        {
            animator.SetTrigger("IsHit");
            animator.SetBool("IsHostile", IsHostile = true);

            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHitAction : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public bool IsHit { get { return owner.isHit; } set { owner.isHit = value; } }
    public bool IsTracking { get { return owner.isTracking; } set { owner.isTracking = value; } }

    public BearHitAction(Animal owner) : base(owner)
    {
        
    }

    public override NodeState Evaluate()
    {
        if (IsHit && CurHp >= 1)
        {
            animator.SetTrigger("IsHit");
            animator.SetBool("IsTracking", IsTracking = true);

            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}

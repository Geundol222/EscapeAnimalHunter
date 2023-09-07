using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUnconsciousAction : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public bool IsHit { get { return owner.isHit; } set { owner.isHit = value; } }
    public bool IsUnconscious { get { return owner.isUnconscious; } set { owner.isUnconscious = value; } }

    //public BearUnconsciousAction(Animator animator, int curHp, bool isHit, bool isUnconscious) : base(animator)
    //{
    //    this.curHp = curHp;
    //    this.isHit = isHit;
    //    this.isUnconscious = isUnconscious;
    //}

    public BearUnconsciousAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHit)
        {
            if (IsUnconscious)
                return NodeState.Success;

            animator.SetTrigger("IsUnconscious");
            IsUnconscious = true;

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

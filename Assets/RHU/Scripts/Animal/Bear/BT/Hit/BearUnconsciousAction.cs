using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUnconsciousAction : ActionNode
{
    private bool isHit;
    private bool isUnconscious;
    private int curHp;

    public BearUnconsciousAction(Animator animator, bool isHit ,bool isUnconscious, int curHp) : base(animator)
    {
        this.isHit = isHit;
        this.isUnconscious = isUnconscious;
        this.curHp = curHp;
    }

    public override NodeState Evaluate()
    {
        if (curHp <= 0 && isHit)
        {
            if (isUnconscious)
                return NodeState.Success;

            animator.SetTrigger("IsUnconscious");
            isUnconscious = true;

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

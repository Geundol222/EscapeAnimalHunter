using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class BearHitAction : ActionNode
{
    public int curHp;
    public bool isHit;

    public BearHitAction(Animator animator, bool isHit, int curHp) : base(animator)
    {
        this.isHit = isHit;
        this.curHp = curHp;
    }

    public override NodeState Evaluate()
    {
        if (isHit && curHp >= 1)
        {
            animator.SetTrigger("IsHit");

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

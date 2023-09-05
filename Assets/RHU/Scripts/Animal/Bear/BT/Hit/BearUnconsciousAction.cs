using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearUnconsciousAction : ActionNode
{
    private bool isUnconscious;

    private void Awake()
    {
        isUnconscious = false;
    }

    public override NodeState Evaluate()
    {
        if (btBase.curHp <= 0 && btBase.isHit)
        {
            if (isUnconscious)
                return NodeState.Success;

            btBase.animator.SetTrigger("IsUnconscious");
            isUnconscious = true;

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

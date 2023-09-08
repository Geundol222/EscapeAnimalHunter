using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHostileCondition : ActionNode
{
    private bool IsHostile { get { return owner.isHostile; } }

    public BearHostileCondition(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHostile)
            return NodeState.Success;

        return NodeState.Failure;
    }
}

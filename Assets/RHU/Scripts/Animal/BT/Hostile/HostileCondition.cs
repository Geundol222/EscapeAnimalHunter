using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileCondition : ActionNode
{
    private bool IsHostile { get { return owner.isHostile; } }

    public HostileCondition(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHostile)
            return NodeState.Success;

        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileCondition : ActionNode
{
    private bool IsWary { get { return owner.isWary; } }

    public HostileCondition(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsWary)
            return NodeState.Success;

        return NodeState.Failure;
    }
}

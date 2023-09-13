using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetAwayCondition : ActionNode
{
    private bool IsWary { get { return owner.isWary; } }

    public GetAwayCondition (Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsWary)
            return NodeState.Success;

        return NodeState.Failure;
    }
}

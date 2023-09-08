using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearCheckFieldOfViewCondition : ActionNode
{
    private FieldOfView filedOfView;

    public BearCheckFieldOfViewCondition(Animal owner) : base(owner)
    {
        filedOfView = owner.GetComponentInChildren<FieldOfView>();
    }

    public override NodeState Evaluate()
    {
        if (filedOfView.FindPlayer())
            return NodeState.Success;

        return NodeState.Failure;
    }
}

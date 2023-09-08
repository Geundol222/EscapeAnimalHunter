using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearCheckFieldOfViewCondition : ActionNode
{
    private FieldOfView fieldOfView { get { return owner.fieldOfView; } }
    private bool IsTracking { set { owner.isTracking = value; } }

    public BearCheckFieldOfViewCondition(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (fieldOfView.FindPlayer())
        {

            return NodeState.Success;
        }

        IsTracking = false;

        return NodeState.Failure;
    }
}

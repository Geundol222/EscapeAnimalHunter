using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFieldOfViewCondition : ActionNode
{
    private FieldOfView fieldOfView { get { return owner.fieldOfView; } }
    private bool IsTracking { set { owner.isTracking = value; } }

    public CheckFieldOfViewCondition(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (fieldOfView.TrackingFOV())
        {

            return NodeState.Success;
        }

        IsTracking = false;

        return NodeState.Failure;
    }
}

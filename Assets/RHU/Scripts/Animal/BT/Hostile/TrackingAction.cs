using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAction : ActionNode
{
    private FieldOfView fieldOfView { get { return owner.fieldOfView; } }
    private float TrackingTime { set { owner.trackingTime = value; } }
    private bool IsTracking { set { owner.isTracking = value; } }

    public TrackingAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        TrackingTime = 0;

        if (fieldOfView.TrackingFOV())
        {
            IsTracking = true;
            owner.transform.LookAt(fieldOfView.playerTransform);

            return NodeState.Success;
        }

        return NodeState.Failure;
    }
}

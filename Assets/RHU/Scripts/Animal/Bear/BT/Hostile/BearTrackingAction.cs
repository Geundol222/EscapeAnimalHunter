using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrackingAction : ActionNode
{
    public float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    public bool IsTracking { get { return owner.isTracking; } set { owner.isTracking = value; } }
    private FieldOfView fieldOfView;

    public BearTrackingAction(Animal owner) : base(owner)
    {
        fieldOfView = owner.GetComponentInChildren<FieldOfView>();
    }

    public override NodeState Evaluate()
    {
        TrackingTime = 0;

        if (fieldOfView.FindPlayer())
        {
            IsTracking = true;
            owner.transform.LookAt(fieldOfView.playerTransform);

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

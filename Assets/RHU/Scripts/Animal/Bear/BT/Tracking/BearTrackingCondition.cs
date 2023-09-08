using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrackingCondition : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    public bool IsTracking { get { return owner.isTracking; } set { owner.isTracking = value; } }
    private int hpBeforeHit;

    public BearTrackingCondition(Animal owner) : base(owner)
    {
        hpBeforeHit = owner.curHp;
    }

    public override NodeState Evaluate()
    {
        if (IsTracking)
            return NodeState.Success;

        return NodeState.Failure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHostileCondition : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    public bool IsHostile { get { return owner.isHostile; } set { owner.isHostile = value; } }

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

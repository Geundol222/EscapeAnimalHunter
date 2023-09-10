using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearRunAction : ActionNode
{
    private float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    private bool IsTracking { get { return owner.isTracking; } }
    private bool IsHostile { set { owner.isHostile = value; } }

    public BearRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (TrackingTime <= 15f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Bear") && !IsTracking)
                animator.SetInteger("RandomRun", RandomRun());
            
            TrackingTime += Time.deltaTime;
            
            return NodeState.Success;
        }

        TrackingTime = 0;
        animator.SetBool("IsHostile", IsHostile = false);

        return NodeState.Failure;
    }

    private int RandomRun()
    {
        RandomAction(8);

        if (random <= 1)
            return 1;
        else if (2 <= random && random <= 3)
            return 2;

        return 0;                               // Animator에서 1 == Left Run, 2 == Right Run, 나머지는 run 계속 재생
    }
}

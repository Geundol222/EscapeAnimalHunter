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
            random = 8;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Bear") && !IsTracking)
                animator.SetInteger("RandomRun", RandomRun());      // Animator에서 0 == Left Run, 1 == Right Run, 나머지는 run 계속 재생
            
            TrackingTime += Time.deltaTime;
            
            return NodeState.Success;
        }

        TrackingTime = 0;
        animator.SetBool("IsHostile", IsHostile = false);

        return NodeState.Failure;
    }

    private int RandomRun()
    {
        RandomAction(9);

        if (random <= 1)
            return 0;
        else if (2 <= random && random <= 3)
            return 1;

        return random;
    }
}

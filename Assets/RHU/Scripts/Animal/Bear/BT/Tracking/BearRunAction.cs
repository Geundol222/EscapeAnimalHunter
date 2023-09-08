using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearRunAction : ActionNode
{
    public float TrackingTime { get { return owner.trackingTime; } set { owner.trackingTime = value; } }
    public bool IsTracking { get { return owner.isTracking; } set { owner.isTracking = value; } }

    public BearRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (TrackingTime <= 1f)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run_Bear"))
                animator.SetInteger("RandomRun", RandomRun());      // Animator에서 0 == Left Run, 1 == Right Run, 나머지는 run 계속 재생
            
            TrackingTime += Time.deltaTime;
            Debug.Log($"TrackingTme : {TrackingTime}");
            
            return NodeState.Running;
        }

        TrackingTime = 0;
        animator.SetBool("IsTracking", IsTracking = false);
        Debug.Log("Run false");
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

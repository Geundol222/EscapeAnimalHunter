using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearWalkAction : ActionNode
{
    public BearWalkAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk_Bear"))
        {
            animator.SetInteger("RandomWalk", RandomWalk());    // Animator에서 0 == Left Walk, 1 == Right Walk, 2 == IdleState로 이동, 나머지는 Walk 계속 재생

            return NodeState.Running;
        }

        return NodeState.Failure;
    }

    private int RandomWalk() 
    {
        RandomAction(6);

        if (random == 2 || random == 3)
            return 2;

        return random;
    }
}

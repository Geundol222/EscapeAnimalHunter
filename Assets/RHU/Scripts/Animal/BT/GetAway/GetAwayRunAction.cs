using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetAwayRunAction : ActionNode
{
    private float WaryTime { get { return owner.waryTime; } set { owner.waryTime = value; } }
    private bool IsWary { set { owner.isWary = value; } }

    public GetAwayRunAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (WaryTime <= 20f)
            WaryTime += Time.deltaTime;
        else
        {
            WaryTime = 0;
            animator.SetBool("IsWary", IsWary = false);

            return NodeState.Failure;
        }
        Debug.Log("Test1");

        if (curAnimationCheck("Run") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            animator.SetInteger("RandomRun", RandomAction(6));
            Debug.Log("Test2");
        }
        Debug.Log("Test3");

        return NodeState.Success;
    }
}

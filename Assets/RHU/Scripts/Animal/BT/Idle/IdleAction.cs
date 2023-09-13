using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction: ActionNode
{
    private bool IsWary { get { return owner.isWary; } }

    public IdleAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !IsWary)     // 각 Animation은 Has Exit Time 걸려있어서 불필요한 연산 방지, 살짝 어긋남이 있긴 한데 감수할 정도임
        {                                                                                   // .normalizedTime >= 1f 하면 제대로 동작 안함, Transition 넘어가는 사이에 normalizedTime 값이 초기화되서 안되는걸로 추정
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.SetInteger("RandomWalk", RandomAction(5));
            else
                animator.SetInteger("RandomIdle", RandomAction(10));
        }

        return NodeState.Success;
    }
}

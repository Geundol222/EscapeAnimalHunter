using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearIdleActionNode : ActionNode
{
    public BearIdleActionNode(Bear owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle State") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            RandomAction(10);

            if (random == 0)
                animator.SetTrigger("IsDig");
            else if (random == 1)
                animator.SetTrigger("IsLook");
            else if (random == 2)
                animator.SetTrigger("IsSmell");
            else if (3 <= random && random <= 5)
            {
                animator.SetTrigger("IsWalk");

                return NodeState.Failure;
            }

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
    //switch (RandomAction(10))     // 연산은 상술한 if문과 동일, 성능은 크게 차이 없지만 if문이 약간 우위, 가독성은 switch문이 더 좋은 것 같음,
    //{                             // 취사선택 필요
    //    case 0:
    //        animator.SetTrigger("IsDig");
    //        break;

    //    case 1:
    //        animator.SetTrigger("IsLook");
    //        break;

    //    case 2:
    //        animator.SetTrigger("IsSmell");
    //        break;

    //    case 3 | 4 | 5:
    //        //animator.SetTrigger("IsWalk");
    //        return NodeState.Failure;       // WalkAction 계산

    //    default:        // Idle Animation 출력
    //        break;
    //}
}

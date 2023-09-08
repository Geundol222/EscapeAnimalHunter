using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttackAction : ActionNode
{
    public BearAttackAction(Bear owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        RandomAction(4);

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)   // TODO : 공격 사거리 내에 플레이어가 들어오면 실행하도록 교체 필요
        {
            switch (random)
            {
                case 0:
                    animator.SetTrigger("IsBiteLeft");
                    break;

                case 1:
                    animator.SetTrigger("IsBiteRight");
                    break;

                case 2:
                    animator.SetTrigger("IsPawLeft");
                    break;

                case 3:
                    animator.SetTrigger("IsPawRight");
                    break;
            }

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}

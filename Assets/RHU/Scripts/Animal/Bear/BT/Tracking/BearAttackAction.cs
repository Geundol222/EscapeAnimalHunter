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

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)   // TODO : ���� ��Ÿ� ���� �÷��̾ ������ �����ϵ��� ��ü �ʿ�
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

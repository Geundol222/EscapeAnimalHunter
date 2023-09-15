using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction: ActionNode
{
    private bool IsWary { get { return owner.isWary; } }
    private bool IsSit { get { return owner.isSit; } set { owner.isSit = value; } }

    public IdleAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsSit)
            return NodeState.Failure;

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && !IsWary)   // �� Animation�� Has Exit Time �ɷ��־ ���ʿ��� ���� ����, ��¦ ��߳��� �ֱ� �ѵ� ������ ������
        {                                                                                           // .normalizedTime >= 1f �ϸ� ����� ���� ����, Transition �Ѿ�� ���̿� normalizedTime ���� �ʱ�ȭ�Ǽ� �ȵǴ°ɷ� ����
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                animator.SetInteger("RandomWalk", RandomAction(5));
            else
            {
                RandomAction(10);

                if (random == 9)
                    return NodeState.Failure;

                animator.SetInteger("RandomIdle", random);
            }
        }

        return NodeState.Success;
    }
}

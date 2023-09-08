using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearCheckFieldOfViewCondition : ActionNode
{
    public bool IsTracking { get { return owner.isTracking; } set { owner.isTracking = value; } }
    private FieldOfView fieldOfView;

    public BearCheckFieldOfViewCondition(Animal owner) : base(owner)
    {
        //filedOfView = new FieldOfView();
        fieldOfView = owner.GetComponentInChildren<FieldOfView>();      // TODO : �ϼ��ǰ� range, angle�� ��� ��������
                                                                        // GetComponent�� �������� ���ϰ� �ణ �����ϱ�
                                                                        // GetComponent ��� FieldOfVeiw ��ü�� ������ �ִ°� ������ ����
    }

    public override NodeState Evaluate()
    {
        if (fieldOfView.FindPlayer())
        {

            return NodeState.Success;
        }

        IsTracking = false;

        return NodeState.Failure;
    }
}

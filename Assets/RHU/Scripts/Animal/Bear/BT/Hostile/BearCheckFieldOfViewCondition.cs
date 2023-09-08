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
        fieldOfView = owner.GetComponentInChildren<FieldOfView>();      // TODO : 완성되고 range, angle등 모두 정해지면
                                                                        // GetComponent로 가져오면 부하가 약간 있으니까
                                                                        // GetComponent 대신 FieldOfVeiw 자체를 가지고 있는게 좋을것 같음
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

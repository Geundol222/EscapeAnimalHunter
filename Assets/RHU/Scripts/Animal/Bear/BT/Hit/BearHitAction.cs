using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class BearHitAction : ActionNode
{
    public int CurHp { get { return owner.curHp; } set { owner.curHp = value; } }
    public bool IsHit { get { return owner.isHit; } set { owner.isHit = value; } }

    //public BearHitAction(GameObject owner) : base(owner)
    //{

    //}

    public BearHitAction(Animal owner) : base(owner)
    {
        
    }

    //public BearHitAction(Animator animator, ref int curHp, ref bool isHit) : base(animator)
    //{
    //    this.curHp = curHp;
    //    this.isHit = isHit;
    //    Debug.Log(object.ReferenceEquals(this.curHp, curHp));
    //}

    //public BearHitAction(Animator animator, Bear bear) : base(animator)
    //{
    //      owner = bear;
    //    this.animator = bear.animator;
    //    this.curHp = bear.curHp;
    //    this.isHit = bear.isHit;
    //    Debug.Log(object.ReferenceEquals(this.curHp, bear.curHp));
    //}

    public override NodeState Evaluate()
    {
        Debug.Log($"Hit Evaluate, Hit������ curHp : {CurHp}");
        if (IsHit && CurHp >= 1)
        {
            animator.SetTrigger("IsHit");
            Debug.Log("Hit, SetTrigger �� Success ��ȯ");
            return NodeState.Success;
        }
        Debug.Log("Hit, Failure ��ȯ");
        return NodeState.Failure;
    }
}

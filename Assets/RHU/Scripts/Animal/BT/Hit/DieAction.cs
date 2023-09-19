using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public class DieAction : ActionNode
{
    protected bool IsHit { get { return owner.isHit; } }
    protected bool IsDie { get { return owner.isDie; } set { owner.isDie = value; } }
    protected int Cost { get { return owner.data.Animals[(int)owner.animalName].cost; } }

    public DieAction(Animal owner) : base(owner)
    {

    }

    public override NodeState Evaluate()
    {
        if (IsHit)
        {
            if (IsDie)
                return NodeState.Running;

            if (curAnimationCheck("Run") &&
                curAnimationCheck("Jump Run") &&
                curAnimationCheck("Run Left") &&
                curAnimationCheck("Run Right")
                )
                animator.SetInteger("RandomDie", 2);
            else
                animator.SetInteger("RandomDie", RandomAction(2));

            animator.SetTrigger("IsDie");
            IsDie = true;
            GameManager.Data.AddMoney(Cost);
            DataManager.Challenge.AnimalCount(owner);
            owner.onDied?.Invoke();

            return NodeState.Running;
        }

        return NodeState.Failure;
    }
}
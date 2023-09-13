using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    public override void SetUpBT()
    {   
        hitNode.childrenNode = new List<Node>()
        {                                           // ����ϴ� Owner�� ����
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        getAwayNode.childrenNode = new List<Node>
        {
            new GetAwayCondition(this),             // IsWary
            new GetAwayRunAction(this)              // WaryTime, IsWary
        };

        idleNode = new IdleAction(this);            // IsWary

        bTBase = new BTBase(hitNode, getAwayNode, idleNode);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    public override void SetUpBT()
    {   
        hitNode.childrenNode = new List<Node>()
        {                                           // 사용하는 Owner의 변수
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        getAwayNode.childrenNode = new List<Node>
        {
            new GetAwayAction(this)                 // WaryTime, IsWary
        };

        idleNode = new IdleAction(this);            // IsWary

        bTBase = new BTBase(hitNode, getAwayNode, idleNode);
    }
}

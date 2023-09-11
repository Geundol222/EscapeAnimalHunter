using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    public override void SetUpBT()
    {
        bTBase = new BTBase(hitNode, getAwayNode, idleNode);

        hitNode.childrenNode = new List<Node>()
        {                                           // 사용하는 Owner의 변수
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        getAwayNode.childrenNode = new List<Node>
        {

        };

        idleNode = new IdleAction(this);

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}

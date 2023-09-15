using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : Animal
{
    private SelectorNode hitNode = new SelectorNode();
    private SelectorNode getAwayNode = new SelectorNode();
    private SelectorNode idleNode = new SelectorNode();

    public override void SetUpBT()
    {
        bTBase = new BTBase(rootNode);

        rootNode.childrenNode = new List<Node>()
        {
            hitNode,
            getAwayNode,
            idleNode
        };

        hitNode.childrenNode = new List<Node>()
        {                                           // 사용하는 Owner의 변수
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        getAwayNode.childrenNode = new List<Node>
        {
            new GetAwayCondition(this),             // IsWary
            new GetAwayRunAction(this)              // WaryTime, IsWary
        };

        idleNode.childrenNode = new List<Node>
        {
            new IdleAction(this)                    // IsWary
        };
    }
}

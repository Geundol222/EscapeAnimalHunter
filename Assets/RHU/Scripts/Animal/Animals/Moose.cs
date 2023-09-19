using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    private SelectorNode hitNode = new SelectorNode();
    private SequenceNode getAwayNode = new SequenceNode();
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
            new DieAction(this)                     // IsHit, IsDie, Cost
        };

        getAwayNode.childrenNode = new List<Node>
        {
            new GetAwayCondition(this),             // IsWary
            new RunAction(this),                    // WaryTime, IsTracking, IsWary
        };

        idleNode.childrenNode = new List<Node>
        {
            new IdleAction(this)                    // IsWary
        };
    }
}

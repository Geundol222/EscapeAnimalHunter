using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    private SelectorNode hitNode = new SelectorNode();
    private SequenceNode hostileNode = new SequenceNode();
    private SelectorNode idleNode = new SelectorNode();

    public override void SetUpBT()
    {
        bTBase = new BTBase(rootNode);

        rootNode.childrenNode = new List<Node>()
        {
            hitNode,
            hostileNode,
            idleNode
        };

        hitNode.childrenNode = new List<Node>()
        {                                           // 사용하는 Owner의 변수
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        hostileNode.childrenNode = new List<Node>
        {
            new HostileCondition(this),             // IsWary
            new HostileRunAction(this),             // WaryTime, IsTracking, IsWary
            new CheckFieldOfViewCondition(this),    // FieldOfView, IsTracking
            new TrackingAction(this),               // FieldOfView, WaryTime, IsTracking
            new AttackAction(this)                  // FieldOfView
        };

        idleNode.childrenNode = new List<Node>
        {
            new IdleAction(this)                    // IsWary
        };
    }
}

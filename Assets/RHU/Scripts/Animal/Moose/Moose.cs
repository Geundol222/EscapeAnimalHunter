using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    public override void SetUpBT()
    {
        bTBase = new BTBase(hitNode, getAwayNode, idleNode);

        hitNode.childrenNode = new List<Node>()
        {                                           // ����ϴ� Owner�� ����
            new HitAction(this),                    // CurHp, IsHit, IsWary
            new DieAction(this)                     // IsHit, IsDie
        };

        hostileNode.childrenNode = new List<Node>
        {
            new HostileCondition(this),             // IsWary
            new RunAction(this),                    // TrackingTime, TrackingTime, IsWary
            new CheckFieldOfViewCondition(this),    // FieldOfView, IsTracking
            new TrackingAction(this),               // FieldOfView, TrackingTime, IsTracking
            new AttackAction(this)                  // FieldOfView
        };

        idleNode = new IdleAction(this);

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}

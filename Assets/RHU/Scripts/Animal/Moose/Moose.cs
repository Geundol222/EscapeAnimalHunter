using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose : Animal
{
    public override void SetUpBT()
    {
        hitNode.childrenNode = new List<Node>()
        {                                               // ����ϴ� Owner�� ����
            new HitAction(this),                    // CurHp, IsHit, IsHostile
            new DieAction(this)                     // IsHit, IsDie
        };

        hostileNode.childrenNode = new List<Node>
        {
            new HostileCondition(this),             // IsHostile
            new RunAction(this),                    // TrackingTime, TrackingTime, IsHostile
            new CheckFieldOfViewCondition(this),    // FieldOfView, IsTracking
            new TrackingAction(this),               // FieldOfView, TrackingTime, IsTracking
            new AttackAction(this)                  // FieldOfView
        };

        idleNode.childrenNode = new List<Node>
        {
            new IdleActionNode(this),
            new WalkAction(this)
        };

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}
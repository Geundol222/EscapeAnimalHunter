using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    public override void SetUpBT()
    {
        hitNode.childrenNode = new List<Node>()
        {                                               // 사용하는 Owner의 변수
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

        idleNode = new IdleAction(this);

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}

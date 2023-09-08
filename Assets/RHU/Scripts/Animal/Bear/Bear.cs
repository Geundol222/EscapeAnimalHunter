using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    public override void SetUpBT()
    {
        hitNode.childrenNode = new List<Node>()
        {                                               // 사용하는 Owner의 변수
            new BearHitAction(this),                    // CurHp, IsHit, IsHostile
            new BearUnconsciousAction(this)             // IsHit, IsUnconscious
        };

        hostileNode.childrenNode = new List<Node>
        {
            new BearHostileCondition(this),             // IsHostile
            new BearRunAction(this),                    // TrackingTime, TrackingTime, IsHostile
            new BearCheckFieldOfViewCondition(this),    // FieldOfView, IsTracking
            new BearTrackingAction(this),               // FieldOfView, TrackingTime, IsTracking
            new BearAttackAction(this)                  // FieldOfView
        };

        idleNode.childrenNode = new List<Node>
        {
            new BearIdleActionNode(this),
            new BearWalkAction(this)
        };

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}

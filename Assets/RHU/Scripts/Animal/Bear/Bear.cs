using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Animal
{
    public override void SetUpBT()
    {
        hitNode.childrenNode = new List<Node>()
        {
            new BearHitAction(this),
            new BearUnconsciousAction(this)
        };

        hostileNode.childrenNode = new List<Node>
        {
            new BearHostileCondition(this),
            new BearRunAction(this),
            new BearCheckFieldOfViewCondition(this),
            new BearTrackingAction(this),
            new BearAttackAction(this)
        };

        idleNode.childrenNode = new List<Node>
        {
            new BearIdleActionNode(this),
            new BearWalkAction(this)
        };

        this.bTBase = new BTBase(hitNode, hostileNode, idleNode);
    }
}

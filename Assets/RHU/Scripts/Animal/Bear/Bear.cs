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

        trackingNode.childrenNode = new List<Node>
        {
            //new BearDigAction(animator),
            //new BearLookAction(animator),
            //new BearSmellAction(animator),
            ////new BearWalkAction(animator)
        };

        idleNode.childrenNode = new List<Node>
        {
            new BearIdleActionNode(this),
            new BearWalkAction(this)
        };

        this.bTBase = new BTBase(hitNode, trackingNode, idleNode);
    }
}

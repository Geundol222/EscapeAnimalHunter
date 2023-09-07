using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase
{
    public SelectorNode rootNode;
    public SelectorNode hitNode;
    public SequenceNode trackingNode;
    public SelectorNode getAwayNode;
    public ActionNode idleActionNode;

    public BTBase(SelectorNode hitNode, SelectorNode getAwayNode, ActionNode idleActionNode)    // 초식
    {
        rootNode = new SelectorNode();
        this.hitNode = hitNode;
        this.getAwayNode = getAwayNode;
        this.idleActionNode = idleActionNode;

        rootNode.childrenNode = new List<Node>()
        {
            this.hitNode,
            this.getAwayNode,
            this.idleActionNode
        };
    }

    public BTBase(SelectorNode hitNode, SequenceNode trackingNode, ActionNode idleActionNode)   // 육식
    {
        rootNode = new SelectorNode();
        this.hitNode = hitNode;
        this.trackingNode = trackingNode;
        this.idleActionNode = idleActionNode;
        
        rootNode.childrenNode = new List<Node>()
        {
            this.hitNode,
            this.trackingNode,
            this.idleActionNode
        };
    }

    // 각 동물별로 행동을 좀 더 다양하게 하고 싶으면 생성자가 없는게 더 좋을것 같음

    public void Update()
    {
        rootNode.Evaluate();
    }
}

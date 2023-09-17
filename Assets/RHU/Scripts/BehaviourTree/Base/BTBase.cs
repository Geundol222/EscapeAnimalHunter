using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase
{
    public SelectorNode rootNode = new SelectorNode();
    //public SelectorNode hitNode;
    //public SequenceNode sequenceNode;
    //public ActionNode idleNode;

    public BTBase(SelectorNode rootNode)
    {
        this.rootNode = rootNode;
    }

    //public BTBase(SelectorNode hitNode, SequenceNode sequenceNode, ActionNode idleNode)
    //{
    //    rootNode = new SelectorNode();
    //    this.hitNode = hitNode;
    //    this.sequenceNode = sequenceNode;
    //    this.idleNode = idleNode;

    //    rootNode.childrenNode = new List<Node>()
    //    {
    //        this.hitNode,
    //        this.sequenceNode,
    //        this.idleNode
    //    };
    //}

    // 각 동물별로 행동을 좀 더 다양하게 하고 싶으면 생성자가 없는게 더 좋을것 같음

    public void Update()
    {
        rootNode.Evaluate();
    }
}

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

    // �� �������� �ൿ�� �� �� �پ��ϰ� �ϰ� ������ �����ڰ� ���°� �� ������ ����

    public void Update()
    {
        rootNode.Evaluate();
    }
}

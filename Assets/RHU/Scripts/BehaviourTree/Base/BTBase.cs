using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase
{
    public SelectorNode rootNode;
    public SelectorNode hitNode;
    public SequenceNode trackingNode;
    public SelectorNode getAwayNode;
    public ActionNode idleNode;

    public BTBase(SelectorNode hitNode, SelectorNode getAwayNode, ActionNode idleNode)    // �ʽ�
    {
        rootNode = new SelectorNode();
        this.hitNode = hitNode;
        this.getAwayNode = getAwayNode;
        this.idleNode = idleNode;

        rootNode.childrenNode = new List<Node>()
        {
            this.hitNode,
            this.getAwayNode,
            this.idleNode
        };
    }

    public BTBase(SelectorNode hitNode, SequenceNode trackingNode, ActionNode idleActionNode)   // ����
    {
        rootNode = new SelectorNode();
        this.hitNode = hitNode;
        this.trackingNode = trackingNode;
        this.idleNode = idleActionNode;
        
        rootNode.childrenNode = new List<Node>()
        {
            this.hitNode,
            this.trackingNode,
            this.idleNode
        };
    }

    // �� �������� �ൿ�� �� �� �پ��ϰ� �ϰ� ������ �����ڰ� ���°� �� ������ ����

    public void Update()
    {
        rootNode.Evaluate();
    }
}

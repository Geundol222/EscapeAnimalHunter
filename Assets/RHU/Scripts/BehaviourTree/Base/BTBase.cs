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

    public BTBase(SelectorNode hitNode, SelectorNode getAwayNode, ActionNode idleActionNode)    // �ʽ�
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

    public BTBase(SelectorNode hitNode, SequenceNode trackingNode, ActionNode idleActionNode)   // ����
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

    // �� �������� �ൿ�� �� �� �پ��ϰ� �ϰ� ������ �����ڰ� ���°� �� ������ ����

    public void Update()
    {
        rootNode.Evaluate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    public List<Node> childrenNode;

    public SelectorNode(List<Node> childrenNode)
    {
        this.childrenNode = childrenNode;
    }

    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach(Node childNode in childrenNode)
        {
            switch (childNode.Evaluate())
            {
                case NodeState.Success :
                    return NodeState.Success;

                case NodeState.Running :
                    return NodeState.Running;
            }
        }

        return NodeState.Failure;
    }
}

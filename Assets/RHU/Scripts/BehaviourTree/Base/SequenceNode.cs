using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public List<Node> childrenNode;

    public SequenceNode(List<Node> childrenNode)
    {
        this.childrenNode = childrenNode;
    }

    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach (Node childNode in childrenNode)
        {
            switch (childNode.Evaluate())
            {
                case NodeState.Success:
                    continue;

                case NodeState.Running :
                    return NodeState.Running;

                case NodeState.Failure :
                    return NodeState.Failure;
            }
        }

        return NodeState.Success;
    }
}

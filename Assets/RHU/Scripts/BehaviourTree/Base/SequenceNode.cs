using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public SequenceNode(Node item, Node parent, Node left, Node right) : base(item, parent, left, right)
    {
    }

    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach (Node childNode in childrenNode)
        {
            switch (childNode.CurState)
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

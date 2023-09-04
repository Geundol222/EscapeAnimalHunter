using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public SelectorNode(Node item, Node parent, Node left, Node right) : base(item, parent, left, right)
    {
    }

    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach(Node childNode in childrenNode)
        {
            switch (childNode.CurState)
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

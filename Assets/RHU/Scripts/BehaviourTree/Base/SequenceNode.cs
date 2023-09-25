using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach (var childNode in childrenNode)
        {
            switch (childNode.Evaluate())
            {
                case NodeState.Running :
                    return NodeState.Running;

                case NodeState.Success:
                    continue;

                case NodeState.Failure :
                    return NodeState.Failure;
            }
        }

        return NodeState.Success;
    }
}

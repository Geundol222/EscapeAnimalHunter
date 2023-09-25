using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : CompositeNode
{
    public override NodeState Evaluate()
    {
        if (childrenNode == null || childrenNode.Count == 0)
            return NodeState.Failure;

        foreach (var childNode in childrenNode)
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

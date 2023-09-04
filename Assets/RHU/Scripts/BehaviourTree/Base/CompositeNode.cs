using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeNode : Node
{
    public List<Node> childrenNode;
    public Node item;

    public CompositeNode(Node item, Node parent, Node left, Node right) : base(item, parent, left, right)
    {
        this.item = item;
    }

    public abstract NodeState Evaluate();
}

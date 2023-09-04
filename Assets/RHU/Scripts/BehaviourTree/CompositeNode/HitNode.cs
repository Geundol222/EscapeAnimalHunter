using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitNode : SelectorNode
{
    protected HitNode(Node item, Node parent, Node left, Node right) : base(item, parent, left, right)
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public enum NodeState { Failure, Success, Running }

    private NodeState curState;
    private Node item;
    private Node parent;
    private Node left;
    private Node right;

    public NodeState CurState { get { return curState; } set { curState = value; } }
    public Node Item { get { return item; } set { item = value; } }
    public Node Parent { get { return parent; } set { parent = value; } }
    public Node Left { get { return left; } set { left = value; } }
    public Node Right { get { return right; } set { right = value; } }
    public bool IsRootNode { get { return parent == null; } }
    public bool IsLeftChild { get { return parent != null && parent.left == this; } }
    public bool IsRightChild { get { return parent != null && parent.right == this; } }
    public bool HasLeftChild { get { return left != null; } }
    public bool HasRightChild { get { return right != null; } }
    public bool HasNoChild { get { return !HasLeftChild && !HasRightChild; } }
    public bool HasBothChild { get { return HasLeftChild && HasRightChild; } }

    public Node(Node item, Node parent, Node left, Node right)
    {
        this.item = item;
        this.parent = parent;
        this.left = left;
        this.right = right;
    }
}

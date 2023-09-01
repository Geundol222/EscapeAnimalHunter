using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum NodeState { Failure, Success, Running }

    private NodeState curState;
    public NodeState CurState { get { return curState; } set { curState = value; } }
}

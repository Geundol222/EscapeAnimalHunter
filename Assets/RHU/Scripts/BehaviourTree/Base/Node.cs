using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{
    public enum NodeState { Failure, Success, Running }

    public abstract NodeState Evaluate();
}

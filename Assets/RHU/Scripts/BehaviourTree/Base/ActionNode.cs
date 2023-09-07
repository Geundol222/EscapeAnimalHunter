using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    public Animator animator;
    protected int random;

    public ActionNode(Animator animator)
    {
        this.animator = animator;
    }

    public bool RandomAction(/*int max*/)
    {
        random = Random.Range(0, /*max*/10);

        switch (random)
        {
            case 0:
                return true;

            default:
                return false;
        }
    }
}

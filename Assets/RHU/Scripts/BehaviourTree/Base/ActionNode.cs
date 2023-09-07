using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : Node
{
    public Animal owner;
    public Animator animator;
    protected int random;

    public ActionNode(Animal owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
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

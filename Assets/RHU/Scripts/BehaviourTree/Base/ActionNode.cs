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

    public int RandomAction(int max)
    {
        //random = Random.Range(0, max);

        //switch (random)
        //{
        //    case 0:
        //        return true;

        //    default:
        //        return false;
        //}
        return random = Random.Range(0, max);
    }
}

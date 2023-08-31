using AnimalStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AnimalStateBase
{
    protected AnimalStateMachine animal;

    public IdleState(AnimalStateMachine animal)
    {
        this.animal = animal;
    }

    public override void Enter()
    {

    }

    public override void Update()
    {

    }

    public override void Exit()
    {

    }
}

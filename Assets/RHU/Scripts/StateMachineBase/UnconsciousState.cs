using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimalStates;

public class UnconsciousState : AnimalStateBase
{
    protected AnimalStateMachine animal;

    public UnconsciousState(AnimalStateMachine animal)
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

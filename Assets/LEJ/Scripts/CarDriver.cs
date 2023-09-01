using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    GameObject player;
    CharacterController characterController;
    PlayerCarInteractor carInteractor;
    PlayerInputDetecter inputDetecter;
    SetGearState gearState;

    [SerializeField] GameObject gearObj;
    [SerializeField] GameObject handleRotationObj;
    [SerializeField] GameObject GearPositionObj;

    [SerializeField] float maxSpeed;

    [SerializeField] bool isAcceling;
    [SerializeField] bool isBreaking;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        characterController = GetComponent<CharacterController>();
        carInteractor = player.GetComponent<PlayerCarInteractor>();
        inputDetecter = player.GetComponent<PlayerInputDetecter>();
        gearState = gearObj.GetComponent<SetGearState>();
    }

    private void Update()
    {
        if (carInteractor.isPlayerTakingCar)
        {
            WhichPedalIsUsing();

            if (isAcceling)
                UseAccelPedal();

            if (isBreaking)
                UseBreakPedal();
        }
    }

    private void WhichPedalIsUsing()
    {
        if (carInteractor.isPlayerTakingCar)
        {
            if (inputDetecter.rightJoyStickYValue >= 0f && !inputDetecter.isLeftTriggerPressed)
            {
                isAcceling = true;
                isBreaking = false;
            }

            if (inputDetecter.isLeftTriggerPressed)
            {
                isAcceling = false;
                isBreaking = true;
            }

        }
    }

    private void UseAccelPedal()
    {
        if (gearState.isDriveState)
            characterController.Move(transform.forward * Time.deltaTime * maxSpeed * inputDetecter.rightJoyStickYValue);
        
        if (gearState.isReverseState)
            characterController.Move(-transform.forward * Time.deltaTime * maxSpeed * inputDetecter.rightJoyStickYValue);
    }

    private void UseBreakPedal()
    {
        characterController.Move(transform.position);
    }

}


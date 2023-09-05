using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CarDriver : MonoBehaviour
{
    GameObject player;
    CharacterController characterController;
    PlayerCarInteractor carInteractor;
    PlayerInputDetecter inputDetecter;
    SetGearState gearState;

    [SerializeField] GameObject gearObj;
    [SerializeField] GameObject handleObj;
    [SerializeField] GameObject handleRotatePivotObj;

    [SerializeField] float maxSpeed;
    [SerializeField] float handleRotateSpeed;
    [SerializeField] float backToZeroSpeed;

    [SerializeField] bool isAcceling;
    bool isBreaking;
    XRKnobLEJ handleKnob;
    float setHandleValue;
    bool isMoving;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;

        player = GameObject.FindWithTag("Player");
        carInteractor = player.GetComponent<PlayerCarInteractor>();
        inputDetecter = player.GetComponent<PlayerInputDetecter>();

        gearState = gearObj.GetComponent<SetGearState>();
        handleKnob = handleObj.GetComponent<XRKnobLEJ>();

        handleKnob.value = 0.5f;
    }

    private void Update()
    {
        if (player.GetComponent<PlayerCarInteractor>().isPlayerTakingCar)
            characterController.enabled = true;

        if (gearState.isDriveState || gearState.isReverseState)
        {
            WhichPedalIsUsing();

            if (isAcceling)
            {
                UseAccelPedal();
                Handling();
            }

            if (isBreaking)
            {
                UseBreakPedal();
                Handling();
            }
        }
        if (!handleKnob.isHandleGripped)
            handleKnob.value = Mathf.Lerp(handleKnob.value, 0.5f, backToZeroSpeed);

    }

    private void WhichPedalIsUsing()
    {
        if (carInteractor.isPlayerTakingCar)
        {
            if (inputDetecter.rightJoyStickYValue > 0 || inputDetecter.leftJoyStickYValue > 0)
            {
                isAcceling = true;
                isBreaking = false;

                isMoving = true;
            }
            else if (inputDetecter.rightJoyStickYValue < 0 || inputDetecter.leftJoyStickYValue < 0)
            {
                isAcceling = false;
                isBreaking = true;

                isMoving = true;
            }
            else
                isMoving = false;
        }
    }

    private void UseAccelPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
        {
            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * maxSpeed * inputDetecter.leftJoyStickYValue);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * maxSpeed * inputDetecter.leftJoyStickYValue);
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * maxSpeed * inputDetecter.rightJoyStickYValue);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * maxSpeed * inputDetecter.rightJoyStickYValue);
        }
    }

    
    private void UseBreakPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
            characterController.Move(transform.position * Time.deltaTime * -inputDetecter.leftJoyStickYValue);

        if (inputDetecter.rightJoyStickYValue != 0)
            characterController.Move(transform.position * Time.deltaTime * -inputDetecter.rightJoyStickYValue);
    }

    private void Handling()
    {
        SetHandleValue();

        if (isMoving)
            transform.RotateAround(transform.position, new Vector3(0f, 1f, 0f), -handleRotateSpeed * setHandleValue);
    }

    private void SetHandleValue()
    {
        //To make handleValue.value 0f ~ 1f to -1f ~ 1f

        if (handleKnob.value == 0.5f)
            setHandleValue = 0f;

        else if (handleKnob.value < 0.5f)
            setHandleValue = -1f + handleKnob.value * 2f; //-1f ~ 0f

        else
            setHandleValue = (handleKnob.value - 0.5f) * 2f; //0f ~ 1f
    }
}


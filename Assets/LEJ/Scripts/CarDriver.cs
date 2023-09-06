using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CarDriver : MonoBehaviour
{
    [SerializeField] GameObject player;
    CharacterController characterController;
    PlayerCarInteractor carInteractor;
    PlayerInputDetecter inputDetecter;
    SetGearState gearState;
    XRKnobLEJ handleKnob;
    DetectHandleGrab handleGrab;

    [SerializeField] GameObject gearObj;
    [SerializeField] GameObject handleObj;
    [SerializeField] GameObject handleRotatePivotObj;

    [SerializeField] float maxSpeed; //in LEJTestScene : 30
    [SerializeField] float curSpeed;
    [SerializeField] float accelLerpValue; //in LEJTestScene : 0.01
    [SerializeField] float breakLerpValue;
    [SerializeField] float handleRotateSpeed; //in LEJTestScene : 0.5
    [SerializeField] float backToZeroSpeed; //in LEJTestScene : 0.01

    bool isAcceling;
    bool isBreaking;
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
        handleGrab = handleObj.GetComponent<DetectHandleGrab>();

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
        if (!handleGrab.isRightGrip|| !handleGrab.isLeftGrip)
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
            }
            else if (inputDetecter.rightJoyStickYValue < 0 || inputDetecter.leftJoyStickYValue < 0)
            {
                isAcceling = false;
                isBreaking = true;
            }
            else
            {
                curSpeed = Mathf.Lerp(curSpeed, 0, accelLerpValue);
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);
            }


            if (curSpeed < 1f)
                isMoving = false;
            else
                isMoving = true;
        }
    }

    private void UseAccelPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
        {
            curSpeed += maxSpeed * inputDetecter.leftJoyStickYValue * accelLerpValue;

            if (curSpeed > maxSpeed)
                curSpeed = maxSpeed;

            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * curSpeed);
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            curSpeed += maxSpeed * inputDetecter.rightJoyStickYValue * accelLerpValue;

            if (curSpeed > maxSpeed)
                curSpeed = maxSpeed;

            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * curSpeed);
        }
    }

    
    private void UseBreakPedal()
    {

        if (inputDetecter.leftJoyStickYValue != 0)
        {
            curSpeed += maxSpeed * inputDetecter.leftJoyStickYValue * breakLerpValue;
            if (curSpeed < 0)
                curSpeed = 0;

            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * curSpeed);

        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            curSpeed += maxSpeed * inputDetecter.rightJoyStickYValue * breakLerpValue;
            if (curSpeed < 0)
                curSpeed = 0;

            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * curSpeed);
        }
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


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
    XRKnobLEJ handleKnob;

    [SerializeField] GameObject gearObj;
    [SerializeField] GameObject handleObj;
    [SerializeField] GameObject handleRotatePivotObj;

    [SerializeField] float maxSpeed; //in LEJTestScene : 30
    [SerializeField] float curSpeed;
    [SerializeField] float targetSpeed;
    [SerializeField] float speedLerpValue; //in LEJTestScene : 0.005
    [SerializeField] float handleRotateSpeed; //in LEJTestScene : 0.5
    [SerializeField] float backToZeroSpeed; //in LEJTestScene : 0.05

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
            }
            else if (inputDetecter.rightJoyStickYValue < 0 || inputDetecter.leftJoyStickYValue < 0)
            {
                isAcceling = false;
                isBreaking = true;
            }
            else
            {
                curSpeed = Mathf.Lerp(curSpeed, 0, speedLerpValue);
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);
            }


            if (curSpeed == 0)
                isMoving = false;
            else
                isMoving = true;
        }
    }

    private void UseAccelPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
        {
            targetSpeed = maxSpeed * inputDetecter.leftJoyStickYValue;
            curSpeed = Mathf.Lerp(curSpeed, targetSpeed, speedLerpValue);

            if (gearState.isDriveState)
                characterController.Move(transform.forward * Time.deltaTime * curSpeed);

            if (gearState.isReverseState)
                characterController.Move(-transform.forward * Time.deltaTime * curSpeed);
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            targetSpeed = maxSpeed * inputDetecter.rightJoyStickYValue;
            curSpeed = Mathf.Lerp(curSpeed, targetSpeed, speedLerpValue);

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
            curSpeed = Mathf.Lerp(curSpeed, 0, speedLerpValue * -inputDetecter.leftJoyStickYValue);
            characterController.Move(transform.forward * Time.deltaTime * -inputDetecter.leftJoyStickYValue);
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            curSpeed = Mathf.Lerp(curSpeed, 0, speedLerpValue * -inputDetecter.rightJoyStickYValue);
            characterController.Move(transform.forward * Time.deltaTime * -inputDetecter.rightJoyStickYValue);
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


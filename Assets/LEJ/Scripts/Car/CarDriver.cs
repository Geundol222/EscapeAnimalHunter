using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class CarDriver : MonoBehaviour
{
    
    [SerializeField] GameObject player;
    Rigidbody rb;
    PlayerCarInteractor carInteractor;
    PlayerInputDetecter inputDetecter;
    SetGearState gearState;
    XRKnobLEJ handleKnob;
    DetectHandleGrab handleGrab;

    public UnityAction OnMaxSpeedChanged;

    [SerializeField] GameObject gearObj;
    [SerializeField] GameObject handleObj;
    [SerializeField] GameObject handleRotatePivotObj;

    [SerializeField] float maxSpeed; //in LEJTestScene : 30
    public float MaxSpeed { get { return maxSpeed; }
        set 
        { 
            maxSpeed = value;
            OnMaxSpeedChanged?.Invoke();
        }
    }
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
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindWithTag("Player");
        carInteractor = player.GetComponent<PlayerCarInteractor>();
        inputDetecter = player.GetComponent<PlayerInputDetecter>();

        gearState = gearObj.GetComponent<SetGearState>();
        handleKnob = handleObj.GetComponent<XRKnobLEJ>();
        handleGrab = handleObj.GetComponent<DetectHandleGrab>();

        handleKnob.value = 0.5f;
    }

    private void FixedUpdate()
    {

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
            
        }
    }

    private void UseAccelPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
        {
            rb.AddForce(new Vector3(0, 0, inputDetecter.leftJoyStickYValue * maxSpeed));
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            rb.AddForce(new Vector3(0, 0, inputDetecter.rightJoyStickYValue * maxSpeed));
        }
    }

    
    private void UseBreakPedal()
    {

        if (inputDetecter.leftJoyStickYValue != 0)
        {
            rb.AddForce(new Vector3(0, 0, -inputDetecter.leftJoyStickYValue * maxSpeed));

        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            rb.AddForce(new Vector3(0, 0, -inputDetecter.rightJoyStickYValue * maxSpeed));
        }
    }

    private void Handling()
    {
        SetHandleValue();

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


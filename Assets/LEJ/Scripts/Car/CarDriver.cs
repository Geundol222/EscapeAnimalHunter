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

    public UnityAction OnCurSpeedChanged;
    [SerializeField] private float curSpeed;
    public float CurSpeed { get { return curSpeed; }
        set 
        {
            curSpeed = value;
            OnCurSpeedChanged?.Invoke();
        }
    }

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
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

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

    /// <summary>
    /// 컨트롤러의 조이스틱 value에 따라 엑셀(0 ~ 1)인지 브레이크(0 ~ -1)인지 판단
    /// </summary>

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

    /// <summary>
    /// 만약 엑셀 페달을 밟고 있다면 기어 상태에 따라 전진, 후진함
    /// </summary>
    private void UseAccelPedal()
    {
        if (gearState.isDriveState)
        {
            if (inputDetecter.leftJoyStickYValue != 0)
            {
                CurSpeed = inputDetecter.leftJoyStickYValue * maxSpeed;
                rb.AddForce(transform.forward * CurSpeed);
            }

            if (inputDetecter.rightJoyStickYValue != 0)
            {
                CurSpeed = inputDetecter.rightJoyStickYValue * maxSpeed;
                rb.AddForce(transform.forward * CurSpeed);
            }
        }

        if (gearState.isReverseState)
        {
            if (inputDetecter.leftJoyStickYValue != 0)
            {
                CurSpeed = inputDetecter.leftJoyStickYValue * maxSpeed;
                rb.AddForce(transform.forward * -CurSpeed);
            }

            if (inputDetecter.rightJoyStickYValue != 0)
            {
                CurSpeed = inputDetecter.rightJoyStickYValue * maxSpeed;
                rb.AddForce(transform.forward * -CurSpeed);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void UseBreakPedal()
    {
        if (inputDetecter.leftJoyStickYValue != 0)
        {
            CurSpeed = -inputDetecter.leftJoyStickYValue * maxSpeed;
            rb.AddForce(transform.forward * CurSpeed);
        }

        if (inputDetecter.rightJoyStickYValue != 0)
        {
            CurSpeed = -inputDetecter.rightJoyStickYValue * maxSpeed;
            rb.AddForce(transform.forward * CurSpeed);
        }
    }

    private void Handling()
    {
        SetHandleValue();

        if (curSpeed > 0)
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


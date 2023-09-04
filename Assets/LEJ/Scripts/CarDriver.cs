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

    bool isAcceling;
    bool isBreaking;
    XRKnobLEJ handleValue;
    float setHandleValue;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        player = GameObject.FindWithTag("Player");
        carInteractor = player.GetComponent<PlayerCarInteractor>();
        inputDetecter = player.GetComponent<PlayerInputDetecter>();

        gearState = gearObj.GetComponent<SetGearState>();
        handleValue = handleObj.GetComponent<XRKnobLEJ>();

        handleValue.value = 0.5f;
    }

    private void Update()
    {
        Handling();

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

    private void WhichPedalIsUsing()
    {
        if (carInteractor.isPlayerTakingCar)
        {
            if (inputDetecter.rightJoyStickYValue > 0)
            {
                isAcceling = true;
                isBreaking = false;
            }

            if (inputDetecter.leftJoyStickYValue > 0)
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
        if (gearState.isDriveState)
            characterController.Move(transform.position * Time.deltaTime * inputDetecter.leftJoyStickYValue);

        if (gearState.isReverseState)
            characterController.Move(-transform.position * Time.deltaTime * inputDetecter.leftJoyStickYValue);
    }

    private void Handling()
    {
        SetHandleValue();

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y - handleRotateAmount * setHandleValue, transform.rotation.z);
        transform.RotateAround(transform.position, new Vector3(0f, 1f, 0f), -handleRotateSpeed * setHandleValue);
    }

    private void SetHandleValue()
    {
        //To make handleValue.value 0f ~ 1f to -1f ~ 1f

        if (handleValue.value == 0.5f)
            setHandleValue = 0f;

        else if (handleValue.value < 0.5f)
            setHandleValue = -1f + handleValue.value * 2f; //-1f ~ 0f

        else
            setHandleValue = (handleValue.value - 0.5f) * 2f; //0f ~ 1f
    }
}


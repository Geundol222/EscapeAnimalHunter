using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;

public class DetectHandleGrab : MonoBehaviour
{
    [SerializeField] private GameObject rightControllerModel;
    [SerializeField] private GameObject leftControllerModel;

    [SerializeField] private GameObject rightHandleHand;
    [SerializeField] private GameObject leftHandleHand;

    public bool isRightGrip;
    public bool isLeftGrip;

    private PlayerInputDetecter playerInput;
    XRKnob knob;


    private void Awake()
    {
        knob = GetComponent<XRKnob>();
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInputDetecter>();
        
        rightControllerModel.SetActive(false);
        leftControllerModel.SetActive(false);

        rightControllerModel.SetActive(true);
        leftControllerModel.SetActive(true);
    }

    private void OnEnable()
    {
        knob.OnStartGrab += SetActiveTrue;
        knob.OnExitGrab += SetActiveFalse;
    }

    private void OnDisable()
    {
        knob.OnStartGrab -= SetActiveTrue;
        knob.OnExitGrab -= SetActiveFalse;
    }

    private void SetActiveTrue(bool isRight)
    {
        if (isRight)
        {
            rightControllerModel.SetActive(false);
            rightHandleHand.SetActive(true);
            isRightGrip = true;
        }
        else
        {
            leftControllerModel.SetActive(false);
            leftHandleHand.SetActive(true);
            isLeftGrip = true;
        }

        if (isRightGrip || isLeftGrip)
            knob.changeAmount = knob.handleChangeAmount;
    }

    private void SetActiveFalse(bool isRight)
    {
        if (isRight)
        {
            rightControllerModel.SetActive(true);
            rightHandleHand.SetActive(false);
            isRightGrip = false;
        }
        else
        {
            leftControllerModel.SetActive(true);
            leftHandleHand.SetActive(false);
            isLeftGrip = false;
        }

        if (!isRightGrip && !isLeftGrip)
            knob.changeAmount = 180f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectHandleGrab : MonoBehaviour
{
    [SerializeField] private GameObject rightControllerModel;
    [SerializeField] private GameObject leftControllerModel;

    [SerializeField] private GameObject rightHandleHand;
    [SerializeField] private GameObject leftHandleHand;

    private bool isRightAttach;
    private bool isLeftAttach;

    public bool isRightGrip;
    public bool isLeftGrip;


    private PlayerInputDetecter playerInput;



    private void Awake()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInputDetecter>();
        
        rightControllerModel.SetActive(false);
        leftControllerModel.SetActive(false);
    }

    private void Update()
    {
        if (isRightAttach && playerInput.isRightGripPressed)
            isRightGrip = true;


        if (isLeftAttach && playerInput.isLeftGripPressed)
            isLeftGrip = true;

        if (isRightGrip)
        {
            rightControllerModel.SetActive(false);
            rightHandleHand.SetActive(true);

        }

        if (!isRightGrip)
        {
            rightControllerModel.SetActive(true);
            rightHandleHand.SetActive(false);


        }

        if (isLeftGrip)
        {
            leftControllerModel.SetActive(false);
            leftHandleHand.SetActive(true);
        }
        if (!isLeftGrip)
        {

            leftControllerModel.SetActive(true);
            leftHandleHand.SetActive(false);
        }
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightController")
            isRightAttach = true;

        if (other.tag == "LeftController")
            isLeftAttach = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RightController")
            isRightAttach = false;
        if (other.tag == "LeftController")
            isLeftAttach = false;
    }
}

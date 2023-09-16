using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class PlayerControllerMoveDetecter : MonoBehaviour
{
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;

    [SerializeField] XRKnobLEJ handleKnob;
    [SerializeField] DetectHandleGrab handleGrab;
    
    public UnityAction<bool> OnRotationChanged; //true : rightController, false : leftController

    Vector3 r_prevRot;
    Vector3 r_curRot;
    
    Vector3 l_prevRot;
    Vector3 l_curRot;

    
    private void Update()
    {
        if (handleGrab.isRightGrip)
        {
            r_prevRot = rightController.transform.localRotation.eulerAngles;

            if (r_prevRot != r_curRot)
            {
                OnRotationChanged?.Invoke(true);
                r_curRot = r_prevRot;
            }
        }

        if (handleGrab.isLeftGrip)
        {
            l_prevRot = leftController.transform.localRotation.eulerAngles;

            if (l_prevRot != l_curRot)
            {
                OnRotationChanged?.Invoke(false);
                l_curRot = l_prevRot;
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class HandleRotationSupporter : MonoBehaviour
{
    [SerializeField] float backToZeroSpeed;
    XRKnobLEJ xrKnob;
    bool backToZero;

    private void Awake()
    {
        xrKnob = GetComponent<XRKnobLEJ>();
    }

    private void Update()
    {
        if (backToZero)
            xrKnob.value = Mathf.Lerp(xrKnob.value, 0.5f, backToZeroSpeed);

    }

    private void SetBackToZeroBoolTrue()
    {
        backToZero = true;
    }

    private void SetBackToZeroBoolFalse()
    {
        backToZero = false;
    }

}

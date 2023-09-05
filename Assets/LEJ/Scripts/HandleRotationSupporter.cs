using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class HandleRotationSupporter : MonoBehaviour
{
    [SerializeField] float backToZeroSpeed;
    XRKnobLEJ xrKnob;

    private void Awake()
    {
        xrKnob = GetComponent<XRKnobLEJ>();
    }

    private void Update()
    {
        

    }

}

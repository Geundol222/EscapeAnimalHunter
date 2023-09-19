using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class RotateFakeHandle : MonoBehaviour
{
    [SerializeField] GameObject realHandle;
    XRKnob knob;
    float rotateAmount = 3f;

    private void Awake()
    {
        knob = realHandle.GetComponent<XRKnob>();
    }

    public void Update()
    {

        transform.GetChild(0).transform.localRotation = Quaternion.Euler(0f, transform.GetChild(0).transform.localRotation.y + realHandle.transform.GetChild(0).transform.localRotation.y * 180f * rotateAmount, 0f);
    }
}

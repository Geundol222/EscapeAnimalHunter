using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Rot : MonoBehaviour
{
    [SerializeField] XRKnobLEJ knob;

    float prevY;
    float curY;

    private void Awake()
    {
        prevY = knob.value * 180f;
    }
    private void Update()
    {
        if (prevY + 0.1 < knob.value * 180f)
            return;

        gameObject.transform.rotation = Quaternion.Euler(0f, knob.value * 180f, 0f);
        prevY = gameObject.transform.rotation.y;
    }
}

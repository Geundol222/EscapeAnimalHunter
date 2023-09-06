using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SetGearState : MonoBehaviour
{
    [SerializeField] public bool isParkState;
    [SerializeField] public bool isReverseState;
    [SerializeField] public bool isNeutralState;
    [SerializeField] public bool isDriveState;

    XRSliderLEJ slider;

    private void Awake()
    {
        slider = GetComponent<XRSliderLEJ>();
        slider.value = 0f;
    }

    private void OnEnable()
    {
        slider.OnStopGrabbing += FixPosition;
    }

    private void OnDisable()
    {
        slider.OnStopGrabbing -= FixPosition;
    }

    private void FixPosition()
    {
        SetAllBoolsToFalse();

        if (slider.value >= 0f && slider.value < 0.3f)
        {
            slider.value = 0f;
            isParkState = true;
        }
        else if (slider.value >= 0.3f && slider.value < 0.6f)
        {
            slider.value = 0.5f;
            isReverseState = true;
        }
        else if (slider.value >= 0.6f && slider.value < 0.9f)
        {
            slider.value = 0.75f;
            isNeutralState = true;
        }
        else
        {
            slider.value = 1f;
            isDriveState = true;
        }
    }

    private void SetAllBoolsToFalse()
    {
        isParkState = false;
        isReverseState = false;
        isNeutralState = false;
        isDriveState = false;
    }
}

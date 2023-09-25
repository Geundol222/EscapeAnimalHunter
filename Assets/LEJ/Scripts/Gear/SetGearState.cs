using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class SetGearState : MonoBehaviour
{
    [SerializeField] public bool isParkState;
    [SerializeField] public bool isReverseState;
    [SerializeField] public bool isNeutralState;
    [SerializeField] public bool isDriveState;

    XRSliderLEJ slider;
    public UnityAction<string> OnCurGearStateChanged;

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
            OnCurGearStateChanged?.Invoke("Park");
        }
        else if (slider.value >= 0.3f && slider.value < 0.6f)
        {
            slider.value = 0.5f;
            isReverseState = true;
            OnCurGearStateChanged?.Invoke("Reverse");
        }
        else if (slider.value >= 0.6f && slider.value < 0.9f)
        {
            slider.value = 0.75f;
            isNeutralState = true;
            OnCurGearStateChanged?.Invoke("Neutral");
        }
        else
        {
            slider.value = 1f;
            isDriveState = true;
            OnCurGearStateChanged?.Invoke("Drive");
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

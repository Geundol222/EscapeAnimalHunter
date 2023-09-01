using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum Select { First, Second }

public class GunGrabEvent : MonoBehaviour
{
    [SerializeField] Transform attachPoint;
    [SerializeField] Transform secondaryPoint;

    int handCount = 0;
    Dictionary<Select, Transform> interactors;
    XRGrabInteractable grabInteractable;

    private void Awake()
    {
        interactors = new Dictionary<Select, Transform>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void SetPriority(IXRInteractor interactor)
    {
        interactors.Add((Select)handCount, interactor.transform);

        if (handCount == 0)
            handCount++;
    }

    public void SetAttachPointToFirst()
    {
        grabInteractable.attachTransform = attachPoint;
    }

    public void SetAttachPointToSecond()
    {
        grabInteractable.attachTransform = secondaryPoint;
    }
}

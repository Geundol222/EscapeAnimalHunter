using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum Select { First, Second }

public class GunGrabEvent : MonoBehaviour
{
    [SerializeField] Transform attachPoint;
    [SerializeField] Transform secondaryPoint;

    bool isFirst;
    Dictionary<Select, Transform> interactors;
    XRGrabInteractable grabInteractable;
    private void Awake()
    {
        interactors = new Dictionary<Select, Transform>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void SetPriority(SelectEnterEventArgs args)
    {
        isFirst = grabInteractable.interactorsSelecting.Count <= 1 || ReferenceEquals(args.interactorObject, grabInteractable.interactorsSelecting[0]);

        if (isFirst)
        {
        }
    }

    public void SetAttachPointToFirst(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform == interactors[Select.First])
            grabInteractable.attachTransform = attachPoint;
        else
            return;
    }

    public void SetAttachPointToSecond(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform == interactors[Select.First])
            grabInteractable.attachTransform = secondaryPoint;
        else
            return;
    }
}

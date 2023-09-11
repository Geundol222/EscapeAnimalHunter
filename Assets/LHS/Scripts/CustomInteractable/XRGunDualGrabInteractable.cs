using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGunDualGrabInteractable : XRGrabInteractable
{
    [SerializeField] Transform gripTransform;

    XRGunDualGrabTransformer transformer;

    protected override void Awake()
    {
        base.Awake();
        transformer = GetComponent<XRGunDualGrabTransformer>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
    }
}
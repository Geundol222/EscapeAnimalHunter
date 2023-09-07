using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;
using static TwoHandGrabInteractable;
using static UnityEngine.XR.Interaction.Toolkit.Transformers.XRGeneralGrabTransformer;

public class XRGunDualGrabInteractable : XRBaseInteractable
{
    #region Enum
    public enum TwoHandRotationType { None, First, Second };
    public enum GrabState { None, Main, Sub, Multi }
    private GrabState state;
    #endregion

    #region Component
    private Rigidbody rigid;
    #endregion

    #region Inspector
    [SerializeField] Transform mainAttachTransform;
    [SerializeField] Transform subAttachTransform;
    [SerializeField] TwoHandRotationType twoHandRotationType;
    #endregion

    private Pose mainPose;
    private Pose subPose;

    private IXRInteractor mainInteractor;
    private IXRInteractor subInteractor;

    private bool firstFrameSinceTwoHandedGrab;

    protected override void Awake()
    {
        base.Awake();

        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        mainPose = mainAttachTransform.GetLocalPose();
        subPose = subAttachTransform.GetLocalPose();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        switch (state)
        {
            case GrabState.None:
                mainInteractor = args.interactorObject;
                state = GrabState.Main;
                OnGrab();
                break;
            case GrabState.Main:
                subInteractor = args.interactorObject;
                state = GrabState.Multi;
                firstFrameSinceTwoHandedGrab = true;
                break;
            case GrabState.Sub:
                mainInteractor = args.interactorObject;
                state = GrabState.Multi;
                firstFrameSinceTwoHandedGrab = true;
                break;
        }

        Debug.Log(state);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        if (ReferenceEquals(args.interactorObject, mainInteractor))
        {
            mainInteractor = default;
            switch (state)
            {
                case GrabState.Main:
                    state = GrabState.None;
                    OnDrop();
                    break;
                case GrabState.Multi:
                    state = GrabState.Sub;
                    break;
            }
        }
        else // if (ReferenceEquals(args.interactorObject, subInteractor))
        {
            subInteractor = default;
            switch (state)
            {
                case GrabState.Sub:
                    state = GrabState.None;
                    OnDrop();
                    break;
                case GrabState.Multi:
                    state = GrabState.Main;
                    break;
            }
        }

        Debug.Log(state);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (!isSelected)
            return;

        switch (updatePhase)
        {
            case XRInteractionUpdateOrder.UpdatePhase.Dynamic:
            case XRInteractionUpdateOrder.UpdatePhase.OnBeforeRender:
                switch (state)
                {
                    case GrabState.Main:
                        MainGrabUpdate();
                        break;
                    case GrabState.Sub:
                        SubGrabUpdate();
                        break;
                    case GrabState.Multi:
                        MultiGrabUpdate();
                        break;
                }
                break;
            case XRInteractionUpdateOrder.UpdatePhase.Fixed:
                // TODO : Physics process
                break;
            case XRInteractionUpdateOrder.UpdatePhase.Late:
                // TODO : Late process
                break;
        }
    }

    private void MainGrabUpdate()
    {
        Transform interactorAttachTransform = mainInteractor.GetAttachTransform(this);
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - mainAttachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = mainAttachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * mainAttachTransform.rotation);

        transform.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        transform.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    private void SubGrabUpdate()
    {
        Transform interactorAttachTransform = subInteractor.GetAttachTransform(this);
        Pose interactorAttachPose = new Pose(interactorAttachTransform.position, interactorAttachTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        // Calculate offset of the grab interactable's position relative to its attach transform
        var attachOffset = thisTransformPose.position - subAttachTransform.position;

        // Compute the new target world pose
        // Transform that offset direction from world space to local space of the transform it's relative to.
        // It will be applied to the interactor's attach position using the orientation of the Interactor's attach transform.
        var positionOffset = subAttachTransform.InverseTransformDirection(attachOffset);
        var rotationOffset = Quaternion.Inverse(Quaternion.Inverse(thisTransformPose.rotation) * subAttachTransform.rotation);

        transform.position = (interactorAttachPose.rotation * positionOffset) + interactorAttachPose.position;
        transform.rotation = (interactorAttachPose.rotation * rotationOffset);
    }

    private void MultiGrabUpdate()
    {
        Transform mainInteractorTransform = mainInteractor.GetAttachTransform(this);
        Transform subInteractorTransform = subInteractor.GetAttachTransform(this);

        Pose mainInteractorAttachPose = new Pose(mainInteractorTransform.position, mainInteractorTransform.rotation);
        Pose subInteractorAttachPose = new Pose(subInteractorTransform.position, subInteractorTransform.rotation);
        Pose thisTransformPose = new Pose(transform.position, transform.rotation);

        var attachOffset = thisTransformPose.position - mainAttachTransform.position;

        Quaternion targetRotation;
        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(subInteractorAttachPose.position - mainInteractorAttachPose.position);
        }
        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(subInteractorAttachPose.position - mainInteractorAttachPose.position, mainInteractorAttachPose.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(subInteractorAttachPose.position - mainInteractorAttachPose.position, subInteractorAttachPose.up);
        }

        var forward = (subInteractorTransform.position - mainInteractorTransform.position).normalized;

        var averageRight = Vector3.Slerp(mainInteractorTransform.right, subInteractorTransform.right, 0.5f);
        var up = Vector3.Slerp(mainInteractorTransform.up, subInteractorTransform.up, 0.5f);

        var crossUp = Vector3.Cross(forward, averageRight);
        var angleDiff = Mathf.PingPong(Vector3.Angle(up, forward), 90f);
        up = Vector3.Slerp(crossUp, up, angleDiff / 90f);

        var crossRight = Vector3.Cross(up, forward);
        up = Vector3.Cross(forward, crossRight);

        //Quaternion newRotation;
        //if (twoHandedRotationMode == TwoHandedRotationMode.FirstHandDirectedTowardsSecondHand)
        //{
        //    newRotation = interactor0Transform.rotation * Quaternion.FromToRotation(m_StartHandleBar, newHandleBar);
        //}
        //else if (twoHandedRotationMode == TwoHandedRotationMode.TwoHandedAverage)
        //{
        //    var forward = (interactor1Transform.position - interactor0Transform.position).normalized;

        //    var averageRight = Vector3.Slerp(interactor0Transform.right, interactor1Transform.right, 0.5f);
        //    var up = Vector3.Slerp(interactor0Transform.up, interactor1Transform.up, 0.5f);

        //    var crossUp = Vector3.Cross(forward, averageRight);
        //    var angleDiff = Mathf.PingPong(Vector3.Angle(up, forward), 90f);
        //    up = Vector3.Slerp(crossUp, up, angleDiff / 90f);

        //    var crossRight = Vector3.Cross(up, forward);
        //    up = Vector3.Cross(forward, crossRight);

        //    if (firstFrameSinceTwoHandedGrab)
        //    {
        //        firstFrameSinceTwoHandedGrab = false;
        //    }
        //    else
        //    {
        //        // We also keep track of whether the up vector was pointing up or down previously, to allow for objects to be flipped through a series of rotations
        //        // Such as a 180 degree rotation on the y, followed by a 180 degree rotation on the x
        //        if (Vector3.Dot(up, m_LastTwoHandedUp) <= 0f)
        //        {
        //            up = -up;
        //        }
        //    }

        //    m_LastTwoHandedUp = up;

        //    var twoHandedRotation = Quaternion.LookRotation(forward, up);

        //    // Given that this rotation method doesn't really consider the first interactor's start rotation, we have to remove the offset pose computed on grab. 
        //    newRotation = twoHandedRotation * Quaternion.Inverse(m_OffsetPose.rotation);
        //}
        //else
        //{
        //    newRotation = interactor0Transform.rotation;
        //}

        //adjustedInteractorPosition = interactor0Transform.position;
        //adjustedInteractorRotation = newRotation;
        //return;

        /*
         * XRGeneralGrabTransformer->ComputeAdjustedInteractorPose »Æ¿Œ
         */
    }


    bool oldKinematic;
    bool oldGravity;
    float oldDrag;
    float oldAngularDrag;
    private void OnGrab()
    {
        oldKinematic = rigid.isKinematic;
        oldGravity = rigid.useGravity;
        oldDrag = rigid.drag;
        oldAngularDrag = rigid.angularDrag;

        rigid.isKinematic = true;
        rigid.useGravity = false;
        rigid.drag = 0f;
        rigid.angularDrag = 0f;
    }

    private void OnDrop()
    {
        rigid.isKinematic = oldKinematic;
        rigid.useGravity = oldGravity;
        rigid.drag = oldDrag;
        rigid.angularDrag = oldAngularDrag;
    }
}
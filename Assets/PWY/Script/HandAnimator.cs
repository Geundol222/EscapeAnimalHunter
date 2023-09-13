using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XInput;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] Animator left_Animator;
    [SerializeField] Animator right_Animator;

    public void Phone_Grip(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // 오른손 애니메이터 조정
            Debug.Log("오른손으로 잡음");
            right_Animator.SetLayerWeight(1, 1);
        }
        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // 왼손 애니메이터 조정
            Debug.Log("왼손으로 잡음");
            left_Animator.SetLayerWeight(1, 1);
        }
    }

    public void Phone_Drop(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // 오른손 애니메이터 조정
            Debug.Log("오른손으로 놓음");
            right_Animator.SetLayerWeight(1, 0);
        }
        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // 왼손 애니메이터 조정
            Debug.Log("왼손으로 놓음");
            left_Animator.SetLayerWeight(1, 0);
        }
    }
}

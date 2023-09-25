using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] Animator left_Animator;
    [SerializeField] Animator right_Animator;


    #region // 핸드폰 잡았을 때 애니메이션
    // 잡기
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

    // 놓기
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
    #endregion

    #region // 총 장전할때 애니메이션
    // 장전중
    public void GunReload_Enter(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // 오른손으로 장전손잡이 잡기
            Debug.Log("오른손장전중");
            right_Animator.SetLayerWeight(2, 1);
        }

        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // 왼손으로 장전손잡이 잡기
            Debug.Log("왼손장전중");
            left_Animator.SetLayerWeight(2, 1);
        }
    }

    // 장전 끝
    public void GunReload_Exit(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.gameObject.name == "Right Direct Interactor")
        {
            // 오른손에서 장전손잡이 놓기
            Debug.Log("오른손 장전끝");
            right_Animator.SetLayerWeight(2, 0);

        }

        else if (args.interactorObject.transform.gameObject.name == "Left Direct Interactor")
        {
            // 왼손에서 장전손잡이 놓기
            Debug.Log("왼손장전 끝");
            left_Animator.SetLayerWeight(2, 0);
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;

public class CarInteractor : MonoBehaviour
{

    [SerializeField] GameObject car_Enter_Point;    // 운전석의 위치
    [SerializeField] GameObject Operator;           // 플레이어
    [SerializeField] GameObject locomotion_Move;    // 플레이어의 로코모션에서 Move오브젝트
    [SerializeField] GameObject exit_Point;         // 내리는 곳의 위치
    [SerializeField] CharacterController xROrigin_CharacterController;  // 플레이어의 캐릭터 컨트롤러
    [SerializeField] Animator fade_Animator;        // 타고 내릴떄의 페이드 애니메이션 xr 오리진의 카메라 하위자식으로 있음

    public bool car;                                // 탑승 유무

    private void Update()
    {
        if (car)    // 운전석의 위치와 회전을 동기화함
        {
            Operator.transform.position = car_Enter_Point.transform.position;

            float operatorX = car_Enter_Point.transform.rotation.eulerAngles.x;
            float operatorZ = car_Enter_Point.transform.rotation.eulerAngles.z;

            Vector3 newRotation = new Vector3(operatorX, Operator.transform.rotation.eulerAngles.y, operatorZ);

            Operator.transform.rotation = Quaternion.Euler(newRotation);
        }
    }

    public void CarCoordinate()
    {
        fade_Animator.Play("Fade");
        BoardingConfirmation();
        Car_Front_View();
    }


    public void Car_Front_View() // 탑승시 차 앞을 바라봄
    {
        float operatorY = car_Enter_Point.transform.rotation.eulerAngles.y;
        Vector3 newRotation = new Vector3(Operator.transform.rotation.eulerAngles.x, operatorY, Operator.transform.rotation.z);
        Operator.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void Car_Enter() // 차량에 탑승시 탑승버튼을 눌러도 반응없게 함
    {
        if (!car)   // 탑승상태가 아님
        {
            car = true;
            CarCoordinate();
        }
        else        // 탑승한 상태
        {
            return;
        }
    }

    public void BoardingConfirmation()  // 차량 탑승시 로코모션의 Move와 플레이어의 캐릭터 컨트롤러를 끔
    {
        if (car)    // 끄기(탑승)
        {
            locomotion_Move.SetActive(false);
            xROrigin_CharacterController.enabled = false;
        }

        else        // 켜기(내림)
        {
            locomotion_Move.SetActive(true);
            xROrigin_CharacterController.enabled = true;
        }
    }

    public void CarExit()   // 차에서 내리기 위한 함수
    {
        if (car)    // 차에 탄 상태
        {
            car = false;
            BoardingConfirmation();
            fade_Animator.Play("Fade");
            Operator.transform.position = exit_Point.transform.position;
            Operator.transform.rotation = exit_Point.transform.rotation;
        }
        else    // 차에 내린상태면 작동안하게
        {
            return;
        }

    }
}

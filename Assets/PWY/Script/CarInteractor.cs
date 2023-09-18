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

    [SerializeField] GameObject car_Enter_Point;    // �������� ��ġ
    [SerializeField] GameObject Operator;           // �÷��̾�
    [SerializeField] GameObject locomotion_Move;    // �÷��̾��� ���ڸ�ǿ��� Move������Ʈ
    [SerializeField] GameObject exit_Point;         // ������ ���� ��ġ
    [SerializeField] CharacterController xROrigin_CharacterController;  // �÷��̾��� ĳ���� ��Ʈ�ѷ�
    [SerializeField] Animator fade_Animator;        // Ÿ�� �������� ���̵� �ִϸ��̼� xr �������� ī�޶� �����ڽ����� ����

    public bool isInCar;                                // ž�� ����

    private void Update()
    {
        if (isInCar)    // �������� ��ġ�� ȸ���� ����ȭ��
        {
            Operator.transform.position = car_Enter_Point.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(car_Enter_Point.transform.forward);

            Operator.transform.rotation = lookRotation;
        }
    }

    public void CarCoordinate()
    {
        fade_Animator.Play("Fade");
        BoardingConfirmation();
        Car_Front_View();
    }


    public void Car_Front_View() // ž�½� �� ���� �ٶ�
    {
        float operatorY = car_Enter_Point.transform.rotation.eulerAngles.y;
        Vector3 newRotation = new Vector3(Operator.transform.rotation.eulerAngles.x, operatorY, Operator.transform.rotation.z);
        Operator.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void Car_Interactor() // ������ ž�½� ž�¹�ư�� ������ �������� ��
    {
        if (!isInCar)   // ž�»��°� �ƴ�
        {
            isInCar = true;
            CarCoordinate();
        }
        else        // ž���� ����
        {
            isInCar = false;
            BoardingConfirmation();
            fade_Animator.Play("Fade");
            Operator.transform.position = exit_Point.transform.position;
            Operator.transform.rotation = exit_Point.transform.rotation;
        }
    }

    public void BoardingConfirmation()  // ���� ž�½� ���ڸ���� Move�� �÷��̾��� ĳ���� ��Ʈ�ѷ��� ��
    {
        if (isInCar)    // ����(ž��)
        {
            xROrigin_CharacterController.enabled = false;
        }
        else        // �ѱ�(����)
        {
            xROrigin_CharacterController.enabled = true;
        }
    }
}

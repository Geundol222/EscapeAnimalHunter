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

    public bool car;                                // ž�� ����

    private void Update()
    {
        if (car)    // �������� ��ġ�� ȸ���� ����ȭ��
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


    public void Car_Front_View() // ž�½� �� ���� �ٶ�
    {
        float operatorY = car_Enter_Point.transform.rotation.eulerAngles.y;
        Vector3 newRotation = new Vector3(Operator.transform.rotation.eulerAngles.x, operatorY, Operator.transform.rotation.z);
        Operator.transform.rotation = Quaternion.Euler(newRotation);
    }

    public void Car_Enter() // ������ ž�½� ž�¹�ư�� ������ �������� ��
    {
        if (!car)   // ž�»��°� �ƴ�
        {
            car = true;
            CarCoordinate();
        }
        else        // ž���� ����
        {
            return;
        }
    }

    public void BoardingConfirmation()  // ���� ž�½� ���ڸ���� Move�� �÷��̾��� ĳ���� ��Ʈ�ѷ��� ��
    {
        if (car)    // ����(ž��)
        {
            locomotion_Move.SetActive(false);
            xROrigin_CharacterController.enabled = false;
        }

        else        // �ѱ�(����)
        {
            locomotion_Move.SetActive(true);
            xROrigin_CharacterController.enabled = true;
        }
    }

    public void CarExit()   // ������ ������ ���� �Լ�
    {
        if (car)    // ���� ź ����
        {
            car = false;
            BoardingConfirmation();
            fade_Animator.Play("Fade");
            Operator.transform.position = exit_Point.transform.position;
            Operator.transform.rotation = exit_Point.transform.rotation;
        }
        else    // ���� �������¸� �۵����ϰ�
        {
            return;
        }

    }
}

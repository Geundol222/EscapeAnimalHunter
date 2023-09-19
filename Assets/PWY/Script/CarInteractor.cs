using UnityEngine;

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

    public void Car_Interaction() // ������ ž�½� ž�¹�ư�� ������ �������� ��
    {
        if (!car)   // ž�»��°� �ƴ�
        {
            car = true;
            CarCoordinate();
        }
        else        // ž���� ����
        {
            car = false;
            BoardingConfirmation();
            fade_Animator.Play("Fade");
            Operator.transform.position = exit_Point.transform.position;
            Operator.transform.rotation = exit_Point.transform.rotation;
        }
    }

    public void BoardingConfirmation()  // ���� ž�½� ���ڸ���� Move�� �÷��̾��� ĳ���� ��Ʈ�ѷ��� ��
    {
        if (car)    // ����(ž��)
        {
            //locomotion_Move.SetActive(false);
            xROrigin_CharacterController.enabled = false;
        }

        else        // �ѱ�(����)
        {
            //locomotion_Move.SetActive(true);
            xROrigin_CharacterController.enabled = true;
        }
    }

}
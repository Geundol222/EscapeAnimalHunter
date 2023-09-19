using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Setting : MonoBehaviour
{
    /// <summary>
    /// Activate_List ���� 
    /// 0. Phone_On_BackGround 
    /// 1. Phone_Off_BackGround
    /// 2. Center Display
    /// 3. Map_Activate
    /// 4. Challenge_Activate
    /// 5. Account_Activate
    /// 6. Phonelist_Activate
    /// 7. Camera_Activate
    /// 8. Gallery_Activate
    /// 9. Setting_Activate
    /// </summary>
    [SerializeField] List<GameObject> Activate_List;
    /// <summary>
    /// Setting_Acivate ����
    /// 0.Setting_Menu
    /// 1.Back_Image
    /// 2.Audio_Text
    /// 3.Audio_Master
    /// 4.Audio_Vibration
    /// 5.Audio_Camera
    /// 6.Brightness_Text
    /// 7.Brightness.X
    /// 8.Brightness.Y
    /// 9.PhoneSize_Text
    /// 10.PhoneSize_X
    /// 11.PhoneSize_Y
    /// </summary>
    [SerializeField] List<GameObject> Setting_Acivate;


    #region Ȩȭ�� ����
    public void Home()
    {
        // ����
        Activate_List[0].SetActive(true);     // ��׶��� (On)
        Activate_List[2].SetActive(true);     // ����
        // ����
        Activate_List[1].SetActive(false);    // ��׶��� (Off)
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����
        Debug.Log("off");
    }

    public void Map()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[3].SetActive(true);     // ��
        //����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����
    }

    public void Challenge()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[4].SetActive(true);     // ç����
        // ����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����
    }

    public void Account()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[5].SetActive(true);     // ����
        // ����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����
    }

    public void Phonelist()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        //����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[7].SetActive(true);     // ī�޶�
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����
    }

    public void Camera()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[7].SetActive(true);     // ī�޶�
        //����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[8].SetActive(false);    // ������
        Activate_List[9].SetActive(false);    // ����

    }

    public void Gallery()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[8].SetActive(true);     // ������
        // ����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[9].SetActive(false);    // ����
    }

    public void Setting()
    {
        // ����
        Activate_List[1].SetActive(true);     // ��׶��� (Off)
        Activate_List[9].SetActive(true);     // ����
        //����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[2].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ������
    }
    #endregion

    public void Audio_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[2].SetActive(true);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(true);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(true);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(true);    // 5.Audio_Camera
        // ����
        Setting_Acivate[0].SetActive(false);    // 0.Setting_Menu
        Setting_Acivate[6].SetActive(false);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(false);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(false);    // 8.Brightness.Y
        Setting_Acivate[9].SetActive(false);    // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(false);   // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(false);   // 11.PhoneSize_Y
    }

    public void Brightness_Setting()
    {
        // ����
        // ����
        Setting_Acivate[0].SetActive(false);   // 0.Setting_Menu
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[2].SetActive(false);   // 2.Audio_Text
        Setting_Acivate[3].SetActive(false);   // 3.Audio_Master
        Setting_Acivate[4].SetActive(false);   // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(false);   // 5.Audio_Camera
        Setting_Acivate[6].SetActive(true);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(true);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(true);    // 8.Brightness.Y
        Setting_Acivate[9].SetActive(false);   // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(false);  // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(false);  // 11.PhoneSize_Y
    }

    public void PhoneSize_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[9].SetActive(true);    // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(true);   // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(true);   // 11.PhoneSize_Y
        // ����
        Setting_Acivate[0].SetActive(false);    // 0.Setting_Menu
        Setting_Acivate[2].SetActive(false);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(false);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(false);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(false);    // 5.Audio_Camera
        Setting_Acivate[6].SetActive(false);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(false);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(false);    // 8.Brightness.Y
    }

    public void BackGround_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        // ����
        Setting_Acivate[0].SetActive(false);    // 0.Setting_Menu
        Setting_Acivate[2].SetActive(false);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(false);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(false);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(false);    // 5.Audio_Camera
        Setting_Acivate[6].SetActive(false);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(false);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(false);    // 8.Brightness.Y
        Setting_Acivate[9].SetActive(false);    // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(false);   // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(false);   // 11.PhoneSize_Y
    }


}


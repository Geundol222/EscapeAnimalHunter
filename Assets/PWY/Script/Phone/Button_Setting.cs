using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Setting : MonoBehaviour
{
    /// <summary>
    /// Activate_List ���� 
    /// 0.Phone_BackGround 
    /// 1.Center Display
    /// 2.Map_Activate
    /// 3.Challenge_Activate
    /// 4.Account_Activate
    /// 5.Phonelist_Activate
    /// 6.Camera_Activate
    /// 7.Gallery_Activate
    /// 8.Setting_Activate
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
    /// 12.BackGround_Center
    /// 13.BackGrounds_Center
    /// 14.Audio_Car
    /// </summary>
    [SerializeField] List<GameObject> Setting_Acivate;

    /// <summary>
    /// 0.Audio_Setting
    /// 1.Brightness_Setting
    /// 2.PhoneSize_Setting
    /// 3.BackGround_Setting
    /// 4.Clock_Setting
    /// </summary>
    [SerializeField] List<GameObject> Setting_Butting;

    /// <summary>
    /// 0.Colling_Activate
    /// 1.Answer The Phone_Quit
    /// 2.
    /// 3.
    /// </summary>
    [SerializeField] List<GameObject> calling_Setting;

    /// <summary>
    /// 0.Clock_Style_Chang
    /// 1.Round_Select_Outline
    /// 2.Digital_Select_Outline
    /// 3.Round_Time_ Clock
    /// 4.Digital_Tiem_Clock
    /// 5.Phone_BackGround_Upper/Under
    /// 6.Harp_Seal
    /// 7.BonoBono
    /// 8.Null
    /// 9.Null_Outline
    /// 10.Round_Harp_Seal
    /// 11.Round_Harp_Outline
    /// 12.Round_BonoBono
    /// 13.BonoBono_Outline
    /// 14.Moon
    /// 15.Moon_Outline
    /// 16.Neogulmaen
    /// 17.Neogulmaen_Outline
    /// 18.Round_Moon
    /// 19.Round_Neogulmaen
    /// </summary>
    [SerializeField] List<GameObject> clock_Setting;

    #region �� ���� �ѹ��� ���� �� �ѱ�
    private void Activate_List_Allfalse()
    {
        Activate_List[0].SetActive(false);
        Activate_List[1].SetActive(false);
        Activate_List[2].SetActive(false);
        Activate_List[3].SetActive(false);
        Activate_List[4].SetActive(false);
        Activate_List[5].SetActive(false);
        Activate_List[6].SetActive(false);
        Activate_List[7].SetActive(false);
        Activate_List[8].SetActive(false);

    }
    private void Activate_List_Alltrue()
    {
        Activate_List[0].SetActive(true);
        Activate_List[1].SetActive(true);
        Activate_List[2].SetActive(true);
        Activate_List[3].SetActive(true);
        Activate_List[4].SetActive(true);
        Activate_List[5].SetActive(true);
        Activate_List[6].SetActive(true);
        Activate_List[7].SetActive(true);
        Activate_List[8].SetActive(true);

    }

    private void Setting_Acivate_Allfalse()
    {
        Setting_Acivate[0].SetActive(false); 
        Setting_Acivate[1].SetActive(false); 
        Setting_Acivate[2].SetActive(false); 
        Setting_Acivate[3].SetActive(false); 
        Setting_Acivate[4].SetActive(false); 
        Setting_Acivate[5].SetActive(false); 
        Setting_Acivate[6].SetActive(false); 
        Setting_Acivate[7].SetActive(false); 
        Setting_Acivate[8].SetActive(false); 
        Setting_Acivate[9].SetActive(false); 
        Setting_Acivate[10].SetActive(false);
        Setting_Acivate[11].SetActive(false);
        Setting_Acivate[12].SetActive(false);
        Setting_Acivate[13].SetActive(false);
        Setting_Acivate[14].SetActive(false);
    }
    private void Setting_Acivate_Alltrue()
    {
        Setting_Acivate[0].SetActive(true);
        Setting_Acivate[1].SetActive(true);
        Setting_Acivate[2].SetActive(true);
        Setting_Acivate[3].SetActive(true);
        Setting_Acivate[4].SetActive(true);
        Setting_Acivate[5].SetActive(true);
        Setting_Acivate[6].SetActive(true);
        Setting_Acivate[7].SetActive(true);
        Setting_Acivate[8].SetActive(true);
        Setting_Acivate[9].SetActive(true);
        Setting_Acivate[10].SetActive(true);
        Setting_Acivate[11].SetActive(true);
        Setting_Acivate[12].SetActive(true);
        Setting_Acivate[13].SetActive(false);
        Setting_Acivate[14].SetActive(false);
    }

    private void Setting_Butting_Allfalse()
    {
        Setting_Butting[0].SetActive(false);
        Setting_Butting[1].SetActive(false);
        Setting_Butting[2].SetActive(false);
        Setting_Butting[3].SetActive(false);
        Setting_Butting[4].SetActive(false);
    }
    private void Setting_Butting_Alltrue()
    {
        Setting_Butting[0].SetActive(true);
        Setting_Butting[1].SetActive(true);
        Setting_Butting[2].SetActive(true);
        Setting_Butting[3].SetActive(true);
        Setting_Butting[4].SetActive(true);
    }


    private void calling_Setting_Allfalse()
    {
        calling_Setting[0].SetActive(false);
        calling_Setting[1].SetActive(false);
    }
    private void calling_Setting_Alltrue()
    {
        calling_Setting[0].SetActive(true);
        calling_Setting[1].SetActive(true);
    }
    #endregion


    #region Ȩȭ�� ����
    public void Home()
    {
        // ����
        Activate_List[0].SetActive(true);     // ��׶���        
        Activate_List[1].SetActive(true);     // ����             
        // ����                                                   
        Activate_List[2].SetActive(false);    // ��               
        Activate_List[3].SetActive(false);    // ç����            
        Activate_List[4].SetActive(false);    // ����             
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��        
        Activate_List[6].SetActive(false);    // ī�޶�            
        Activate_List[7].SetActive(false);    // ������                
        Activate_List[8].SetActive(false);    // ����
        Setting_Butting[4].SetActive(false);
        // �� ���ֱ�
        Setting_Acivate_Allfalse();
        calling_Setting_Allfalse();
    }

    public void Map()
    {
        // ����
        Activate_List[2].SetActive(true);     // ��
        //����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ����
    }

    public void Challenge()
    {
        // ����
        Activate_List[3].SetActive(true);     // ç����
        // ����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ����
    }

    public void Account()
    {
        // ����
        Activate_List[4].SetActive(true);     // ����
        // ����
        Activate_List[0].SetActive(false);    // ��׶��� (On)
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ����
    }

    public void Phonelist()
    {
        // ����
        Activate_List[5].SetActive(true);    // ��ȭ��ȣ��
        //����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ����
    }

    public void Camera()
    {
        // ����
        Activate_List[6].SetActive(true);     // ī�޶�
        //����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ����
    }

    public void Gallery()
    {
        // ����
        Activate_List[7].SetActive(true);     // ������
        // ����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[8].SetActive(false);    // ����
    }

    public void Setting()
    {
        // ����
        Activate_List[8].SetActive(true);     // ����
        //����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        // �߰� �޴� Ȱ��ȭ
        Setting_Acivate[0].SetActive(true);   // 0.Setting_Menu
    }

    public void Exit()
    {
        // ����
        Activate_List[9].SetActive(true);     // ����
        //����
        Activate_List[0].SetActive(false);    // ��׶���
        Activate_List[1].SetActive(false);    // ����
        Activate_List[2].SetActive(false);    // ��
        Activate_List[3].SetActive(false);    // ç����
        Activate_List[4].SetActive(false);    // ����
        Activate_List[5].SetActive(false);    // ��ȭ��ȣ��
        Activate_List[6].SetActive(false);    // ī�޶�
        Activate_List[7].SetActive(false);    // ������
        Activate_List[8].SetActive(false);    // ������
    }
    #endregion

    #region Setting �޴�
    public void Back_Setting()
    {
        //����
        Setting_Butting[0].SetActive(true);    // 0.Audio_Setting
        Setting_Butting[1].SetActive(true);    // 1.Brightness_Setting
        Setting_Butting[2].SetActive(true);    // 2.PhoneSize_Setting
        Setting_Butting[3].SetActive(true);    // 3.BackGround_Setting
        Setting_Acivate[0].SetActive(true);    // 0.Setting_Menu
        // ����
        Setting_Acivate[1].SetActive(false);    // 1.Back_Image
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
        Setting_Acivate[12].SetActive(false);   // 12.BackGround_Center
        Setting_Acivate[13].SetActive(false);   // 13.BackGround_Center
        Setting_Acivate[14].SetActive(false);   // 14.Audio_Car
        Setting_Butting[4].SetActive(false);

    }

    public void Audio_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[2].SetActive(true);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(true);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(true);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(true);    // 5.Audio_Camera
        Setting_Acivate[12].SetActive(true);   // 12.BackGround_Center
        Setting_Acivate[14].SetActive(true);  // 14.Audio_Car
        // ����
        Setting_Acivate[0].SetActive(false);   // 0.Setting_Menu
        Setting_Acivate[6].SetActive(false);   // 6.Brightness_Text
        Setting_Acivate[7].SetActive(false);   // 7.Brightness.X
        Setting_Acivate[8].SetActive(false);   // 8.Brightness.Y
        Setting_Acivate[9].SetActive(false);   // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(false);  // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(false);  // 11.PhoneSize_Y
        Setting_Acivate[13].SetActive(false);  // 13.BackGround_Center
        Setting_Butting[4].SetActive(false);
    }

    public void Brightness_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[6].SetActive(true);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(true);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(true);    // 8.Brightness.Y
        Setting_Acivate[12].SetActive(true);   // 12.BackGround_Center
        // ����
        Setting_Acivate[0].SetActive(false);   // 0.Setting_Menu
        Setting_Acivate[2].SetActive(false);   // 2.Audio_Text
        Setting_Acivate[3].SetActive(false);   // 3.Audio_Master
        Setting_Acivate[4].SetActive(false);   // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(false);   // 5.Audio_Camera
        Setting_Acivate[9].SetActive(false);   // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(false);  // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(false);  // 11.PhoneSize_Y
        Setting_Acivate[13].SetActive(false);  // 13.BackGround_Center
        Setting_Acivate[14].SetActive(false);   // 14.Audio_Car
        Setting_Butting[4].SetActive(false);
    }

    public void PhoneSize_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[9].SetActive(true);    // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(true);   // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(true);   // 11.PhoneSize_Y
        Setting_Acivate[12].SetActive(true);   // 12.BackGround_Center  
        // ����
        Setting_Acivate[0].SetActive(false);    // 0.Setting_Menu
        Setting_Acivate[2].SetActive(false);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(false);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(false);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(false);    // 5.Audio_Camera
        Setting_Acivate[6].SetActive(false);    // 6.Brightness_Text
        Setting_Acivate[7].SetActive(false);    // 7.Brightness.X
        Setting_Acivate[8].SetActive(false);    // 8.Brightness.Y
        Setting_Acivate[13].SetActive(false);   // 13.BackGround_Center
        Setting_Acivate[14].SetActive(false);   // 14.Audio_Car
        Setting_Butting[4].SetActive(false);
    }

    public void BackGround_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);     // 1.Back_Image
        Setting_Acivate[12].SetActive(true);    // 12.BackGround_Center
        Setting_Acivate[13].SetActive(true);    // 13.BackGround_Center
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
        Setting_Acivate[14].SetActive(false);   // 14.Audio_Car
        Setting_Butting[4].SetActive(false);
    }

    public void Clock_Setting()
    {
        // ����
        Setting_Acivate[1].SetActive(true);     // 1.Back_Image
        Setting_Butting[4].SetActive(true);
        Setting_Acivate[12].SetActive(true);    // 12.BackGround_Center
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
        Setting_Acivate[13].SetActive(false);    // 13.BackGround_Center
        Setting_Acivate[14].SetActive(false);   // 14.Audio_Car
    }
    #endregion

    #region ��ȭ�ɱ�
    public void Calling()                        
    {
        // ����
        calling_Setting[0].SetActive(true);
        calling_Setting[1].SetActive(true);
        // ����
        Activate_List[0].SetActive(false);
        Activate_List[5].SetActive(false);
    }                     
    
    // ��ȭ����                                   
    public void Phone_Quit()                     
    {   // ����                                  
        calling_Setting[1].SetActive(true);
        Activate_List[0].SetActive(true);
        Activate_List[1].SetActive(true);
        // ����                                  
        calling_Setting[0].SetActive(false);

    }
    #endregion

    #region �ð�ٲٱ�
    public void Digital_Clock_Change()
    {
        // ����
        clock_Setting[0].SetActive(true);
        clock_Setting[2].SetActive(true);
        clock_Setting[4].SetActive(true);
        // ����
        clock_Setting[1].SetActive(false);
        clock_Setting[3].SetActive(false);
        clock_Setting[5].SetActive(false);
        clock_Setting[6].SetActive(false);
        clock_Setting[7].SetActive(false);
        clock_Setting[8].SetActive(false);
        clock_Setting[10].SetActive(false);
        clock_Setting[12].SetActive(false);
        clock_Setting[14].SetActive(false);
        clock_Setting[16].SetActive(false);
        clock_Setting[18].SetActive(false);
        clock_Setting[19].SetActive(false);
    }

    public void Round_Clock_Change()
    {
        // ����
        clock_Setting[0].SetActive(true);
        clock_Setting[1].SetActive(true);
        clock_Setting[3].SetActive(true);
        clock_Setting[5].SetActive(true);
        clock_Setting[6].SetActive(true);
        clock_Setting[7].SetActive(true);
        clock_Setting[8].SetActive(true);
        clock_Setting[10].SetActive(true);
        clock_Setting[12].SetActive(true);
        clock_Setting[14].SetActive(true);
        clock_Setting[16].SetActive(true);
        clock_Setting[18].SetActive(true);
        clock_Setting[19].SetActive(true);
        // ����
        clock_Setting[2].SetActive(false);
        clock_Setting[4].SetActive(false);
    }

    public void Roune_Clock_Null_Style()
    {
        // ����
        clock_Setting[9].SetActive(true);
        // ����
        clock_Setting[6].SetActive(false);
        clock_Setting[7].SetActive(false);
        clock_Setting[11].SetActive(false);
        clock_Setting[13].SetActive(false);
        clock_Setting[14].SetActive(false);
        clock_Setting[15].SetActive(false);
        clock_Setting[16].SetActive(false);
        clock_Setting[17].SetActive(false);
    }

    public void Roune_Clock_Harp_Style()
    {
        // ����
        clock_Setting[5].SetActive(true);
        clock_Setting[6].SetActive(true);
        clock_Setting[11].SetActive(true);
        // ����
        clock_Setting[7].SetActive(false);
        clock_Setting[9].SetActive(false);
        clock_Setting[13].SetActive(false);
        clock_Setting[14].SetActive(false);
        clock_Setting[15].SetActive(false);
        clock_Setting[16].SetActive(false);
        clock_Setting[17].SetActive(false);

    }

    public void Round_Clock_BonoBono_Style()
    {
        // ����
        clock_Setting[5].SetActive(true);
        clock_Setting[7].SetActive(true);
        clock_Setting[13].SetActive(true);
        // ����
        clock_Setting[6].SetActive(false);
        clock_Setting[9].SetActive(false);
        clock_Setting[11].SetActive(false);
        clock_Setting[14].SetActive(false);
        clock_Setting[15].SetActive(false);
        clock_Setting[16].SetActive(false);
        clock_Setting[17].SetActive(false);

    }
    public void Round_Clock_Moon_Style()
    {
        // ����
        clock_Setting[5].SetActive(true);
        clock_Setting[14].SetActive(true);
        clock_Setting[15].SetActive(true);
        // ����
        clock_Setting[6].SetActive(false);
        clock_Setting[7].SetActive(false);
        clock_Setting[9].SetActive(false);
        clock_Setting[11].SetActive(false);
        clock_Setting[13].SetActive(false);
        clock_Setting[16].SetActive(false);
        clock_Setting[17].SetActive(false);
    }
    public void Round_Clock_Neogulmaen_Style()
    {
        // ����
        clock_Setting[5].SetActive(true);
        clock_Setting[16].SetActive(true);
        clock_Setting[17].SetActive(true);
        // ����
        clock_Setting[6].SetActive(false);
        clock_Setting[7].SetActive(false);
        clock_Setting[9].SetActive(false);
        clock_Setting[11].SetActive(false);
        clock_Setting[13].SetActive(false);
        clock_Setting[14].SetActive(false);
        clock_Setting[15].SetActive(false);


    }
    #endregion
}


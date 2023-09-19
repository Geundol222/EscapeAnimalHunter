using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Setting : MonoBehaviour
{
    /// <summary>
    /// Activate_List ¼ø¼­ 
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
    /// Setting_Acivate ¼ø¼­
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


    #region È¨È­¸é ¼³Á¤
    public void Home()
    {
        // ÄÑÁü
        Activate_List[0].SetActive(true);     // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(true);     // ¼¾ÅÍ
        // ²¨Áü
        Activate_List[1].SetActive(false);    // ¹é±×¶ó¿îµå (Off)
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤
        Debug.Log("off");
    }

    public void Map()
    {
        // ÄÑÁø
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[3].SetActive(true);     // ¸Ê
        //²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤
    }

    public void Challenge()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[4].SetActive(true);     // Ã§¸°Áö
        // ²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤
    }

    public void Account()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[5].SetActive(true);     // °èÁÂ
        // ²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤
    }

    public void Phonelist()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        //²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[7].SetActive(true);     // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤
    }

    public void Camera()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[7].SetActive(true);     // Ä«¸Þ¶ó
        //²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[8].SetActive(false);    // °¶·¯¸®
        Activate_List[9].SetActive(false);    // ¼³Á¤

    }

    public void Gallery()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[8].SetActive(true);     // °¶·¯¸®
        // ²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[9].SetActive(false);    // ¼³Á¤
    }

    public void Setting()
    {
        // ÄÑÁü
        Activate_List[1].SetActive(true);     // ¹é±×¶ó¿îµå (Off)
        Activate_List[9].SetActive(true);     // ¼³Á¤
        //²¨Áü
        Activate_List[0].SetActive(false);    // ¹é±×¶ó¿îµå (On)
        Activate_List[2].SetActive(false);    // ¼¾ÅÍ
        Activate_List[3].SetActive(false);    // ¸Ê
        Activate_List[4].SetActive(false);    // Ã§¸°Áö
        Activate_List[5].SetActive(false);    // °èÁÂ
        Activate_List[6].SetActive(false);    // ÀüÈ­¹øÈ£ºÎ
        Activate_List[7].SetActive(false);    // Ä«¸Þ¶ó
        Activate_List[8].SetActive(false);    // °¶·¯¸®
    }
    #endregion

    public void Audio_Setting()
    {
        // ÄÑÁü
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[2].SetActive(true);    // 2.Audio_Text
        Setting_Acivate[3].SetActive(true);    // 3.Audio_Master
        Setting_Acivate[4].SetActive(true);    // 4.Audio_Vibration
        Setting_Acivate[5].SetActive(true);    // 5.Audio_Camera
        // ²¨Áü
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
        // ÄÑÁü
        // ²¨Áü
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
        // ÄÑÁü
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        Setting_Acivate[9].SetActive(true);    // 9.PhoneSize_Text
        Setting_Acivate[10].SetActive(true);   // 10.PhoneSize_X
        Setting_Acivate[11].SetActive(true);   // 11.PhoneSize_Y
        // ²¨Áü
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
        // ÄÑÁü
        Setting_Acivate[1].SetActive(true);    // 1.Back_Image
        // ²¨Áü
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


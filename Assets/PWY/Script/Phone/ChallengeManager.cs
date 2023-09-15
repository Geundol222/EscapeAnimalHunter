using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeManager: MonoBehaviour
{
    GameObject p;
    public TMP_Text[] challengeName;         // 포획할 동물 이름 핸드폰에 나타내기
    
    public Image[] challenge_Uncheck_Image;   // 업적이 하나도 안돼있을때 !모양
    public Image[] challenge_Progress_Image;  // 업적 진행도
    public Image[] challenge_Check_Image;     // 업적을 다 완료했을 때 V 모양
    // 돈 업적
    // public GameObject[] colling_Enble;
    // public GameObject[] colling_Dissble;
    // 전화 돈
    // private Bear bear;
    // private Animal animal;
    // public Button colling;
    // public Toggle _colling;
    // 동물 죽을 때마다 카운팅

    //TestBearPWY testBearpwy;
    public void T123()
    {
        
    }
    public bool Bear_Challenge_Attainment; 
    public bool moos_Challenge_Attainment;

    // public bool name_Challenge_Attainment;
    // public bool Name_Challenge_Attainment;


    private void Start()
    {
        StartSetText();
    }

    private void Colling_Enable_Disable()
    {
        
        //colling_Enble[0].SetActive(true);
        //
        //colling_Dissble[0].SetActive(false);
        //colling_Dissble[1].SetActive(false);
        //colling_Dissble[2].SetActive(false);
        //colling_Dissble[3].SetActive(false);
        //colling_Dissble[4].SetActive(false);
        //colling_Dissble[5].SetActive(false);
        //colling_Dissble[6].SetActive(false);
        //colling_Dissble[7].SetActive(false);
        //colling_Dissble[8].SetActive(false);
        //colling_Dissble[9].SetActive(false);
    }
    
    public void StartSetText()
    {
        
        challengeName[0].text = "<size=80%>Bear</size>";    // 곰
        challengeName[1].text = "<size=80%>Moos</size>";    // 무스
        //challengeName[2].text = "<size=80%>Moos</size>";
        //challengeName[3].text = "<size=80%>Deer Capture</size>";
        //challenge_Uncheck_Image[2].SetActive(true);
        //challenge_Uncheck_Image[3].SetActive(true);
        // 시작
        challenge_Uncheck_Image[0].enabled = true;
        challenge_Uncheck_Image[1].enabled = true;
        // 진행도
        challenge_Progress_Image[0].enabled = false;
        challenge_Progress_Image[1].enabled = false;
        // 완료
        challenge_Check_Image[0].enabled=false;
        challenge_Check_Image[1].enabled=false;
    }

    private void Update()
    {
        Bear_Challenge();
        Moos_Challenge();
    }

    public void Bear_Challenge()
    {
        if (Bear_Challenge_Attainment)
        {
            challenge_Uncheck_Image[0].enabled = false;
            challenge_Progress_Image[0].enabled = true;
            if (challenge_Progress_Image[0].fillAmount == 0.25f)
            {

            }
            if (challenge_Progress_Image[0].fillAmount == 0.5f)
            {

            }
            if (challenge_Progress_Image[0].fillAmount == 0.75f)
            {

            }
            if (challenge_Progress_Image[0].fillAmount == 1f)
            {
                challengeName[0].text = "<size=80%><color=red><s>Bear Capture</color></s></size>";
                challenge_Progress_Image[0].enabled = false;
                challenge_Check_Image[0].enabled = true;
            }
            Colling_Enable_Disable();
        }
    }
    
    public void Moos_Challenge() 
    {
        if(moos_Challenge_Attainment)
        {
            challenge_Uncheck_Image[1].enabled = false;
            challenge_Progress_Image[1].enabled = true;
            if (challenge_Progress_Image[1].fillAmount == 0.25f)
            {

            }
            if (challenge_Progress_Image[1].fillAmount == 0.5f)
            {

            }
            if (challenge_Progress_Image[1].fillAmount == 0.75f)
            {

            }
            if (challenge_Progress_Image[1].fillAmount == 1f)
            {
                challengeName[1].text = "<size=80%><color=red><s>Moos Capture</color></s></size>";
                challenge_Progress_Image[1].enabled = false;
                challenge_Check_Image[0].enabled = true;
            }
            Colling_Enable_Disable();
        }
    }

    #region // 아직 추가 안됨
    /*public void Name_Challenge() 
    {
        if (name_Challenge_Attainment)
        {
            challengeName[2].text = "<size=80%><color=red><s>Moos</color></s></size>";
            challenge_Uncheck_Image[2].SetActive(false);
            challenge_Check_Image[2].SetActive(true);
        }

        else
        {
            return;
        }
    }*/

    /*public void Name_Challenge4() 
    {
        if(Name_Challenge_Attainment)
        {
            challengeName[2].text = "<size=80%><color=red><s>Moos</color></s></size>";
            challenge_Uncheck_Image[3].SetActive(false);
            challenge_Check_Image[3].SetActive(true);
        }

        else
        {
            return;
        }
    }*/

    // 업적에 따라 인테리어 소품이 늘어남
    // ex.)사슴잡으면 침대와 테이블이 생김
    // Start is called before the first frame update
    #endregion
}

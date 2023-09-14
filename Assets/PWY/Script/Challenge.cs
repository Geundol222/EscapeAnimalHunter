using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public TMP_Text[] challengeName;

    public GameObject[] challenge_Uncheck_Image;
    public GameObject[] challenge_Check_Image;

    public GameObject[] colling_Enble;
    public GameObject[] colling_Dissble;

    public GameObject graveyard;

    public bool Bear_Challenge_Attainment;
    public bool moos_Challenge_Attainment;

    public bool name_Challenge_Attainment;
    public bool Name_Challenge_Attainment;

    private void Update()
    {
       // Did(die);
    }

    private void Start()
    {
        
        StartSetText();
    }

    private void Colling_Enable_Disable()
    {
        
        colling_Enble[0].SetActive(true);

        colling_Dissble[0].SetActive(false);
        colling_Dissble[1].SetActive(false);
        colling_Dissble[2].SetActive(false);
        colling_Dissble[3].SetActive(false);
        colling_Dissble[4].SetActive(false);
        colling_Dissble[5].SetActive(false);
        colling_Dissble[6].SetActive(false);
        colling_Dissble[7].SetActive(false);
        colling_Dissble[8].SetActive(false);
        colling_Dissble[9].SetActive(false);
    }

    public void StartSetText()
    {
        
        challengeName[0].text = "<size=80%>Deer Capture</size>";
        challengeName[1].text = "<size=80%>Bear</size>";
        challengeName[2].text = "<size=80%>Moos</size>";
        //challengeName[3].text = "<size=80%>Deer Capture</size>";
        challenge_Uncheck_Image[0].SetActive(true);
        challenge_Uncheck_Image[1].SetActive(true);
        challenge_Uncheck_Image[2].SetActive(true);
        //challenge_Uncheck_Image[3].SetActive(true);
    }

    public void Bear_Challenge()
    {
        if(Bear_Challenge_Attainment)
        {
            challengeName[0].text = "<size=80%><color=red><s>Bear Capture</color></s></size>";
            challenge_Uncheck_Image[0].SetActive(false);
            challenge_Check_Image[0].SetActive(true);

            Colling_Enable_Disable();


        }

        else
        {
            return;    
        }

    }

    public void Moos_Challenge() 
    {
        if(moos_Challenge_Attainment)
        {
            challengeName[1].text = "<size=80%><color=red><s>Moos Capture</color></s></size>";
            challenge_Uncheck_Image[1].SetActive(false);
            challenge_Check_Image[1].SetActive(true);

            Colling_Enable_Disable();
        }

        else 
        { 
            return; 
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

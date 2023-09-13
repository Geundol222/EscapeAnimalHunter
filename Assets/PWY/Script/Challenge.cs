using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public TMP_Text[] challengeName = new TMP_Text[3];
    public GameObject[] challenge_Uncheck_Image = new GameObject[3];
    public GameObject[] challenge_Check_Image = new GameObject[3];
    //public string challengeName;

    public bool challengeAttainment1;
    public bool challengeAttainment2;
    //public bool challengeAttainment3;
    //public bool challengeAttainment4;

    private void Start()
    {
        StartSetText();
        Challenge1();
        Challenge2();
    }

    public void StartSetText()
    {

        challengeName[0].text = "<size=80%>Deer Capture</size>";
        challengeName[1].text = "<size=80%>Bear</size>";
        //challengeName[2].text = "<size=80%>Deer Capture</size>";
        //challengeName[3].text = "<size=80%>Deer Capture</size>";
        challenge_Uncheck_Image[0].SetActive(true);
        challenge_Uncheck_Image[1].SetActive(true);
        challenge_Uncheck_Image[2].SetActive(true);
    }

    public void Challenge1()
    {
        challengeAttainment1 = true;
        
        if(challengeAttainment1)
        {
            challengeName[0].text = "<size=80%><color=red><s>Deer Capture</color></s></size>";
        }

        else
        {
            return;    
        }

    }

    public void Challenge2() 
    {
        challengeAttainment2 = true;

        if(challengeAttainment2)
        {
            challengeName[1].text = "<size=80%><color=red><s>Bear</color></s></size>";
        }

        else 
        { 
            return; 
        }
    }

    /*public void Challenge3() 
    {
        challengeAttainment3 = true;
        if (challengeAttainment3)
        {

        }

        else
        {
            return;
        }
    }

    public void Challenge4() 
    {
        challengeAttainment4 = true;
        if(challengeAttainment4)
        {

        }

        else
        {
            return;
        }
    }

    // 업적에 따라 인테리어 소품이 늘어남
    // ex.)사슴잡으면 침대와 테이블이 생김
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}

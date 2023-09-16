using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Challenge : MonoBehaviour
{
    public TMP_Text[] challengeName;         // 포획할 동물 이름 핸드폰에 나타내기

    public Image[] challenge_Dissble_Image;   // 업적이 하나도 안돼있을때 !모양
    public Image[] challenge_Progress_Image;  // 업적 진행도
    public Image[] challenge_Enble_Image;     // 업적을 다 완료했을 때 V 모양

    private void Start()
    {
        StartSetText();
    }

    public void StartSetText()
    {

        challengeName[0].text = "<size=80%>Bear</size>";    // 곰
        challengeName[1].text = "<size=80%>Moos</size>";    // 무스
        
        // 시작
        challenge_Dissble_Image[0].enabled = true;
        challenge_Dissble_Image[1].enabled = true;
        // 진행도
        challenge_Progress_Image[0].enabled = true;
        challenge_Progress_Image[1].enabled = true;
        // 완료
        challenge_Enble_Image[0].enabled = false;
        challenge_Enble_Image[1].enabled = false;

    }

    public void Bear_Challenge(ChallengeManager challengeManager)
    {
        if (challengeManager.bearCount == challengeManager.bear_Challenge_Exit)
        {

            challenge_Progress_Image[0].enabled = true;
            challenge_Progress_Image[0].fillAmount += 0.175f;
        }
        else if (challengeManager.bear_Challenge_Exit <= 11)
        {
            challengeName[0].text = "<size=80%><color=red><s>Bear Capture</color></s></size>";
            challenge_Progress_Image[0].fillAmount = 1f;
            challenge_Dissble_Image[0].enabled = false;
            challenge_Enble_Image[0].enabled = true;
            return;
        }
    }

    public void Moos_Challenge(ChallengeManager challengeManager) 
    {
        if (challengeManager.bearCount == challengeManager.bear_Challenge_Exit)
        {
            challenge_Progress_Image[1].enabled = true;
            challenge_Progress_Image[1].fillAmount += 0.175f;
        }
        else if (challengeManager.moose_Challenge_Exit <= 11)
        {
            challengeName[1].text = "<size=80%><color=red><s>Moos Capture</color></s></size>";
            challenge_Progress_Image[1].fillAmount = 1f;
            challenge_Dissble_Image[1].enabled = false;
            challenge_Enble_Image[1].enabled = true;
            return;
        }
    }

    //private void Colling_Enable_Disable()
    //{
    //    
    //    colling_Enble[0].SetActive(true);
    //
    //    colling_Dissble[0].SetActive(false);
    //    colling_Dissble[1].SetActive(false);
    //    colling_Dissble[2].SetActive(false);
    //    colling_Dissble[3].SetActive(false);
    //    colling_Dissble[4].SetActive(false);
    //    colling_Dissble[5].SetActive(false);
    //    colling_Dissble[6].SetActive(false);
    //    colling_Dissble[7].SetActive(false);
    //    colling_Dissble[8].SetActive(false);
    //    colling_Dissble[9].SetActive(false);
    //}
}

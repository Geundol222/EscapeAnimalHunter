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
    //public TMP_Text[] challengeName;         // 포획할 동물 이름 핸드폰에 나타내기
    ////
    ////public Image[] challenge_Dissble_Image;   // 업적이 하나도 안돼있을때 !모양
    ////public Image[] challenge_Progress_Image;  // 업적 진행도
    ////public Image[] challenge_Enble_Image;     // 업적을 다 완료했을 때 V 모양

    //[SerializeField] List<Image> challenge_Achive_Images;     // 도전 성취 이미지
    //[SerializeField] List<Image> challenge_Progress_Images;   // 업적 진행도 이미지

    //[SerializeField] Sprite challenge_UnChack_Image;       // 업적 전 이미지 (!)
    //[SerializeField] Sprite challenge_Chack_Image;         // 업적 후 이미지 (V)

    //[SerializeField] AudioSource challenge_AudioSource;
    //[SerializeField] AudioClip challenge_Vibrate;

    //private int bearComplete;
    //private int mooseComplete;

    //private float bearAmountValue;
    //private float mooseAmountValue;

    //private void Awake()
    //{
    //    bearComplete = DataManager.Challenge.bear_Challenge_Exit;   // 5
    //    mooseComplete = DataManager.Challenge.moose_Challenge_Exit; // 8

    //    bearAmountValue = 1 / bearComplete;     // 1 / 5
    //    mooseAmountValue = 1 / mooseComplete;   // 1 / 8
    //}

    //private void OnEnable()
    //{
    //    InitSetting();
    //    SetProgressAmount();
    //}

    //public void InitSetting()
    //{
    //    // 시작시 텍스처 이름과 크기
    //    challengeName[0].text = "<size=80%>Bear</size>";    // 곰
    //    challengeName[1].text = "<size=80%>Moos</size>";    // 무스

    //    for (int i = 0; i < challenge_Achive_Images.Count; i++)
    //    {
    //        challenge_Achive_Images[i].enabled = true;
    //        challenge_Progress_Images[i].enabled = true;

    //        if (DataManager.Challenge.AchiveChallengeCheck(challengeName[i].text))
    //            challenge_Achive_Images[i].sprite = challenge_Chack_Image;
    //        else
    //            challenge_Achive_Images[i].sprite = challenge_UnChack_Image;
    //    }
    //}

    //// 업적 진행도
    //private void SetProgressAmount()
    //{
    //    for (int i = 0; i < challengeName.Length; i++)
    //    {
    //        if (challengeName[i].text == "Bear")    // 챌린지의 이름이 Bear과 같으면
    //        {
    //            if (DataManager.Challenge.AchiveChallengeCheck(challengeName[i].text))
    //            {
    //                challengeName[i].text = "<size=80%><color=red><s>Bear Capture</color></s></size>";
    //                challenge_Progress_Images[i].fillAmount = 1f;
    //                challenge_Achive_Images[i].sprite = challenge_Chack_Image;
    //            }
    //            else
    //            {
    //                challenge_Progress_Images[i].fillAmount = DataManager.Challenge.bearCount + bearAmountValue;
    //            }
    //        }

    //        else if (challengeName[i].text == "Moose") //  챌린지의 이름이 Moose와 같으면
    //        {
    //            if (DataManager.Challenge.AchiveChallengeCheck(challengeName[i].text))
    //            {
    //                challengeName[i].text = "<size=80%><color=red><s>Moose Capture</color></s></size>";
    //                challenge_Progress_Images[i].fillAmount = 1f;   // 진행도 게이지를 다 채움
    //                challenge_Achive_Images[i].sprite = challenge_Chack_Image; // 이미지도 V로 바꿈
    //            }
    //            else
    //            {
    //                challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue; 
    //            }
    //        }
    //    }
    //}

    //private void Challenge_Audio()
    //{
    //    if (DataManager.Challenge.bearCount == 1)
    //    {
    //        challenge_AudioSource.clip = challenge_Vibrate;
    //        challenge_AudioSource.Play();
    //    }
    //    else if (DataManager.Challenge.bearCount == 4)
    //    {
    //        challenge_AudioSource.clip = challenge_Vibrate;
    //        challenge_AudioSource.Play();
    //    }

    //    else if (DataManager.Challenge.mooseCount == 1)
    //    {
    //        challenge_AudioSource.clip = challenge_Vibrate;
    //        challenge_AudioSource.Play();
    //    }
    //    else if (DataManager.Challenge.mooseCount == 8)
    //    {
    //        challenge_AudioSource.clip = challenge_Vibrate;
    //        challenge_AudioSource.Play();
    //    }

    //}

    /*public void StartSetText()
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
    }*/

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

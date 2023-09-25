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
    //
    //public Image[] challenge_Dissble_Image;   // 업적이 하나도 안돼있을때 !모양
    //public Image[] challenge_Progress_Image;  // 업적 진행도
    //public Image[] challenge_Enble_Image;     // 업적을 다 완료했을 때 V 모양

    [SerializeField] List<Image> challenge_Achive_Images;     // 도전 성취 이미지
    [SerializeField] List<Image> challenge_Progress_Images;   // 업적 진행도 이미지

    [SerializeField] Sprite challenge_UnChack_Image;       // 업적 전 이미지 (!)
    [SerializeField] Sprite challenge_Chack_Image;         // 업적 후 이미지 (V)

    [SerializeField] AudioSource challenge_AudioSource;
    [SerializeField] AudioClip challenge_Vibrate;

    private int bearComplete;
    private int mooseComplete;
    private int deerComplete;
    private int tigerComplete;

    private float bearAmountValue;
    private float mooseAmountValue;
    private float deerAmountValue;
    private float tigerAmountValue;

    private void Awake()
    {
        bearComplete = DataManager.Challenge.bear_Challenge_Exit;   // 5
        mooseComplete = DataManager.Challenge.moose_Challenge_Exit; // 8
        deerComplete = DataManager.Challenge.deer_Challenge_Exit;
        tigerComplete = DataManager.Challenge.tiger_Challenge_Exit;

        bearAmountValue = 1 / bearComplete;     // 1 / 5
        mooseAmountValue = 1 / mooseComplete;   // 1 / 8
        deerAmountValue = 1 / deerComplete;
        tigerAmountValue = 1 / tigerComplete;
    }

    private void OnEnable()
    {
        InitSetting();
        SetProgressAmount();
    }

    public void InitSetting()
    {
        // 시작시 텍스처 이름과 크기
        challengeName[0].text = "<size=80%>Bear</size>";    // 곰
        challengeName[1].text = "<size=80%>Moose</size>";    // 무스
        challengeName[2].text = "<size=80%>Deer</size>";
        challengeName[3].text = "<size=80%>Tiger</size>";

        for (int i = 0; i < challenge_Achive_Images.Count; i++)
        {
            challenge_Achive_Images[i].enabled = true;
            challenge_Progress_Images[i].enabled = true;

            if (DataManager.Challenge.unLockRewardDic[challengeName[i].text])
                challenge_Achive_Images[i].sprite = challenge_Chack_Image;
            else
                challenge_Achive_Images[i].sprite = challenge_UnChack_Image;
        }
    }


    #region 업적 진행도
    private void SetProgressAmount()
    {
        for (int i = 0; i < challengeName.Length; i++)
        {
            if (challengeName[i].text == "Bear")    // 챌린지의 이름이 Bear과 같으면
            {
                if (DataManager.Challenge.unLockRewardDic["Bear"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Bear Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image;
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.bearCount + bearAmountValue;
                }
            }

            else if (challengeName[i].text == "Moose") //  챌린지의 이름이 Moose와 같으면
            {
                if (DataManager.Challenge.unLockRewardDic["Moose"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Moose Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // 진행도 게이지를 다 채움
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // 이미지도 V로 바꿈
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }

            else if (challengeName[i].text == "Deer") //  챌린지의 이름이 Moose와 같으면
            {
                if (DataManager.Challenge.unLockRewardDic["Deer"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Deer Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // 진행도 게이지를 다 채움
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // 이미지도 V로 바꿈
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }

            else if (challengeName[i].text == "Tiger") //  챌린지의 이름이 Moose와 같으면
            {
                if (DataManager.Challenge.unLockRewardDic["Tiger"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Tiger Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // 진행도 게이지를 다 채움
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // 이미지도 V로 바꿈
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }
        }
    }
    #endregion

    #region 처음으로 포획한 순간과 마지막 업적 깨면 진동오기
    private void Challenge_Audio(string animalName)
    {
        if (animalName == "Bear")
        {
            if (DataManager.Challenge.bearCount == 1)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
            else if (DataManager.Challenge.bearCount == 4)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
        }

        else if (animalName == "Moose")
        {
            if (DataManager.Challenge.mooseCount == 1)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
            else if (DataManager.Challenge.mooseCount == 8)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
        }

        else if (animalName == "Deer")
        {
            if (DataManager.Challenge.deerCount == 1)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
            else if (DataManager.Challenge.deerCount == 10)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
        }

        else if (animalName == "Tiger")
        {
            if (DataManager.Challenge.tigerCount == 1)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
            else if (DataManager.Challenge.tigerCount == 2)
            {
                challenge_AudioSource.clip = challenge_Vibrate;
                challenge_AudioSource.Play();
            }
        }
    }
    #endregion
}


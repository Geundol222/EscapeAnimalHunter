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
    //public TMP_Text[] challengeName;         // ��ȹ�� ���� �̸� �ڵ����� ��Ÿ����
    ////
    ////public Image[] challenge_Dissble_Image;   // ������ �ϳ��� �ȵ������� !���
    ////public Image[] challenge_Progress_Image;  // ���� ���൵
    ////public Image[] challenge_Enble_Image;     // ������ �� �Ϸ����� �� V ���

    //[SerializeField] List<Image> challenge_Achive_Images;     // ���� ���� �̹���
    //[SerializeField] List<Image> challenge_Progress_Images;   // ���� ���൵ �̹���

    //[SerializeField] Sprite challenge_UnChack_Image;       // ���� �� �̹��� (!)
    //[SerializeField] Sprite challenge_Chack_Image;         // ���� �� �̹��� (V)

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
    //    // ���۽� �ؽ�ó �̸��� ũ��
    //    challengeName[0].text = "<size=80%>Bear</size>";    // ��
    //    challengeName[1].text = "<size=80%>Moos</size>";    // ����

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

    //// ���� ���൵
    //private void SetProgressAmount()
    //{
    //    for (int i = 0; i < challengeName.Length; i++)
    //    {
    //        if (challengeName[i].text == "Bear")    // ç������ �̸��� Bear�� ������
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

    //        else if (challengeName[i].text == "Moose") //  ç������ �̸��� Moose�� ������
    //        {
    //            if (DataManager.Challenge.AchiveChallengeCheck(challengeName[i].text))
    //            {
    //                challengeName[i].text = "<size=80%><color=red><s>Moose Capture</color></s></size>";
    //                challenge_Progress_Images[i].fillAmount = 1f;   // ���൵ �������� �� ä��
    //                challenge_Achive_Images[i].sprite = challenge_Chack_Image; // �̹����� V�� �ٲ�
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

        challengeName[0].text = "<size=80%>Bear</size>";    // ��
        challengeName[1].text = "<size=80%>Moos</size>";    // ����
        
        // ����
        challenge_Dissble_Image[0].enabled = true;
        challenge_Dissble_Image[1].enabled = true;
        // ���൵
        challenge_Progress_Image[0].enabled = true;
        challenge_Progress_Image[1].enabled = true;
        // �Ϸ�
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

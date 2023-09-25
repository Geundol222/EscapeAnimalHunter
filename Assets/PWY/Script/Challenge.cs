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
    public TMP_Text[] challengeName;         // ��ȹ�� ���� �̸� �ڵ����� ��Ÿ����
    //
    //public Image[] challenge_Dissble_Image;   // ������ �ϳ��� �ȵ������� !���
    //public Image[] challenge_Progress_Image;  // ���� ���൵
    //public Image[] challenge_Enble_Image;     // ������ �� �Ϸ����� �� V ���

    [SerializeField] List<Image> challenge_Achive_Images;     // ���� ���� �̹���
    [SerializeField] List<Image> challenge_Progress_Images;   // ���� ���൵ �̹���

    [SerializeField] Sprite challenge_UnChack_Image;       // ���� �� �̹��� (!)
    [SerializeField] Sprite challenge_Chack_Image;         // ���� �� �̹��� (V)

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
        // ���۽� �ؽ�ó �̸��� ũ��
        challengeName[0].text = "<size=80%>Bear</size>";    // ��
        challengeName[1].text = "<size=80%>Moose</size>";    // ����
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


    #region ���� ���൵
    private void SetProgressAmount()
    {
        for (int i = 0; i < challengeName.Length; i++)
        {
            if (challengeName[i].text == "Bear")    // ç������ �̸��� Bear�� ������
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

            else if (challengeName[i].text == "Moose") //  ç������ �̸��� Moose�� ������
            {
                if (DataManager.Challenge.unLockRewardDic["Moose"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Moose Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // ���൵ �������� �� ä��
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // �̹����� V�� �ٲ�
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }

            else if (challengeName[i].text == "Deer") //  ç������ �̸��� Moose�� ������
            {
                if (DataManager.Challenge.unLockRewardDic["Deer"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Deer Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // ���൵ �������� �� ä��
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // �̹����� V�� �ٲ�
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }

            else if (challengeName[i].text == "Tiger") //  ç������ �̸��� Moose�� ������
            {
                if (DataManager.Challenge.unLockRewardDic["Tiger"])
                {
                    challengeName[i].text = "<size=80%><color=red><s>Tiger Capture</color></s></size>";
                    challenge_Progress_Images[i].fillAmount = 1f;   // ���൵ �������� �� ä��
                    challenge_Achive_Images[i].sprite = challenge_Chack_Image; // �̹����� V�� �ٲ�
                }
                else
                {
                    challenge_Progress_Images[i].fillAmount = DataManager.Challenge.mooseCount + mooseAmountValue;
                }
            }
        }
    }
    #endregion

    #region ó������ ��ȹ�� ������ ������ ���� ���� ��������
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


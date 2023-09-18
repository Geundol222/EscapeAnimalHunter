using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager: MonoBehaviour
{
    // �ʱ�ȭ�ϸ� �ȵǴ°�
    public int bearCount = 0;
    public int mooseCount = 0;


    public int bear_Challenge_Exit;     // ������ �Ϸ�Ǵ� ũ��
    public int moose_Challenge_Exit;    // ������ �Ϸ�Ǵ� ũ��

    Dictionary<string, bool> unLockRewardDic;
    AudioSource audioSource;

    private void Awake()
    {
        bear_Challenge_Exit = 5;
        moose_Challenge_Exit = 8;

        unLockRewardDic = new Dictionary<string, bool> // ���� ������ bool�� false�� ����
        {
            { "Bear", false },
            { "Moose", false },
            { "Chair", false },
            { "Bed", false },
            { "C", false },
            { "D", false },
            { "E", false },
            { "F", false },
            { "G", false },
            { "H", false }
        };
    }

    #region ���� ī����
    public void AnimalCount(Animal animal)    // ���� ���� ������ ī���õ�
    {
        if (animal.name == "Bear")
        {
            bearCount++;
            bear_Challenge_Exit++;
        }
        if (animal.name == "Moose")
        {
            mooseCount++;
            moose_Challenge_Exit++;
        }
    }
    #endregion

    #region ����
    public bool AchiveChallengeCheck(string animalName)
    {
        if (unLockRewardDic.ContainsKey(animalName))
        {
            if (animalName == "Bear")
            {
                if (bearCount == 1)
                    return true;
                if (bearCount == bear_Challenge_Exit)
                    return true;
                else
                    return false;
            }

            else if (animalName == "Moose")
            {
                if (mooseCount == 1)
                    return true;
                if (mooseCount == moose_Challenge_Exit)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }
    #endregion

    #region ������
    public bool UnLockRewardCheck(string rewaedName)
    {
        if (unLockRewardDic.ContainsKey(rewaedName))
        {
            // ��
            if (rewaedName == "" && bearCount == 1)
            {
                // GameManager.Data.InitMoney(10);
                return true;
            }
            else if (rewaedName == "" && bearCount == 2)
                return true;
            else if (rewaedName == "" && bearCount == 3)
                return true;
            else if (rewaedName == "" && bearCount == 4)
            {
                // GameManager.Data.InitMoney(40);
                return true;
            }

            // ����
            else if (rewaedName == "" && mooseCount == 2)
                return true;
            else if (rewaedName == "" && mooseCount == 4)
                return true;
            else if (rewaedName == "" && mooseCount == 8)
                return true;
            else if (rewaedName == "" && mooseCount == 10)
                return true;
        }
        return false;
    }
    #endregion

}

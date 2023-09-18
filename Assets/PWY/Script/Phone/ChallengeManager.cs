using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager: MonoBehaviour
{
    // 초기화하면 안되는것
    public int bearCount = 0;
    public int mooseCount = 0;


    public int bear_Challenge_Exit;     // 업적이 완료되는 크기
    public int moose_Challenge_Exit;    // 업적이 완료되는 크기

    Dictionary<string, bool> unLockRewardDic;
    AudioSource audioSource;

    private void Awake()
    {
        bear_Challenge_Exit = 5;
        moose_Challenge_Exit = 8;

        unLockRewardDic = new Dictionary<string, bool> // 곰과 무스의 bool을 false로 만듬
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

    #region 동물 카운팅
    public void AnimalCount(Animal animal)    // 죽은 곰과 무스만 카운팅됨
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

    #region 업적
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

    #region 보상목록
    public bool UnLockRewardCheck(string rewaedName)
    {
        if (unLockRewardDic.ContainsKey(rewaedName))
        {
            // 곰
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

            // 무스
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

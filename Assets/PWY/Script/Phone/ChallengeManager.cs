using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChallengeManager: MonoBehaviour
{
    // 초기화하면 안되는것
    public int bearCount = 0;
    public int mooseCount = 0;
    public int deerCount = 0;
    public int tigerCount = 0;

    public int bear_Challenge_Exit;     // 업적이 완료되는 크기
    public int moose_Challenge_Exit;    // 업적이 완료되는 크기
    public int deer_Challenge_Exit;     // 업적이 완료되는 크기
    public int tiger_Challenge_Exit;    // 업적이 완료되는 크기

    public Dictionary<string, bool> unLockRewardDic {get; private set;}
    public bool startcalling;

    List<GameObject> dieAnimals = new List<GameObject>();

    private void Awake()
    {
        bear_Challenge_Exit = 4;
        moose_Challenge_Exit = 8;
        deer_Challenge_Exit = 10;
        tiger_Challenge_Exit = 2;

        startcalling = true;

        unLockRewardDic = new Dictionary<string, bool> // 곰과 무스의 bool을 false로 만듬
        {
            // 동물
            { "<size=80%>Bear</size>", false },
            { "<size=80%>Moose</size>", false },
            { "<size=80%>Deer</size>", false },
            { "<size=80%>Tiger</size>", false },
            // 보상
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
    public void AnimalCount(Animal animal)
    {
        if (animal.name == "Bear")
        {
            bearCount++;
        }
        if (animal.name == "Moose")
        {
            mooseCount++;
        }
        if (animal.name == "Deer")
        {
            deerCount++;
        }
        if (animal.name == "Tiger")
        {
            tigerCount++;
        }
        dieAnimals.Add(animal.gameObject); 
    }
    #endregion

    #region 업적
    public void AchiveChallengeCheck(string animalName)
    {
        if (unLockRewardDic.ContainsKey(animalName))
        {
            if (animalName == "Bear")
            {
                if (bearCount >= bear_Challenge_Exit)
                {
                    bearCount = bear_Challenge_Exit;
                    unLockRewardDic["Bear"] = true;
                }
                else
                    unLockRewardDic["Bear"] = false;
            }

            else if (animalName == "Moose")
            {
                if (mooseCount >= moose_Challenge_Exit)
                {
                    mooseCount = moose_Challenge_Exit;
                    unLockRewardDic["Moose"] = true;
                }
                else
                    unLockRewardDic["Moose"] = false;
            }

            else if (animalName == "Deer")
            {
                if (deerCount >= deer_Challenge_Exit)
                {
                    deerCount = deer_Challenge_Exit;
                    unLockRewardDic["Deer"] = true;
                }
                else
                    unLockRewardDic["Deer"] = false;
            }

            else if (animalName == "Tiger")
            {
                if (tigerCount >= tiger_Challenge_Exit)
                {
                    tigerCount = tiger_Challenge_Exit;
                    unLockRewardDic["Tiger"] = true;
                }
                else
                    unLockRewardDic["Tiger"] = false;
            }
        }
    }
    #endregion

    #region 보상목록 확인
    public void UnLockRewardCheck(string rewardName)
    {
        if (unLockRewardDic.ContainsKey(rewardName))
        {
            // 곰
            if (rewardName == "Bear")
            {
                if (bearCount == 1)
                {
                    GameManager.Data.AddMoney(20);
                }
                else if (bearCount == 4)
                {
                    GameManager.Data.AddMoney(60);
                }
            }
            // 무스
            else if (rewardName == "Moose")
            {
                if (mooseCount == 1)
                {
                    GameManager.Data.AddMoney(10);
                }
                else if (mooseCount == 4)
                {
                    GameManager.Data.AddMoney(50);
                }
                else if (mooseCount == 8)
                {
                    GameManager.Data.AddMoney(100);
                }
            }
            // 사슴
            else if (rewardName == "Deer")
            {
                if (deerCount == 1)
                {
                    GameManager.Data.AddMoney(5);
                }
                else if (deerCount == 5)
                {
                    GameManager.Data.AddMoney(50);
                }
                else if (deerCount == 10)
                {
                    GameManager.Data.AddMoney(100);
                }
            }
            // 호랭이
            else if (rewardName == "Tiger")
            {
                if (tigerCount == 1)
                {
                    GameManager.Data.AddMoney(100);
                }
                else if (tigerCount == 2)
                {
                    GameManager.Data.AddMoney(200);
                }
            }
        }
    }
    #endregion

    #region 동물 잡아가기
    public void Capture()
    {
        foreach (GameObject animal in dieAnimals)
        {
            SpawnManager.Spawn.ReSpawnAniaml(animal);
        }
    }
    #endregion
}

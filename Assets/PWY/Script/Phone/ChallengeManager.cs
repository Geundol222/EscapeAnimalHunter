using UnityEngine;

public class ChallengeManager: MonoBehaviour
{
    // 초기화하면 안되는것
    public int bearCount = 0;
    public int mooseCount = 0;

    public int bear_Challenge_Exit = 1;
    public int moose_Challenge_Exit = 1;

    bool[] unLockReward;

    

    #region // 동물 카운팅
    public void AnimalCount(Animal animal)    // 곰의 isDie가 true면 bearCount가 올라감
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

    // 숫자가 변할때마다 호출
    // public int Bearcount
    // {
    //     get { return bearCount; }
    //     set
    //     {
    //         OnCurrentBearCount?.Invoke(value);
    //         bearCount = value;
    //     }
    // }
    // 
    // public int Moosecount
    // {
    //     get { return mooseCount; }
    //     set
    //     {
    //         OnCurrentMooseCount?.Invoke(value);
    //         mooseCount = value;
    //     }
    // }
    // public event UnityAction<int> OnCurrentBearCount;
    // public event UnityAction<int> OnCurrentMooseCount;
    #endregion
}

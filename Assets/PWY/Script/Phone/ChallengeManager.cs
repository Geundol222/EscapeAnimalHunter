using UnityEngine;

public class ChallengeManager: MonoBehaviour
{
    // �ʱ�ȭ�ϸ� �ȵǴ°�
    public int bearCount = 0;
    public int mooseCount = 0;

    public int bear_Challenge_Exit = 1;
    public int moose_Challenge_Exit = 1;

    bool[] unLockReward;

    

    #region // ���� ī����
    public void AnimalCount(Animal animal)    // ���� isDie�� true�� bearCount�� �ö�
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

    // ���ڰ� ���Ҷ����� ȣ��
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

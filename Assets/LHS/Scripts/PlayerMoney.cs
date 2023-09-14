using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    private int money;

    private void Awake()
    {
        money = GameManager.Data.Money;
    }

    /// <summary>
    /// Player의 Money에 변동이 있을 때, DataManager에서 해당 함수 호출, Player의 Money가 변동됨
    /// </summary>
    /// <param name="money"></param>
    public void InitMoney(int money)
    {
        this.money = money;
    }
}

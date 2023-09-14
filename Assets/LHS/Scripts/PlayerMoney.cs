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

    public void InitMoney(int money)
    {
        this.money = money;
    }
}

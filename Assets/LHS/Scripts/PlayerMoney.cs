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

    private void OnEnable()
    {
        GameManager.Data.OnCurrentMoneyChanged += ChangedMoney;
    }

    public void ChangedMoney(int cost)
    {
        money = cost;
    }

    private void OnDisable()
    {
        GameManager.Data.OnCurrentMoneyChanged -= ChangedMoney;
    }
}

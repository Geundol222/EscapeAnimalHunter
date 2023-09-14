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
    /// Player�� Money�� ������ ���� ��, DataManager���� �ش� �Լ� ȣ��, Player�� Money�� ������
    /// </summary>
    /// <param name="money"></param>
    public void InitMoney(int money)
    {
        this.money = money;
    }
}

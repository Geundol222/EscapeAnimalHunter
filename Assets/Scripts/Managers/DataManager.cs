using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    private int curMoney = 0;

    public void GetCost(int cost)
    {
        curMoney += cost;
    }

    public void RemoveCost(int cost)
    {
        curMoney -= cost;
    }
}
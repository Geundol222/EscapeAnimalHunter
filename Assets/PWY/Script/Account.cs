using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Account : MonoBehaviour
{
    /// <summary>
    /// ���� ���� ������ �ִ� ��
    /// 1�ʸ��� �ڸ�ƾ���� Ȯ��
    /// </summary>
    [SerializeField] TMP_Text my_Money_Text;
    public int getMoney;
    public bool a;
    private void Start()
    {
        StartCoroutine(AccountBalance());
    }

    public void GetMoney()
    {
        getMoney = GameManager.Data.Money;
    }

    IEnumerator AccountBalance()
    {
        while (true)
        {
            if (a)
            {
                my_Money_Text.text = $"{getMoney.ToString()}$";
                //my_Money_Text.text = $"{GameManager.Data.Money.ToString()} $ ";
                yield return new WaitForSeconds(1f);
            }

        }
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    [SerializeField] TMP_Text my_Money_Text;
    public bool collingMoney;

    private void Start()
    {
        my_Money_Text.text = $"{GameManager.Data.Money.ToString()}";
        //StartCoroutine(AccountBalance());
    }

    public void GetMoney()
    {
        collingMoney = true ;
    }

    private void Update()
    {
        Debug.Log(collingMoney);
        if (collingMoney)
        {
            my_Money_Text.text = $"{GameManager.Data.Money.ToString()}";
            collingMoney = false;
        }
    }

    IEnumerator AccountBalance()
    {
        if (!collingMoney)
        {
            {
                my_Money_Text.text = $"{GameManager.Data.Money.ToString()}";
                collingMoney= true;
                Debug.Log(collingMoney);
            }
            yield return null;
        }
    }
}


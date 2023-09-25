using System.Collections;
using TMPro;
using UnityEngine;

public class Account : MonoBehaviour
{
    [SerializeField] TMP_Text my_Money_Text;
    public bool collingMoney;

    private void Start()
    {
        my_Money_Text.text = $"{GameManager.Data.Money.ToString()}";
        //StartCoroutine(AccountBalance());
    }
    // 터치 이벤트 함수
    public void GetMoney()
    {
        collingMoney = true;
    }

    private void Update()
    {
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


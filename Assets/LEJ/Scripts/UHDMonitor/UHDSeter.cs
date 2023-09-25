using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UHDSeter : MonoBehaviour
{
    [SerializeField] GameObject car;
    
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text maxSpeedText;
    [SerializeField] GameObject durabilityGauge;

    [SerializeField] Material emissionRed;

    GameObject curLastGauge;

    int durabilityToTen;
    float blinkTime;

    private void Awake()
    {
        SetMaxSpeedText();
        SetSpeedText();
        SetDurabilityGauge();
    }

    private void OnEnable()
    {
        car.GetComponent<CarDriver>().OnMaxSpeedChanged += SetMaxSpeedText;
        car.GetComponent<CarDriver>().OnCurSpeedChanged += SetSpeedText;
        car.GetComponent<CarDamager>().OnCurHpChanged += SetDurabilityGauge;
    }

    private void OnDisable()
    {
        car.GetComponent<CarDriver>().OnMaxSpeedChanged -= SetMaxSpeedText;
        car.GetComponent<CarDriver>().OnCurSpeedChanged -= SetSpeedText;
        car.GetComponent<CarDamager>().OnCurHpChanged -= SetDurabilityGauge;
    }

    Coroutine blinkRoutine;

    private void Update()
    {
        if (DataManager.Car.carCurHP != 100 && DataManager.Car.carCurHP != 0)
        {
            if (DataManager.Car.carCurState == CarDataManager.GearState.Drive || DataManager.Car.carCurState == CarDataManager.GearState.Reverse)
            {
                blinkTime += Time.deltaTime;

                if (blinkTime >= 1f)
                {
                    if (FindLastGauge() == curLastGauge)
                        blinkRoutine = StartCoroutine(BlinkGaugeAtOnce(curLastGauge));
                    blinkTime = 0f;
                }
            }
        }
    }

    void SetSpeedText()
    {
        int curSpeed = (int)car.GetComponent<CarDriver>().CurSpeed;
        speedText.SetText(curSpeed.ToString());
    }

    void SetMaxSpeedText()
    {
        int maxSpeed = (int)car.GetComponent<CarDriver>().MaxSpeed;
        maxSpeedText.SetText(maxSpeed.ToString());
    }

    void SetDurabilityGauge()
    {
        durabilityToTen = car.GetComponent<CarDamager>().MaxHp / 10;
        int setActiveCount = 0;
        
        if (car.GetComponent<CarDamager>().CurHp <= 0)
        {
            SetActiveGaugeImages(0);
        }

        for (int i = 1; i < 11; i++)
        {
            if (car.GetComponent<CarDamager>().CurHp > i * durabilityToTen)
                setActiveCount++;
        }

        SetActiveGaugeImages(setActiveCount);
    }

    void SetActiveGaugeImages(int count)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i <= count)
                durabilityGauge.transform.GetChild(i).gameObject.SetActive(true);
            else
                durabilityGauge.transform.GetChild(i).gameObject.SetActive(false);

            if (car.GetComponent<CarDamager>().CurHp < durabilityToTen)
                durabilityGauge.transform.GetChild(0).gameObject.GetComponent<Image>().material = emissionRed;
            else
                durabilityGauge.transform.GetChild(0).gameObject.GetComponent<Image>().material = durabilityGauge.transform.GetChild(1).gameObject.GetComponent<Image>().material;

            if (DataManager.Car.carCurHP == 0)
                durabilityGauge.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public GameObject FindLastGauge()
    {
        int num = 0;

        for (int i = 0; i < durabilityGauge.transform.childCount; i++)
        {
            if (durabilityGauge.transform.GetChild(i) != null)
            {
                if (durabilityGauge.transform.GetChild(i).transform.gameObject.activeInHierarchy)
                    num = i;
            }
        }

        curLastGauge = durabilityGauge.transform.GetChild(num).transform.gameObject;
        return durabilityGauge.transform.GetChild(num).transform.gameObject;
    }

    IEnumerator BlinkGaugeAtOnce(GameObject obj)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(true);
    }
}

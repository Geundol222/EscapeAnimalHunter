using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UHDSeter : MonoBehaviour
{
    [SerializeField] GameObject car;
    
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text maxSpeedText;
    [SerializeField] GameObject durabilityGauge;

    [SerializeField] Material emissionRed;

    int durabilityToTen;

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

    void SetSpeedText()
    {
        speedText.SetText(car.GetComponent<CarDriver>().CurSpeed.ToString());
    }

    void SetMaxSpeedText()
    {
        maxSpeedText.SetText(car.GetComponent<CarDriver>().MaxSpeed.ToString());
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
        }
    }

}

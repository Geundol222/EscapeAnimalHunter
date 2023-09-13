using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarRepairCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text enableRepairText;
    [SerializeField] TMP_Text sliderText;
    [SerializeField] Slider repairSlider;
    [SerializeField] Button okButton;

    int cost;
    int enableRepairValue;
    int sliderValue;

    private void Awake()
    {
        cost = 0;
        enableRepairValue = 0;
        sliderValue = 0;

        costText.text = "0";
        enableRepairText.text = "0";
        sliderText.text = "0";
    }

    private void OnEnable()
    {
        okButton.gameObject.SetActive(false);

        // enableRepariValue = DataManager.Car.maxHp - DataManager.Car.curHp;

        enableRepairText.text = enableRepairValue.ToString();

        repairSlider.maxValue = enableRepairValue;

        StartCoroutine(SliderValueChangeRoutine());
    }

    IEnumerator SliderValueChangeRoutine()
    {
        while (true)
        {
            sliderValue = (int)repairSlider.value;
            cost += (sliderValue * 5);

            sliderText.text = sliderValue.ToString();
            costText.text = cost.ToString();

            okButton.gameObject.SetActive(sliderValue > 0 ? true : false);

            yield return new WaitForEndOfFrame();
        }
    }

    public void PressOkButton()
    {
        GameManager.Data.Money -= cost;
        // DataManager.Car.curHp += sliderValue;

        okButton.gameObject.SetActive(false);

        cost = 0;
        sliderValue = 0;
        // enableRepairValue = DataManager.Car.maxHp - DataManager.Car.curHp;

        costText.text = cost.ToString();
        sliderText.text = sliderValue.ToString();
        enableRepairText.text = enableRepairValue.ToString();

        repairSlider.value = 0;
        repairSlider.maxValue = enableRepairValue;
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        cost = 0;
        enableRepairValue = 0;
        sliderValue = 0;

        costText.text = "0";
        enableRepairText.text = "0";
        sliderText.text = "0";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

public class CarUpgradeCanvas : MonoBehaviour
{
    [SerializeField] GameObject durabilityMeter;
    [SerializeField] GameObject speedMeter;
    [SerializeField] Button okButton;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text speedPlusText;
    [SerializeField] TMP_Text duraPlusText;

    [SerializeField] Transform itemTransform;

    List<Image> durabilityImages;
    List<Image> speedImages;
    GameObject carObject;

    int speedPlus;
    int duraPlus;

    private void Awake()
    {
        durabilityImages = new List<Image>();
        speedImages = new List<Image>();

        speedPlus = 0;
        duraPlus = 0;

        costText.text = "0";
        speedPlusText.text = "0";
        duraPlusText.text = "0";

        for (int i = 0; i < durabilityMeter.transform.childCount; i++)
        {
            durabilityImages.Add(durabilityMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }

        for (int i = 0; i < speedMeter.transform.childCount; i++)
        {
            speedImages.Add(speedMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }
    }

    private void OnEnable()
    {
        okButton.gameObject.SetActive(false);

        carObject = GameManager.Resource.Instantiate<GameObject>("Prefabs/ItemCar", itemTransform.position, Quaternion.Euler(0, -90, 0), true);
    }

    private void IncreaseCost()
    {
        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    private void DecreaseCost()
    {
        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    public void DurabilityUp()
    {
        DataManager.Upgrade.DurabilityUp();

        if (duraPlus >= 90)
            duraPlus = 90;
        else
            duraPlus += 10;

        duraPlusText.text = duraPlus.ToString();

        okButton.gameObject.SetActive(DataManager.Upgrade.durabilityIndex > 0 ? true : false);

        durabilityImages[DataManager.Upgrade.durabilityIndex].color = Color.white;

        IncreaseCost();
    }

    public void DurabilityDown()
    {
        durabilityImages[DataManager.Upgrade.durabilityIndex].color = Color.black;

        DataManager.Upgrade.DurabilityDown();

        if (duraPlus <= 0)
            duraPlus = 0;
        else
            duraPlus -= 10;

        duraPlusText.text = duraPlus.ToString();

        if (DataManager.Upgrade.durabilityIndex <= 0)
        {
            if (DataManager.Upgrade.carSpeedIndex <= 0)
                okButton.gameObject.SetActive(false);

            durabilityImages[0].color = Color.white;
        }

        DecreaseCost();
    }

    public void SpeedUp()
    {
        DataManager.Upgrade.CarSpeedUp();

        if (speedPlus >= 45)
            speedPlus = 45;
        else
            speedPlus += 5;

        speedPlusText.text = speedPlus.ToString();

        okButton.gameObject.SetActive(DataManager.Upgrade.carSpeedIndex > 0 ? true : false);

        speedImages[DataManager.Upgrade.carSpeedIndex].color = Color.white;

        IncreaseCost();
    }

    public void SpeedDown()
    {
        speedImages[DataManager.Upgrade.carSpeedIndex].color = Color.black;

        DataManager.Upgrade.CarSpeedDown();

        if (speedPlus <= 0)
            speedPlus = 0;
        else
            speedPlus -= 5;

        speedPlusText.text = speedPlus.ToString();

        if (DataManager.Upgrade.carSpeedIndex <= 0)
        {
            if (DataManager.Upgrade.durabilityIndex <= 0)
                okButton.gameObject.SetActive(false);

            speedImages[0].color = Color.white;
        }

        DecreaseCost();
    }

    public void PressOkButton()
    {
        DataManager.Upgrade.applyCost = 0;

        DataManager.Car.carMaxHP += duraPlus;
        DataManager.Car.SetMaxSpeed(speedPlus);

        GameManager.Data.Money -= DataManager.Upgrade.applyCost;
    }

    private void OnDisable()
    {
        okButton.gameObject.SetActive(false);

        speedPlus = 0;
        duraPlus = 0;

        speedPlusText.text = "0";
        duraPlusText.text = "0";

        if (carObject != null)
            GameManager.Resource.Destroy(carObject);
    }
}

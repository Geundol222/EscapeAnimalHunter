using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] TMP_Text playerMoneyText;
    [SerializeField] TMP_Text materialNameText;
    [SerializeField] List<string> exteriorName;

    [SerializeField] Transform itemTransform;

    List<Image> durabilityImages;
    List<Image> speedImages;
    GameObject carObject;
    GameObject upgradeMatObj;

    int speedPlus;
    int duraPlus;

    int confirmDuraIndex;
    int confirmSpeedIndex;
    int materialIndex;

    bool completeUpgrade;

    private void Awake()
    {
        durabilityImages = new List<Image>();
        speedImages = new List<Image>();

        confirmDuraIndex = 0;
        confirmSpeedIndex = 0;
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
        completeUpgrade = false;

        materialNameText.text = exteriorName[materialIndex].ToString();
        okButton.gameObject.SetActive(false);
        playerMoneyText.text = GameManager.Data.Money.ToString();
        carObject = GameManager.Resource.Instantiate<GameObject>("Prefabs/ItemCar", itemTransform.position, Quaternion.Euler(0, -90, 0), true);
        DataManager.Car.GetUpgradeCar(carObject);

        upgradeMatObj = carObject.transform.GetChild(2).gameObject;

        if (materialIndex < 5)
            DataManager.Car.ChangeExteriorPattern(exteriorName[materialIndex]);
        else
            DataManager.Car.ChangeExteriorColor(exteriorName[materialIndex]);
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
        if (DataManager.Upgrade.durabilityIndex == confirmDuraIndex && durabilityImages[confirmSpeedIndex].color == Color.white)
            return;

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
        if (DataManager.Upgrade.carSpeedIndex >= confirmSpeedIndex)
            return;

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
        if (DataManager.Upgrade.carSpeedIndex == confirmSpeedIndex && speedImages[confirmSpeedIndex].color == Color.white)
            return;

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
        if (GameManager.Data.Money < DataManager.Upgrade.applyCost)
        {
            GameManager.Sound.PlaySound("NegativeBeep");
            return;
        }
        else
        {
            GameManager.Sound.PlaySound("ConfirmSound");
            GameManager.Data.RemoveMoney(DataManager.Upgrade.applyCost);

            DataManager.Upgrade.applyCost = 0;
            costText.text = DataManager.Upgrade.applyCost.ToString();

            DataManager.Car.carMaxHP += duraPlus;
            DataManager.Car.SetMaxSpeed(DataManager.Car.carCurMaxSpeed + speedPlus);

            confirmDuraIndex = DataManager.Upgrade.durabilityIndex;
            confirmSpeedIndex = DataManager.Upgrade.carSpeedIndex;

            playerMoneyText.text = GameManager.Data.Money.ToString();

            completeUpgrade = true;

            okButton.gameObject.SetActive(false);
        }
    }

    public void MatRight()
    {
        materialIndex++;

        if (materialIndex >= exteriorName.Count - 1)
        {
            materialIndex = exteriorName.Count - 1;
        }

        if (materialIndex < 5)
            DataManager.Car.ChangeExteriorPattern(exteriorName[materialIndex]);
        else
            DataManager.Car.ChangeExteriorColor(exteriorName[materialIndex]);

        if (upgradeMatObj.transform.GetChild(0).GetComponent<Renderer>().material.name == exteriorName[materialIndex])
            okButton.gameObject.SetActive(false);
        else
            okButton.gameObject.SetActive(true);

        materialNameText.text = exteriorName[materialIndex];
    }

    public void MatLeft()
    {
        materialIndex--;

        if (materialIndex <= 0)
        {
            materialIndex = 0;
        }

        if (materialIndex < 5)
            DataManager.Car.ChangeExteriorPattern(exteriorName[materialIndex]);
        else
            DataManager.Car.ChangeExteriorColor(exteriorName[materialIndex]);

        if (upgradeMatObj.transform.GetChild(0).GetComponent<Renderer>().material.name == exteriorName[materialIndex])
            okButton.gameObject.SetActive(false);
        else
            okButton.gameObject.SetActive(true);

        materialNameText.text = exteriorName[materialIndex];
    }

    private void OnDisable()
    {
        okButton.gameObject.SetActive(false);

        DataManager.Upgrade.applyCost = 0;
        costText.text = DataManager.Upgrade.applyCost.ToString();

        speedPlus = 0;
        duraPlus = 0;

        speedPlusText.text = "0";
        duraPlusText.text = "0";

        if (completeUpgrade)
        {
            if (confirmDuraIndex != 0)
            {
                for (int i = 0; i < confirmDuraIndex; i++)
                {
                    durabilityImages[i].color = Color.white;
                }
            }
            else
            {
                for (int i = 1; i < confirmDuraIndex; i++)
                {
                    durabilityImages[i].color = Color.black;
                }
            }

            if (confirmSpeedIndex != 0)
            {
                for (int i = 0; i < confirmSpeedIndex; i++)
                {
                    speedImages[i].color = Color.white;
                }
            }
            else
            {
                for (int i = 1; i < confirmSpeedIndex; i++)
                {
                    speedImages[i].color = Color.black;
                }
            }
        }
        else
        {
            for (int i = 1; i < DataManager.Upgrade.durabilityIndex; i++)
            {
                durabilityImages[i].color = Color.black;
            }

            for (int i = 0; i < DataManager.Upgrade.carSpeedIndex; i++)
            {
                speedImages[i].color = Color.black;
            }

            materialIndex = 0;

            for (int i = 0; i < upgradeMatObj.transform.childCount; i++)
            {
                upgradeMatObj.transform.GetChild(i).GetComponent<Renderer>().material.name = exteriorName[materialIndex];
            }
        }
        

        if (carObject != null)
            GameManager.Resource.Destroy(carObject);
    }
}

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
    [SerializeField] List<Material> carMat;

    [SerializeField] Transform itemTransform;

    List<Image> durabilityImages;
    List<Image> speedImages;
    GameObject carObject;

    int speedPlus;
    int duraPlus;

    int confirmDuraIndex;
    int confirmSpeedIndex;

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

        okButton.gameObject.SetActive(false);
        playerMoneyText.text = GameManager.Data.Money.ToString();
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

    //public void MatRight()
    //{
    //    if (materialIndex >= carMat.Count - 1)
    //    {
    //        materialIndex = carMat.Count - 1;

    //        if (materialIndex < 4)
    //            DataManager.Car.ChangeExteriorToPattern(carMat[materialIndex].name);
    //        else
    //            DataManager.Car.ChangeExteriorColor(carMat[materialIndex].name);
    //    }
    //}

    //public void MatLeft()
    //{
    //    if (materialIndex <= 0)
    //    {
    //        materialIndex = 0;

    //        if (materialIndex < 4)
    //            DataManager.Car.ChangeExteriorToPattern(carMat[materialIndex].name);
    //        else
    //            DataManager.Car.ChangeExteriorColor(carMat[materialIndex].name);
    //    }
    //}

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
        }
        

        if (carObject != null)
            GameManager.Resource.Destroy(carObject);
    }
}

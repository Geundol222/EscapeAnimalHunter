using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletUpgradeCanvas : MonoBehaviour
{
    [SerializeField] GameObject damageMeter;
    [SerializeField] GameObject speedMeter;
    [SerializeField] Button okButton;
    [SerializeField] TMP_Text costText;

    List<Image> damageImages;
    List<Image> speedImages;

    private void Awake()
    {
        damageImages = new List<Image>();
        speedImages = new List<Image>();

        costText.text = "0";

        for (int i = 0; i < damageMeter.transform.childCount; i++)
        {
            damageImages.Add(damageMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }

        for (int i = 0; i < speedMeter.transform.childCount; i++)
        {
            speedImages.Add(speedMeter.transform.GetChild(i).gameObject.GetComponent<Image>());
        }
    }

    private void OnEnable()
    {
        okButton.gameObject.SetActive(false);
    }

    private void IncreaseCost()
    {
        DataManager.Upgrade.IncreaseCost(20);

        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    private void DecreaseCost()
    {
        DataManager.Upgrade.DecreaseCost(20);

        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    public void DamageUp()
    {
        if (!okButton.gameObject.activeSelf)
            okButton.gameObject.SetActive(true);

        if (DataManager.Upgrade.damageIndex >= damageImages.Count - 1)
        {
            DataManager.Upgrade.damageIndex = damageImages.Count - 1;
            return;
        }

        DataManager.Upgrade.DamageUp();

        damageImages[DataManager.Upgrade.damageIndex].color = Color.white;

        IncreaseCost();
    }

    public void DamageDown()
    {
        damageImages[DataManager.Upgrade.damageIndex + 1].color = Color.black;

        if (DataManager.Upgrade.damageIndex <= 0)
        {
            if (DataManager.Upgrade.bulletSpeedIndex <= 0)
                okButton.gameObject.SetActive(false);

            DataManager.Upgrade.damageIndex = 0;
            damageImages[0].color = Color.black;
            return;
        }

        DataManager.Upgrade.DamageDown();

        DecreaseCost();
    }

    public void SpeedUp()
    {
        if (!okButton.gameObject.activeSelf)
            okButton.gameObject.SetActive(true);

        if (DataManager.Upgrade.bulletSpeedIndex >= speedImages.Count - 1)
        {
            DataManager.Upgrade.bulletSpeedIndex = speedImages.Count - 1;
            return;
        }

        DataManager.Upgrade.BulletSpeedUp();

        speedImages[DataManager.Upgrade.bulletSpeedIndex].color = Color.white;

        IncreaseCost();
    }

    public void SpeedDown()
    {
        speedImages[DataManager.Upgrade.carSpeedIndex].color = Color.black;

        if (DataManager.Upgrade.bulletSpeedIndex <= 0)
        {
            if (DataManager.Upgrade.damageIndex <= 0)
                okButton.gameObject.SetActive(false);

            DataManager.Upgrade.bulletSpeedIndex = 0;
            speedImages[0].color = Color.black;
            return;
        }

        DataManager.Upgrade.BulletSpeedDown();

        DecreaseCost();
    }

    public void PressOkButton()
    {
        DataManager.Upgrade.applyCost = 0;

        DataManager.Bullet.damage *= DataManager.Upgrade.bulletCostMagFirst;
        DataManager.Bullet.bulletSpeed *= DataManager.Upgrade.bulletCostMagSecond;

        GameManager.Data.RemoveCost(DataManager.Upgrade.applyCost);
    }

    private void OnDisable()
    {
        okButton.gameObject.SetActive(false);
    }
}

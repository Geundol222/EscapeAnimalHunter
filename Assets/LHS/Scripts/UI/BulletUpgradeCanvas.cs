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
    [SerializeField] TMP_Text speedPlusText;
    [SerializeField] TMP_Text damagePlusText;
    [SerializeField] TMP_Text playerMoneyText;

    [SerializeField] Transform itemTransform;

    List<Image> damageImages;
    List<Image> speedImages;
    GameObject bulletObject;

    int speedPlus;
    int damagePlus;

    int confirmDamageIndex;
    int confirmSpeedIndex;

    private void Awake()
    {
        damageImages = new List<Image>();
        speedImages = new List<Image>();

        speedPlus = 0;
        damagePlus = 0;

        costText.text = "0";
        speedPlusText.text = "0";
        damagePlusText.text = "0";

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
        playerMoneyText.text = GameManager.Data.Money.ToString();
        bulletObject = GameManager.Resource.Instantiate<GameObject>("Prefabs/ItemBullet", itemTransform.position, Quaternion.Euler(-90, 0, 0), true);
    }

    private void IncreaseCost()
    {
        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    private void DecreaseCost()
    {
        costText.text = DataManager.Upgrade.applyCost.ToString();
    }

    public void DamageUp()
    {
        DataManager.Upgrade.DamageUp();

        if (damagePlus >= 12)
            damagePlus = 12;
        else
            damagePlus += 1;

        damagePlusText.text = damagePlus.ToString();

        okButton.gameObject.SetActive(DataManager.Upgrade.damageIndex > 0 ? true : false);

        damageImages[DataManager.Upgrade.damageIndex].color = Color.white;

        IncreaseCost();
    }

    public void DamageDown()
    {
        if (DataManager.Upgrade.damageIndex == confirmDamageIndex && damageImages[confirmDamageIndex].color == Color.white)
            return;

        damageImages[DataManager.Upgrade.damageIndex].color = Color.black;

        DataManager.Upgrade.DamageDown();

        if (damagePlus <= 0)
            damagePlus = 0;
        else
            damagePlus -= 1;

        damagePlusText.text = damagePlus.ToString();

        if (DataManager.Upgrade.damageIndex <= 0)
        {
            if (DataManager.Upgrade.bulletSpeedIndex <= 0)
                okButton.gameObject.SetActive(false);

            damageImages[0].color = Color.white;
        }

        DecreaseCost();
    }

    public void SpeedUp()
    {
        DataManager.Upgrade.BulletSpeedUp();

        if (speedPlus >= 90)
            speedPlus = 90;
        else
            speedPlus += 10;

        speedPlusText.text = speedPlus.ToString();

        okButton.gameObject.SetActive(DataManager.Upgrade.bulletSpeedIndex > 0 ? true : false);

        speedImages[DataManager.Upgrade.bulletSpeedIndex].color = Color.white;

        IncreaseCost();
    }

    public void SpeedDown()
    {
        if (DataManager.Upgrade.bulletSpeedIndex == confirmSpeedIndex && speedImages[confirmSpeedIndex].color == Color.white)
            return;

        speedImages[DataManager.Upgrade.bulletSpeedIndex].color = Color.black;

        DataManager.Upgrade.BulletSpeedDown();

        if (speedPlus <= 0)
            speedPlus = 0;
        else
            speedPlus -= 10;

        speedPlusText.text = speedPlus.ToString();

        if (DataManager.Upgrade.bulletSpeedIndex <= 0)
        {
            if (DataManager.Upgrade.damageIndex <= 0)
                okButton.gameObject.SetActive(false);

            speedImages[0].color = Color.white;
        }

        DecreaseCost();
    }

    public void PressOkButton()
    {
        if (GameManager.Data.Money < DataManager.Upgrade.applyCost)
            return;
        else
        {
            GameManager.Data.RemoveMoney(DataManager.Upgrade.applyCost);

            DataManager.Upgrade.applyCost = 0;
            costText.text = DataManager.Upgrade.applyCost.ToString();

            DataManager.Bullet.AddDamage(damagePlus);
            DataManager.Bullet.AddSpeed(speedPlus);

            confirmDamageIndex = DataManager.Upgrade.damageIndex;
            confirmSpeedIndex = DataManager.Upgrade.bulletSpeedIndex;

            playerMoneyText.text = GameManager.Data.Money.ToString();

            okButton.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        okButton.gameObject.SetActive(false);

        DataManager.Upgrade.applyCost = 0;
        costText.text = DataManager.Upgrade.applyCost.ToString();

        speedPlus = 0;
        damagePlus = 0;

        speedPlusText.text = "0";
        damagePlusText.text = "0";

        if (confirmDamageIndex != 0)
        {
            for (int i = 0; i < confirmDamageIndex; i++)
            {
                damageImages[i].color = Color.white;
            }
        }
        else
        {
            for (int i = 0; i < confirmDamageIndex; i++)
            {
                damageImages[i].color = Color.black;
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
            for (int i = 0; i < confirmSpeedIndex; i++)
            {
                speedImages[i].color = Color.black;
            }
        }

        if (bulletObject != null)
            GameManager.Resource.Destroy(bulletObject);
    }
}

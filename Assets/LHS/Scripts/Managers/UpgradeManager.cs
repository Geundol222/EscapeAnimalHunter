using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int durabilityIndex;
    public int carSpeedIndex;

    public int damageIndex;
    public int bulletSpeedIndex;

    public int applyCost;
    public int carCostMagFirst;
    public int carCostMagSecond;
    public int bulletCostMagFirst;
    public int bulletCostMagSecond;

    private void Awake()
    {
        applyCost = 0;
        carCostMagFirst = 1;
        carCostMagSecond = 1;
        bulletCostMagFirst = 1;
        bulletCostMagSecond = 1;

        durabilityIndex = 0;
        carSpeedIndex = 0;

        damageIndex = 0;
        bulletSpeedIndex = 0;
    }

    public void IncreaseCost(int cost)
    {
        applyCost += cost;
    }

    public void DecreaseCost(int cost)
    {
        applyCost -= cost;
    }

    public void DurabilityUp()
    {
        if (durabilityIndex >= 9)
            durabilityIndex = 9;
        else
        {
            durabilityIndex++;
            carCostMagFirst++;

            IncreaseCost(20);
        }
    }

    public void DurabilityDown()
    {
        if (durabilityIndex <= 0)
            durabilityIndex = 0;
        else
        {
            durabilityIndex--;
            carCostMagFirst--;

            DecreaseCost(20);
        }        
    }

    public void CarSpeedUp()
    {
        if (carSpeedIndex >= 9)
            carSpeedIndex = 9;
        else
        {
            carSpeedIndex++;
            carCostMagSecond++;

            IncreaseCost(20);
        }
    }

    public void CarSpeedDown()
    {
        if (carSpeedIndex <= 0)
            carSpeedIndex = 0;
        else
        {
            carSpeedIndex--;
            carCostMagSecond--;

            DecreaseCost(20);
        }
    }

    public void DamageUp()
    {
        if (damageIndex >= 9)
            damageIndex = 9;
        else
        {
            damageIndex++;
            bulletCostMagFirst++;

            IncreaseCost(20);
        }
    }

    public void DamageDown()
    {
        if (damageIndex <= 0)
            damageIndex = 0;
        else
        {
            damageIndex--;
            bulletCostMagFirst--;

            DecreaseCost(20);
        }
    }

    public void BulletSpeedUp()
    {
        if (bulletSpeedIndex >= 9)
            bulletSpeedIndex = 9;
        else
        {
            bulletSpeedIndex++;
            bulletCostMagSecond++;

            IncreaseCost(20);
        }
    }

    public void BulletSpeedDown()
    {
        if (bulletSpeedIndex <= 0)
            bulletSpeedIndex = 0;
        else
        {
            bulletSpeedIndex--;
            bulletCostMagSecond--;

            DecreaseCost(20);
        }
    }
}

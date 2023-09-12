using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDataManager : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] float damageTime;
    [SerializeField] int damageAmount;
    [SerializeField] int maxUpgradableSpeed = 55;

    public float carCurHP;
    public float carMaxHP;
    public float carCurMaxSpeed;
    public string carCurExterior;
    public enum GearState { Parking, Neutral, Drive, Reverse };
    public GearState carCurState;
    public bool isCarParkingInBaseCamp;

    public bool canUpgrade = false;
    public bool canRepair = false;

    float time;
    GameObject gear;

    public void Awake()
    {
        car.GetComponent<CarReturner>().ReturnToBaseCamp();

        foreach (GameObject child in car.transform)
        {
            if (child.name == "Gear")
                gear = child;
        }
    }

    public void Update()
    {
        DamagedByTime();
    }

    public void OnEnable()
    {
        car.GetComponent<CarDriver>().OnMaxSpeedChanged += SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged += SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged += SetGearStateInThisScript;
    }

    public void OnDisable()
    {
        car.GetComponent<CarDriver>().OnMaxSpeedChanged -= SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged -= SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged -= SetGearStateInThisScript;
    }

    public void DamagedByTime()
    {
        if (car.GetComponent<CarDamager>().CurHp> 0 && carCurState == GearState.Drive || carCurState == GearState.Reverse)
        {
            if (car.GetComponent<CarDamager>().CurHp > 30)
            {
                time += Time.deltaTime;

                if (time > damageTime)
                {
                    SetHP(damageAmount);
                    time = 0;
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time > damageTime * 0.5)
                {
                    SetHP(damageAmount);
                    time = 0;
                }
            }
        }
    }

    public void SetHP(int value)
    {
        car.GetComponent<CarDamager>().CurHp = value;

        if (car.GetComponent<CarDamager>().CurHp >= carMaxHP)
            canRepair = false;
        else
            canRepair = true;
    }

    public void RepairCar(int cost)
    {
        SetHP(cost / 5);
    }

    public void SetDamageAmount(int value)
    {
        damageAmount = value;
    }

    public void SetMaxSpeed(float value)
    {
        car.GetComponent<CarDriver>().MaxSpeed = value;

        if (carCurMaxSpeed >= maxUpgradableSpeed)
            canUpgrade = false;
        else
            canUpgrade = true;
    }

    public void ChangeExteriorToPattern(string patternName)
    {
        Material matToChange = car.GetComponent<CarDecorator>().FindMaterial(patternName);
        car.GetComponent<CarDecorator>().ChangeMaterial(matToChange);
    }

    public void ChangeExteriorColor(string colorName)
    {
        Color colorToChange = car.GetComponent<CarDecorator>().FindColor(colorName);
        car.GetComponent<CarDecorator>().ChangeColor(colorToChange);
    }



    //안 봐도 됩니다--------------------------------------------------------

    void SetCurHPInThisScript()
    {
        carCurHP = car.GetComponent<CarDamager>().CurHp;
    }

    void SetCurMaxSpeedInThisScript()
    {
        carCurMaxSpeed = car.GetComponent<CarDriver>().MaxSpeed;
    }

    void SetGearStateInThisScript(string state)
    {
        switch (state)
        {
            case "Park":
                carCurState = GearState.Parking;
                break;

            case "Neutral":
                carCurState = GearState.Neutral;
                break;

            case "Reverse":
                carCurState = GearState.Reverse;
                break;

            case "Drive":
                carCurState = GearState.Drive;
                break;
        }
    }
}

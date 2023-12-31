using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class CarDataManager : MonoBehaviour
{
    GameObject car;
    GameObject upgradeCar;
    float damageTime = 60f;
    int damageAmount = 5;
    int maxUpgradableSpeed = 55;

    public int carCurHP = 100;
    public int carMaxHP = 100;
    public float carCurMaxSpeed = 30;
    public string carCurExterior;
    public enum GearState { Parking, Neutral, Drive, Reverse };
    public GearState carCurState;
    public bool isCarParkingInBaseCamp;

    public bool canUpgrade = false;
    public bool canRepair = false;

    float time;
    GameObject gear;

    public UnityAction OnDamaged;
    public UnityAction OnGearStateIsChanged;

    private void Awake()
    {
        carCurState = GearState.Parking;
        StartCoroutine(FindRoutine());

    }

    IEnumerator FindRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.Find("Car"); });

        car = GameObject.Find("Car");
        gear = car.transform.Find("Gear").gameObject;

        CarInit();
        yield break;
    }

    public void Update()
    {
        if (car != null)
            DamagedByTime();

    }
    

    public void OnEnable()
    {
        StartCoroutine(EnableRoutine());
    }

    IEnumerator EnableRoutine()
    {
        yield return new WaitUntil(() => { return car != null; });

        car.GetComponent<CarDriver>().OnMaxSpeedChanged += SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged += SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged += SetGearStateInThisScript;
        car.GetComponent<CarParker>().OnParkedOrNot += SetIsParking;

        yield break;
    }

    public void OnDisable()
    {
        if (car == null)
            return;

        car.GetComponent<CarDriver>().OnMaxSpeedChanged -= SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged -= SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged -= SetGearStateInThisScript;
        car.GetComponent<CarParker>().OnParkedOrNot -= SetIsParking;
    }

    public void GetUpgradeCar(GameObject car)
    {
        upgradeCar = car;
    }

    /// <summary>
    /// 씬이 초기화 될 때 차의 설정을 위한 함수
    /// </summary>
    public void CarInit()
    {
        car.GetComponent<CarDamager>().CurHp = carCurHP;
        car.GetComponent<CarDriver>().MaxSpeed = carCurMaxSpeed;
        car.GetComponent<CarReturner>().ReturnToBaseCamp();
        gear.GetComponent<XRSliderLEJ>().value = 0;
        ChangeExteriorColor("Green");
        ChangeExterialMetalic(true);

    }

    /// <summary>
    /// 지속적으로 차의 내구도를 닳게 함 차의 체력이 30 이하면 내구도 닳는 쿨타임이 짧아짐
    /// </summary>
    public void DamagedByTime()
    {
        if (car.GetComponent<CarDamager>().CurHp > 0 && carCurState == GearState.Drive || carCurState == GearState.Reverse)
        {
            if (car.GetComponent<CarDamager>().CurHp > 30)
            {
                time += Time.deltaTime;

                if (time > damageTime)
                {
                    PlusHP(-damageAmount);
                    OnDamaged?.Invoke();
                    time = 0;
                }
            }
            else
            {
                time += Time.deltaTime;

                if (time > damageTime * 0.5)
                {
                    PlusHP(-damageAmount);
                    OnDamaged?.Invoke();
                    time = 0;
                }
            }
        }
    }

    /// <summary>
    /// 차의 현재 체력을 세팅하고 차량 수리 가능 상태를 조정함
    /// </summary>
    /// <param name="value"></param>
    public void SetHP(int value)
    {
        car.GetComponent<CarDamager>().CurHp = value;

        if (car.GetComponent<CarDamager>().CurHp >= carMaxHP)
            canRepair = false;
        else
            canRepair = true;
    }

    public void PlusMaxHP(int value)
    {
        car.GetComponent<CarDamager>().MaxHp += value;
    }

    public void PlusHP(int value)
    {
        SetHP(car.GetComponent<CarDamager>().CurHp + value);
    }

    /// <summary>
    /// 코스트를 입력하면 차량을 수리해주는 함수
    /// </summary>
    /// <param name="cost">코스트</param>
    public void RepairCar(int cost)
    {
        SetHP(cost / 5);
    }

    /// <summary>
    /// 차량 업그레이드 시 차량이 받을 데미지를 설정하는 함수
    /// </summary>
    /// <param name="value">차량이 받을 데미지의 값</param>
    public void SetDamageAmount(int value)
    {
        damageAmount = value;
    }

    /// <summary>
    /// 차량의 최대속도 업그레이드 시 호출해야하는 함수
    /// </summary>
    /// <param name="value"></param>

    public void SetMaxSpeed(float value)
    {
        car.GetComponent<CarDriver>().MaxSpeed = value;

        if (carCurMaxSpeed >= maxUpgradableSpeed)
            canUpgrade = false;
        else
            canUpgrade = true;
    }

    public void SetIsParking()
    {
        isCarParkingInBaseCamp = car.GetComponent<CarParker>().isParking;
    }

    /// <summary>
    /// 차량의 현재 외관 상태를 패턴으로 설정(색을 바꾸는 것과 구분하기 위함)
    /// </summary>
    /// <param name="patternName">메터리얼 이름</param>

    public void ChangeExteriorPattern(string patternName)
    {
        car.GetComponent<CarDecorator>().ChangeMaterial(patternName);
        if (upgradeCar != null)
            upgradeCar.GetComponent<CarDecorator>().ChangeMaterial(patternName);
    }

    /// <summary>
    /// 차량의 현재 외관 상태를 메터리얼 색상 변경 모드로 바꿈
    /// </summary>
    /// <param name="colorName"></param>
    public void ChangeExteriorColor(string colorName)
    {
        car.GetComponent<CarDecorator>().ChangeColor(colorName);
        if (upgradeCar != null)
            upgradeCar.GetComponent<CarDecorator>().ChangeColor(colorName);
    }

    /// <summary>
    /// isMetalic이 true면 광택, false면 매트
    /// </summary>
    /// <param name="isMetalic"></param>
    public void ChangeExterialMetalic(bool isMetalic)
    {
        car.GetComponent<CarDecorator>().ChangeMetalic(isMetalic);
        if (upgradeCar != null)
            upgradeCar.GetComponent<CarDecorator>().ChangeMetalic(isMetalic);
    }


    //안 봐도 됩니다--------------------------------------------------------

    public void SetCurHPInThisScript()
    {
        carCurHP = car.GetComponent<CarDamager>().CurHp;
    }

    public void SetMaxHPInThisScript()
    {
        carMaxHP = car.GetComponent<CarDamager>().MaxHp;
    }

    public void SetCurMaxSpeedInThisScript()
    {
        carCurMaxSpeed = car.GetComponent<CarDriver>().MaxSpeed;
    }

    public void SetGearStateInThisScript(string state)
    {
        switch (state)
        {
            case "Park":
                carCurState = GearState.Parking;
                OnGearStateIsChanged?.Invoke();
                break;

            case "Neutral":
                carCurState = GearState.Neutral;
                OnGearStateIsChanged?.Invoke();
                break;

            case "Reverse":
                carCurState = GearState.Reverse;
                OnGearStateIsChanged?.Invoke();
                break;

            case "Drive":
                carCurState = GearState.Drive;
                OnGearStateIsChanged?.Invoke();
                break;
        }
    }
}

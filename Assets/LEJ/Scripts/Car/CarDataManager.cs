using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CarDataManager : MonoBehaviour
{
    GameObject car;
    [SerializeField] float damageTime;
    [SerializeField] int damageAmount;
    [SerializeField] int maxUpgradableSpeed = 55;

    public int carCurHP;
    public int carMaxHP;
    public float carCurMaxSpeed;
    public string carCurExterior;
    public enum GearState { Parking, Neutral, Drive, Reverse };
    public GearState carCurState;
    public bool isCarParkingInBaseCamp;

    public bool canUpgrade = false;
    public bool canRepair = false;

    float time;
    GameObject gear;

    private void Awake()
    {
        StartCoroutine(FindRoutine());
    }

    IEnumerator FindRoutine()
    {
        yield return new WaitUntil(() => { return GameObject.Find("Car"); });

        car = GameObject.Find("Car");
        gear = car.transform.Find("Gear").gameObject;

        yield break;
    }

    /*
    public void Update()
    {
        if (car != null)
            DamagedByTime();

    }
    */

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

        yield break;
    }

    public void OnDisable()
    {
        car.GetComponent<CarDriver>().OnMaxSpeedChanged -= SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged -= SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged -= SetGearStateInThisScript;
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

    /// <summary>
    /// 차량의 현재 외관 상태를 패턴으로 설정(색을 바꾸는 것과 구분하기 위함)
    /// </summary>
    /// <param name="patternName">메터리얼 이름</param>

    public void ChangeExteriorToPattern(string patternName)
    {
        Material matToChange = car.GetComponent<CarDecorator>().FindMaterial(patternName);
        car.GetComponent<CarDecorator>().ChangeMaterial(matToChange);
    }

    /// <summary>
    /// 차량의 현재 외관 상태를 메터리얼 색상 변경 모드로 바꿈
    /// </summary>
    /// <param name="colorName"></param>
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

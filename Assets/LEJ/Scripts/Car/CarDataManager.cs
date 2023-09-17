using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class CarDataManager : MonoBehaviour
{
    GameObject car;
    float damageTime = 3f;
    int damageAmount = 5;
    int maxUpgradableSpeed = 55;

    public int carCurHP = 100;
    public int carMaxHP = 100;
    public float carCurMaxSpeed = 20;
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
        car.GetComponent<CarDriver>().OnMaxSpeedChanged -= SetCurMaxSpeedInThisScript;
        car.GetComponent<CarDamager>().OnCurHpChanged -= SetCurHPInThisScript;
        gear.GetComponent<SetGearState>().OnCurGearStateChanged -= SetGearStateInThisScript;
        car.GetComponent<CarParker>().OnParkedOrNot -= SetIsParking;
    }

    /// <summary>
    /// ���� �ʱ�ȭ �� �� ���� ������ ���� �Լ�
    /// </summary>
    public void CarInit()
    {
        car.GetComponent<CarDamager>().CurHp = carCurHP;
        car.GetComponent<CarDriver>().MaxSpeed = carCurMaxSpeed;
        car.GetComponent<CarReturner>().ReturnToBaseCamp();
        gear.GetComponent<XRSliderLEJ>().value = 0;
    }

    /// <summary>
    /// ���������� ���� �������� ��� �� ���� ü���� 30 ���ϸ� ������ ��� ��Ÿ���� ª����
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
    /// ���� ���� ü���� �����ϰ� ���� ���� ���� ���¸� ������
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

    public void PlusHP(int value)
    {
        SetHP(car.GetComponent<CarDamager>().CurHp + value);
    }

    /// <summary>
    /// �ڽ�Ʈ�� �Է��ϸ� ������ �������ִ� �Լ�
    /// </summary>
    /// <param name="cost">�ڽ�Ʈ</param>
    public void RepairCar(int cost)
    {
        SetHP(cost / 5);
    }

    /// <summary>
    /// ���� ���׷��̵� �� ������ ���� �������� �����ϴ� �Լ�
    /// </summary>
    /// <param name="value">������ ���� �������� ��</param>
    public void SetDamageAmount(int value)
    {
        damageAmount = value;
    }

    /// <summary>
    /// ������ �ִ�ӵ� ���׷��̵� �� ȣ���ؾ��ϴ� �Լ�
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
    /// ������ ���� �ܰ� ���¸� �������� ����(���� �ٲٴ� �Ͱ� �����ϱ� ����)
    /// </summary>
    /// <param name="patternName">���͸��� �̸�</param>

    public void ChangeExteriorToPattern(string patternName)
    {
        Material matToChange = car.GetComponent<CarDecorator>().FindMaterial(patternName);
        car.GetComponent<CarDecorator>().ChangeMaterial(matToChange);
    }

    /// <summary>
    /// ������ ���� �ܰ� ���¸� ���͸��� ���� ���� ���� �ٲ�
    /// </summary>
    /// <param name="colorName"></param>
    public void ChangeExteriorColor(string colorName)
    {
        Color colorToChange = car.GetComponent<CarDecorator>().FindColor(colorName);
        car.GetComponent<CarDecorator>().ChangeColor(colorToChange);
    }



    //�� ���� �˴ϴ�--------------------------------------------------------

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

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

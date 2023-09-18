using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarDamager : MonoBehaviour, IHittable
{
    [SerializeField] GameObject carForwardCol;
    [SerializeField] GameObject carBackCol;
    
    [SerializeField] int maxHp;
    CarReturner returner;

    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }

    public UnityAction OnCurHpChanged;
    [SerializeField] int curHp;
    public int CurHp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            OnCurHpChanged?.Invoke();
        }
    }

    [SerializeField] int takeDamageAmount = 10;
    [SerializeField] float canHitSpeed;


    public UnityAction OnDie;
    public UnityAction OnHitSomething;

    private void Awake()
    {
        returner = GetComponent<CarReturner>();
    }

    public void OnEnable()
    {
        OnCurHpChanged += CheckCurDamage;
        carForwardCol.GetComponent<CarBodyCollider>().OnHit += HitSomething;
        carBackCol.GetComponent<CarBodyCollider>().OnHit += HitSomething;
    }

    public void OnDisable()
    {
        OnCurHpChanged -= CheckCurDamage;
        carForwardCol.GetComponent<CarBodyCollider>().OnHit -= HitSomething;
        carBackCol.GetComponent<CarBodyCollider>().OnHit -= HitSomething;
    }

    /// <summary>
    /// �� ������Ʈ�� �ڽ� BodyCol�� �ݶ��̴� Ʈ���ſ� ���� �Ǵ� �ͷ����� ���̾ ������ �ߵ���
    /// </summary>
    /// <param name="i">���� ������Ʈ�� ���̾� �ε���</param>
    /// <param name="obj">���� ������Ʈ�� gameObject</param>
    public void HitSomething(GameObject obj, Vector3 direction)
    {
        if (Math.Abs(GetComponent<Rigidbody>().velocity.magnitude) < canHitSpeed)
            return;

        if (obj.layer == 10 || obj.layer == 11)
            GiveDamage(obj, direction);

        TakeHit(takeDamageAmount);

        OnHitSomething?.Invoke();
    }

    /// <summary>
    /// ���� ������Ʈ�� �ھ��� �� �� ������Ʈ�� ICrusher�� ������ �ߵ���
    /// </summary>
    /// <param name="gameObject"></param>
    public void GiveDamage(GameObject gameObject, Vector3 direction)
    {
        if (gameObject.GetComponent<ICrusher>() != null)
        {
            gameObject.GetComponent<ICrusher>().Crusher(GetComponent<Rigidbody>().mass, GetComponent<CarDriver>().CurSpeed, direction);
        }

        GetComponent<CarDriver>().SetSpeedToZero();
    }

    /// <summary>
    /// ���� �������� ����(LEJ�̿��� �ܺ� ��ũ��Ʈ���� �� �� ����)
    /// </summary>
    /// <param name="damage"></param>
    /// <exception cref="System.NotImplementedException"></exception>

    public void TakeHit(int damage)
    {
        curHp -= damage;
    }

    /// <summary>
    /// ���� ���� ü���� Ȯ���ϰ� 0 ������ �� ���̽�ķ�� ���� ��ȯ
    /// </summary>
    public void CheckCurDamage()
    {
        if (curHp <= 0)
        {
            returner.ReturnToBaseCamp();
            OnDie?.Invoke();
        }
    }
}

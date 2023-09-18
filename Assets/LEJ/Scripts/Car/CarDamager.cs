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
    /// 차 오브젝트의 자식 BodyCol의 콜라이더 트리거에 동물 또는 터레인의 레이어가 들어오면 발동함
    /// </summary>
    /// <param name="i">박은 오브젝트의 레이어 인덱스</param>
    /// <param name="obj">박은 오브젝트의 gameObject</param>
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
    /// 차가 오브젝트를 박았을 때 그 오브젝트에 ICrusher가 있으면 발동함
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
    /// 차가 데미지를 받음(LEJ이외의 외부 스크립트에선 쓸 일 없음)
    /// </summary>
    /// <param name="damage"></param>
    /// <exception cref="System.NotImplementedException"></exception>

    public void TakeHit(int damage)
    {
        curHp -= damage;
    }

    /// <summary>
    /// 차의 현재 체력을 확인하고 0 이하일 때 베이스캠프 강제 귀환
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

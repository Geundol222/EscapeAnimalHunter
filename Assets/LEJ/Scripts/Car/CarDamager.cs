using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarDamager : MonoBehaviour, IHittable
{
    [SerializeField] Transform parkingPosition;
    [SerializeField] GameObject carBodyCol;
    
    [SerializeField] int maxHp;
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

    [SerializeField] int damageAmountWhenCarHitAnimal;
    [SerializeField] float canHitSpeed;

    int carnivoreLayerMask;
    int harbivoreLayerMask;
    int groundLayerMask;

    private void Awake()
    {
        carnivoreLayerMask = 1 << LayerMask.NameToLayer("Carnivore");
        harbivoreLayerMask = 1 << LayerMask.NameToLayer("Harbivore");
        groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    public void OnEnable()
    {
        OnCurHpChanged += CheckCurDamage;
        carBodyCol.GetComponent<CarBodyCollider>().OnHit += HitSomething;
    }

    public void OnDisable()
    {
        OnCurHpChanged -= CheckCurDamage;
    }

    /// <summary>
    /// 차 오브젝트의 자식 BodyCol의 콜라이더 트리거에 동물 또는 터레인의 레이어가 들어오면 발동함
    /// </summary>
    /// <param name="i">박은 오브젝트의 레이어 인덱스</param>
    /// <param name="obj">박은 오브젝트의 gameObject</param>
    public void HitSomething(int i, GameObject obj)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < canHitSpeed)
            return;

        if (i == carnivoreLayerMask || i == harbivoreLayerMask)
            GiveDamage(obj);

        TakeHit(damageAmountWhenCarHitAnimal);
    }

    /// <summary>
    /// 차가 오브젝트를 박았을 때 그 오브젝트에 ICrusher가 있으면 발동함
    /// </summary>
    /// <param name="gameObject"></param>
    public void GiveDamage(GameObject gameObject)
    {
        if (gameObject.GetComponent<ICrusher>() != null)
        {
            gameObject.GetComponent<ICrusher>().Crusher(GetComponent<Rigidbody>().mass, GetComponent<CarDriver>().CurSpeed, transform.forward);
        }
    }

    /// <summary>
    /// 차가 데미지를 받음(LEJ이외의 외부 스크립트에선 쓸 일 없음)
    /// </summary>
    /// <param name="damage"></param>
    /// <exception cref="System.NotImplementedException"></exception>

    public void TakeHit(int damage)
    {
        curHp -= damage;

        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 차의 현재 체력을 확인하고 0 이하일 때 베이스캠프 강제 귀환
    /// </summary>
    public void CheckCurDamage()
    {
        if (curHp <= 0)
        {
            ReturnToSwamp();
        }
    }

    /// <summary>
    /// 차를 베이스캠프로 강제 귀환 시키는 함수
    /// </summary>
    public void ReturnToSwamp()
    {
        transform.position = parkingPosition.position;
    }
}

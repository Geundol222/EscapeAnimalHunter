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
    /// �� ������Ʈ�� �ڽ� BodyCol�� �ݶ��̴� Ʈ���ſ� ���� �Ǵ� �ͷ����� ���̾ ������ �ߵ���
    /// </summary>
    /// <param name="i">���� ������Ʈ�� ���̾� �ε���</param>
    /// <param name="obj">���� ������Ʈ�� gameObject</param>
    public void HitSomething(int i, GameObject obj)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < canHitSpeed)
            return;

        if (i == carnivoreLayerMask || i == harbivoreLayerMask)
            GiveDamage(obj);

        TakeHit(damageAmountWhenCarHitAnimal);
    }

    /// <summary>
    /// ���� ������Ʈ�� �ھ��� �� �� ������Ʈ�� ICrusher�� ������ �ߵ���
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
    /// ���� �������� ����(LEJ�̿��� �ܺ� ��ũ��Ʈ���� �� �� ����)
    /// </summary>
    /// <param name="damage"></param>
    /// <exception cref="System.NotImplementedException"></exception>

    public void TakeHit(int damage)
    {
        curHp -= damage;

        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ���� ���� ü���� Ȯ���ϰ� 0 ������ �� ���̽�ķ�� ���� ��ȯ
    /// </summary>
    public void CheckCurDamage()
    {
        if (curHp <= 0)
        {
            ReturnToSwamp();
        }
    }

    /// <summary>
    /// ���� ���̽�ķ���� ���� ��ȯ ��Ű�� �Լ�
    /// </summary>
    public void ReturnToSwamp()
    {
        transform.position = parkingPosition.position;
    }
}

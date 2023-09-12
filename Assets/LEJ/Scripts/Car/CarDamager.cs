using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarDamager : MonoBehaviour, IHittable
{
    [SerializeField] Transform parkingPosition;
    [SerializeField] float maxHp;
    public UnityAction OnCurHpChanged;
    [SerializeField] float curHp;
    public float CurHp
    {
        get { return curHp; }
        set
        {
            curHp = value;
            OnCurHpChanged?.Invoke();
        }
    }

    public void OnEnable()
    {
        OnCurHpChanged += CheckCurDamage;
    }

    public void OnDisable()
    {
        OnCurHpChanged -= CheckCurDamage;
    }

    public void Awake()
    {
        curHp = maxHp;
    }

    public void TakeHit(int damage)
    {
        curHp -= damage;

        throw new System.NotImplementedException();
    }

    public void CheckCurDamage()
    {
        if (curHp <= 0)
        {
            ReturnToSwamp();
        }
    }

    public void ReturnToSwamp()
    {
        transform.position = parkingPosition.position;
    }
}

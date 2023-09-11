using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarDamager : MonoBehaviour, IHittable
{
    [SerializeField] Transform parkingPosition;
    [SerializeField] float maxDamage;
    public UnityAction OnCurDamageChanged;
    [SerializeField] float curDamage;
    public float CurDamage
    {
        get { return curDamage; }
        set
        {
            curDamage = value;
            OnCurDamageChanged?.Invoke();
        }
    }

    public void OnEnable()
    {
        OnCurDamageChanged += CheckCurDamage;
    }

    public void OnDisable()
    {
        OnCurDamageChanged -= CheckCurDamage;
    }

    public void Awake()
    {
        curDamage = maxDamage;
    }

    public void TakeHit(int damage)
    {
        curDamage -= damage;

        throw new System.NotImplementedException();
    }

    public void CheckCurDamage()
    {
        if (curDamage <= 0)
        {
            ReturnToSwamp();
        }
    }

    public void ReturnToSwamp()
    {
        transform.position = parkingPosition.position;
    }
}

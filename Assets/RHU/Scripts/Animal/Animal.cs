using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable
{
    public BTBase bTBase;

    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    [NonSerialized] public Animator animator;
    [NonSerialized] public Collider[] colliders;
    [NonSerialized] public FieldOfView fieldOfView;
    [NonSerialized] public int curHp;
    [NonSerialized] public float trackingTime = 0;
    [NonSerialized] public bool isHit = false;
    [NonSerialized] public bool isDie = false;
    [NonSerialized] public bool isHostile = false;
    [NonSerialized] public bool isTracking = false;
    [NonSerialized] public float bulletDirection = 0;

    public SelectorNode hitNode = new SelectorNode();
    public SequenceNode hostileNode = new SequenceNode();
    public SelectorNode idleNode = new SelectorNode();

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        fieldOfView = GetComponent<FieldOfView>();
        curHp = data.Animals[(int)animalName].maxHp;
        SetUpBT();
    }

    public abstract void SetUpBT();

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        //{
        //    bulletDirection = Quaternion.FromToRotation(Vector3.up, collision.transform.position - transform.position).eulerAngles.y;
        //    Debug.Log(bulletDirection);
        //}
    }

    public void TakeHit(int damage)
    {
        StartCoroutine(HitRoutine(damage));
    }

    IEnumerator HitRoutine(int damage)
    {
        curHp -= damage;
        isHit = true;

        yield return new WaitForSeconds(0.5f);

        isHit = false;

        yield break;
    }
}

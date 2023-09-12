using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    [SerializeField] private Transform footCenter;
    [SerializeField] public FieldOfView fieldOfView;
    [NonSerialized] public Animator animator;
    [NonSerialized] public Collider[] colliders;
    [NonSerialized] public int curHp;
    [NonSerialized] public float trackingTime = 0;
    [NonSerialized] public bool isHit = false;
    [NonSerialized] public bool isDie = false;
    [NonSerialized] public bool isWary = false;
    [NonSerialized] public bool isTracking = false;
    [NonSerialized] public float bulletDirection = 0;
    [SerializeField] LayerMask GroundLayer;
    public BTBase bTBase;
    public SelectorNode hitNode = new SelectorNode();
    public SelectorNode getAwayNode = new SelectorNode();
    public SequenceNode hostileNode = new SequenceNode();
    public IdleAction idleNode;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        curHp = data.Animals[(int)animalName].maxHp;
        SetUpBT();
        StartCoroutine(StepOnGrounRoutine());
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

    //private void StepOnGround()
    //{
    //    RaycastHit hitInfo;

    //    if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 20, /*1 << groundLayer*/GroundLayer))
    //    {
    //        Debug.Log($"normal : {hitInfo.normal}");
    //        Debug.DrawRay(footCenter.position, Vector3.down * 20, Color.red);
    //        Debug.DrawRay(hitInfo.point, hitInfo.normal * 20, Color.blue);

    //        Debug.Log($"Dot : {Vector3.Dot(footCenter.position, hitInfo.normal)}");

    //        transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
    //    }
    //}

    IEnumerator StepOnGrounRoutine()
    {
        RaycastHit hitInfo;

        while (true)
        {
            if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 20, GroundLayer))                
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            yield return new WaitForEndOfFrame();
        }
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

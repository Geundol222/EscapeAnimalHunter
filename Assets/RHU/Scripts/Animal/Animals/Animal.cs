using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable, ICrusher
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    [SerializeField] private Transform footCenter;
    [SerializeField] public FieldOfView fieldOfView;
    [NonSerialized] public Animator animator;
    [NonSerialized] public Collider[] colliders;
    [NonSerialized] public Collider hitCollider;
    [NonSerialized] public int curHp;
    [NonSerialized] public float waryTime;
    [NonSerialized] public bool isHit;
    [NonSerialized] public bool isDie;
    [NonSerialized] public bool isWary;
    [NonSerialized] public bool isTracking;
    [NonSerialized] public float bulletDirection;
    [SerializeField] LayerMask GroundLayer;
    public BTBase bTBase;
    public SelectorNode hitNode = new SelectorNode();
    public SequenceNode getAwayNode = new SequenceNode();
    public SequenceNode hostileNode = new SequenceNode();
    public IdleAction idleNode;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        SetUpBT();
        StartCoroutine(StepOnGrounRoutine());
        //foreach(Collider col in colliders)
        //{
        //    Debug.Log(col.name);
        //}
    }

    public abstract void SetUpBT();

    private void OnEnable()     // ReSpawn 시 초기화를 위해 OnEnable에서 초기화
    {
        curHp = data.Animals[(int)animalName].maxHp;
        waryTime = 0;
        isHit = false;
        isDie = false;
        isWary = false;
        isTracking = false;
        bulletDirection = 0;
    }

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint i in collision.contacts)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                //Debug.Log("총알 충돌");
                //Debug.Log($"i.Point : {i.point}");
                //Debug.Log($"i.Point : {i.normal}");
                //Debug.Log($"i.Point : {i.otherCollider.gameObject.transform.position}");
                
            }
        }

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
            if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 1, GroundLayer))                
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

    Coroutine crushRoutine;

    public void Crusher(float mass, float speed, Vector3 targetsForward)
    {
        Debug.Log("Crushed by car");
        crushRoutine = StartCoroutine(CrushTime(mass, speed, targetsForward));
    }

    IEnumerator CrushTime(float mass, float speed, Vector3 targetsForward)
    {
        animator.applyRootMotion = false;
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, targetsForward.z * speed * 5f), ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        animator.applyRootMotion = true;
    }


}

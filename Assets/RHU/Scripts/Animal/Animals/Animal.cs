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
    [SerializeField] LayerMask GroundLayer;

    // 각 노드에서 사용할 변수들
    [NonSerialized] public Animator animator;
    [NonSerialized] public Collider hitCollider;
    [NonSerialized] public int curHp;
    [NonSerialized] public float waryTime;
    [NonSerialized] public bool isHit;
    [NonSerialized] public bool isDie;
    [NonSerialized] public bool isWary;
    [NonSerialized] public bool isTracking;
    [NonSerialized] public bool isSit;
    [NonSerialized] public float bulletDirection;

    protected BTBase bTBase;
    protected SelectorNode rootNode = new SelectorNode();

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpBT();
        StartCoroutine(StepOnGrounRoutine());
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
        isSit = false;
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

        yield return new WaitForSeconds(1.2f);

        isHit = false;

        yield break;
    }

}

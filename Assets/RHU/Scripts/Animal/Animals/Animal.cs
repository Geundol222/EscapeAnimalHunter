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
    [NonSerialized] public Animator animator;
    [NonSerialized] public Collider[] colliders;
    [NonSerialized] public FieldOfView fieldOfView;
    [NonSerialized] public int curHp;
    [NonSerialized] public float trackingTime = 0;
    [NonSerialized] public bool isHit = false;
    [NonSerialized] public bool isDie = false;
    [NonSerialized] public bool isWary = false;
    [NonSerialized] public bool isTracking = false;
    [NonSerialized] public float bulletDirection = 0;
    private int groundLayer;
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
        fieldOfView = GetComponent<FieldOfView>();
        curHp = data.Animals[(int)animalName].maxHp;
        groundLayer = LayerMask.NameToLayer("Grouond");

        //hitNode = new SelectorNode();
        //hostileNode = new SequenceNode();
        //idleNode = new IdleAction
        SetUpBT();
    }

    public abstract void SetUpBT();

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
        
        StepOnGround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        //{
        //    bulletDirection = Quaternion.FromToRotation(Vector3.up, collision.transform.position - transform.position).eulerAngles.y;
        //    Debug.Log(bulletDirection);
        //}
    }

    private void StepOnGround()
    {
        RaycastHit hitInfo;
        float rotationX = 0;

        if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 20, /*1 << groundLayer*/GroundLayer))
        {
            Debug.Log(hitInfo.normal);
            Debug.DrawRay(footCenter.position, Vector3.down * 20, Color.red);
            Debug.DrawRay(hitInfo.point, hitInfo.normal * 20, Color.blue);

            Debug.Log(Vector3.Dot(footCenter.position, hitInfo.normal));

            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

            //if (Math.Abs(hitInfo.normal.y) >= 0.5f)
            //    rotationX = 50 * Math.Sign(hitInfo.normal.x);
            //else
            //    rotationX = hitInfo.normal.x;

            //transform.rotation = Quaternion.Euler(rotationX * 100, transform.rotation.y, transform.rotation.z);
            //Debug.Log("step");
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

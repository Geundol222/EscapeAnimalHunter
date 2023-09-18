using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SocialPlatforms;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable, ICrusher
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
    [NonSerialized] public Vector2 bulletDirection;
    [SerializeField] LayerMask playerLayer;

    protected BTBase bTBase;
    protected SelectorNode rootNode = new SelectorNode();

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpBT();
        //StartCoroutine(StepOnGrounRoutine());
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
        bulletDirection = new Vector2();
    }

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.layer != LayerMask.NameToLayer("Carnivore"))
            return;

        ContactPoint contactPoint = collision.contacts[0];

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Vector3 hitDir = footCenter.InverseTransformDirection(footCenter.position - contactPoint.point).normalized;

            if (hitDir.x > 0)
                hitDir.x = 1;
            else
                hitDir.x = -1;

            if (hitDir.z > 0)
                hitDir.z = 1;
            else
                hitDir.z = -1;

            animator.SetFloat("HitX", hitDir.x);
            animator.SetFloat("HitZ", hitDir.z);
        }
    }

    IEnumerator StepOnGrounRoutine()
    {
        RaycastHit hitInfo;

        while (true)
        {
            if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 1, GroundLayer))
            {
                Debug.Log(transform.rotation);

                transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                Debug.Log(hitInfo.normal);
                Debug.Log(transform.rotation);
            }

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

        yield return new WaitForSeconds(0.05f);

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

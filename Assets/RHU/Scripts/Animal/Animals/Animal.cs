using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;
using UnityEngine.SocialPlatforms;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable, ICrusher
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    [SerializeField] public FieldOfView fieldOfView;
    [SerializeField] private Transform footCenter;
    
    // �� ��忡�� ����� ������
    [NonSerialized] public Animator animator;
    [NonSerialized] public int curHp;
    [NonSerialized] public float waryTime;
    [NonSerialized] public bool isHit;
    [NonSerialized] public bool isDie;
    [NonSerialized] public bool isWary;
    [NonSerialized] public bool isTracking;
    [NonSerialized] public bool isSit;

    private LayerMask groundLayer;
    private LayerMask waterLayer;
    private LayerMask playerLayer;
    private LayerMask carLayer;
    protected BTBase bTBase;
    protected SelectorNode rootNode = new SelectorNode();
    protected AudioSource audioSource;

    protected void Awake()
    {
        gameObject.name = animalName.ToString();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        groundLayer = LayerMask.NameToLayer("Ground");
        waterLayer = LayerMask.NameToLayer("Water");
        playerLayer = LayerMask.NameToLayer("Player");
        carLayer = LayerMask.NameToLayer("Car");
        SetUpBT();
        //StartCoroutine(StepOnGrounRoutine());
    }
    
    public abstract void SetUpBT();

    private void OnEnable()     // ReSpawn �� �ʱ�ȭ�� ���� OnEnable���� �ʱ�ȭ
    {
        curHp = data.Animals[(int)animalName].maxHp;
        waryTime = 0;
        isHit = false;
        isDie = false;
        isWary = false;
        isTracking = false;
        isSit = false;
    }

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fieldOfView.AttackFOV())
        {
            if (collision.gameObject.layer == playerLayer || collision.gameObject.layer == carLayer)
            {
                IHittable hittable = collision.gameObject.GetComponent<IHittable>();
                hittable?.TakeHit(data.Animals[(int)animalName].attackDamage);
            }
        }

        if (collision.gameObject.layer == waterLayer)
            Destroy(gameObject, 1f);
    }


    IEnumerator StepOnGrounRoutine()
    {
        RaycastHit hitInfo;

        while (true)
        {
            if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 10, groundLayer))
            {
                Debug.Log(hitInfo.normal);
                transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(transform.rotation.x, hitInfo.normal.y, transform.rotation.z));
            }

            yield return null;
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

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

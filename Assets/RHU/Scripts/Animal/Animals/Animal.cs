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

    // 각 노드에서 사용할 변수들
    [NonSerialized] public Animator animator;
    [NonSerialized] public int curHp;
    [NonSerialized] public float waryTime;
    [NonSerialized] public bool isHit;
    [NonSerialized] public bool isDie;
    [NonSerialized] public bool isWary;
    [NonSerialized] public bool isTracking;
    [NonSerialized] public bool isSit;

    protected BTBase bTBase;
    protected SelectorNode rootNode = new SelectorNode();
    protected AudioSource audioSource;
    [SerializeField] private LayerMask groundLayer;
    private int waterLayer;
    private int playerLayer;
    private int carLayer;

    protected void Awake()
    {
        gameObject.name = animalName.ToString();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        waterLayer = LayerMask.NameToLayer("Water");
        playerLayer = LayerMask.NameToLayer("Player");
        carLayer = LayerMask.NameToLayer("Car");
    }

    private void Update()
    {
        if (!isDie)
            bTBase.Update();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fieldOfView.AttackFOV() && isWary)
        {
            if (collision.gameObject.layer == playerLayer || collision.gameObject.layer == carLayer)
            {
                IHittable hittable = collision.gameObject.GetComponent<IHittable>();
                hittable?.TakeHit(data.Animals[(int)animalName].attackDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == waterLayer)
            Destroy(gameObject, 3f);
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
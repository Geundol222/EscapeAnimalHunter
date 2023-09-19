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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
        
    // 각 노드에서 사용할 변수들
    [NonSerialized] public Animator animator;
    [NonSerialized] public int curHp;
    [NonSerialized] public float waryTime;
    [NonSerialized] public bool isHit;
    [NonSerialized] public bool isDie;
    [NonSerialized] public bool isWary;
    [NonSerialized] public bool isTracking;
    [NonSerialized] public bool isSit;

    public UnityEvent onDied;
    protected BTBase bTBase;
    protected SelectorNode rootNode = new SelectorNode();
    protected AudioSource audioSource;

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
    }

    private void Update()
    {
        if (!isDie)
            bTBase.Update();

        Debug.Log(animator.IsInTransition(0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (fieldOfView.AttackFOV())
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Car"))
            {
                IHittable hittable = collision.gameObject.GetComponent<IHittable>();
                hittable?.TakeHit(data.Animals[(int)animalName].attackDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
            Destroy(gameObject, 3f);
    }

    IEnumerator StepOnGrounRoutine()
    {
        RaycastHit hitInfo;

        while (true)
        {
            if (Physics.Raycast(footCenter.position, Vector3.down, out hitInfo, 1, groundLayer))
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

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}

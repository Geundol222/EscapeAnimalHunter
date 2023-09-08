using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public abstract class Animal : MonoBehaviour, IHittable
{
    public BTBase bTBase;

    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider[] colliders;
    [HideInInspector] public int curHp;
    [HideInInspector] public float trackingTime = 0;
    [HideInInspector] public bool isHit;
    [HideInInspector] public bool isUnconscious;
    [HideInInspector] public bool isHostile;
    [HideInInspector] public float delayTime;

    public SelectorNode hitNode = new SelectorNode();
    public SequenceNode hostileNode = new SequenceNode();
    public SelectorNode idleNode = new SelectorNode();

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        isHit = false;
        trackingTime = 0;
        isUnconscious = false;
        isHostile = false;
        curHp = data.Animals[(int)animalName].maxHp;
        delayTime = 0;
        SetUpBT();
    }

    public abstract void SetUpBT();

    private void Update()
    {
        delayTime += Time.deltaTime;

        if (delayTime >= 0.5f && !isUnconscious)
        {
            bTBase.Update();
            delayTime = 0;
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

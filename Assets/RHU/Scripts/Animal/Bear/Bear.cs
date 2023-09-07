using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimalData;

public class Bear : MonoBehaviour, IHittable
{
    private BTBase bTBase;

    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalName animalName;
    private Animator animator;
    private Collider[] colliders;
    private int curHp;
    private bool isHit;
    private bool isUnconscious;
    private float delayTime;

    private SelectorNode hitNode = new SelectorNode();
    private SequenceNode trackingNode = new SequenceNode();
    private ActionNode idleActionNode;

    //private SelectorNode            attackNode = new SelectorNode();

    //public Bear()
    //{
    //    idleActionNode = new BearIdleActionNode(animator);
    //    this.bTBase = new BTBase(hitNode, trackingNode , idleActionNode);
    //}


    private void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        isHit = false;
        curHp = data.Animals[(int)animalName].maxHp;
        delayTime = 0;

        hitNode.childrenNode = new List<Node>()
        {
            new BearHitAction(animator, isHit, curHp),
            new BearUnconsciousAction(animator, isHit, isUnconscious, curHp)
        };

        trackingNode.childrenNode = new List<Node>
        {
            //new BearDigAction(animator),
            //new BearLookAction(animator),
            //new BearSmellAction(animator),
            ////new BearWalkAction(animator)
        };

        idleActionNode = new BearIdleActionNode(animator);

        this.bTBase = new BTBase(hitNode, trackingNode, idleActionNode);
    }

    private void Update()
    {
        //if (!isUnconscious)
        //    bTBase.Update();

        delayTime += Time.deltaTime;

        if (delayTime >= 0.5f)
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
        Debug.Log($"현재 Hp : {curHp}, 맞은 damage : {damage}");
        curHp -= damage;
        Debug.Log($"맞은 후 Hp{curHp}");
        isHit = true;

        yield return new WaitForSeconds(1f);

        isHit = false;

        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase : MonoBehaviour, IHittable, IInteractable
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalData.AnimalName animalName;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Collider[] colliders;
    [HideInInspector] public int curHp;
    [HideInInspector] public bool isHit;

    //public BTBase(SelectorNode rootNode, SelectorNode hitNode, SelectorNode idleNode, SelectorNode getAwayNode)                              // �ʽĵ���
    //{
    //    this.rootNode = rootNode;

    //    rootNode.rootChildren.Add(hitNode);
    //    rootNode.rootChildren.Add(idleNode);
    //    rootNode.rootChildren.Add(getAwayNode);
    //}

    //public BTBase(SelectorNode rootNode, SelectorNode hitNode, SelectorNode idleNode, SequenceNode trackingNode, SelectorNode attackNode)     // ���ĵ���
    //{
    //    this.rootNode = rootNode;

    //    rootNode.rootChildren.Add(hitNode);
    //    rootNode.rootChildren.Add(idleNode);
    //    rootNode.rootChildren.Add(trackingNode);
    //    trackingNode.rootChildren.Add(attackNode);
    //}

    private void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        isHit = false;
        curHp = data.Animals[(int)animalName].maxHp; ;
    }

    public void TakeHit(int damage)
    {
        StartCoroutine(HitRoutine(damage));
    }

    IEnumerator HitRoutine(int damage)
    {
        curHp -= damage;
        isHit = true;

        yield return new WaitForSeconds(1.5f);

        isHit = false;

        yield break;
    }

    public bool RandomAction()
    {
        int random = Random.Range(0, 2);        // +, - �� �����ϰ� ���

        switch (random)
        {
            case 0:
                return false;

            default:
                return true;
        }
    }

    public void Interact()  // TODO : ����, �ٸ� ������ ��ȣ�ۿ� ��
    {
        
    }
}

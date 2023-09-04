using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTBase : MonoBehaviour, IHittable, IInteractable
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalData.AnimalName animalName;

    protected Node rootNode;
    protected Animator animator;
    protected Collider[] colliders;
    protected int curHp;

    public BTBase(Node rootNode)
    {
        this.rootNode = rootNode;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        colliders = GetComponentsInChildren<Collider>();
        curHp = data.Animals[(int)animalName].maxHp; ;
    }

    public void TakeHit(int damage)
    {
        curHp -= damage;
    }

    public void Interact()  // TODO : 공격, 다른 동물과 상호작용 등
    {
        
    }
}

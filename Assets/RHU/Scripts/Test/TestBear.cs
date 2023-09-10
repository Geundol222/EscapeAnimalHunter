using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TestBear : MonoBehaviour, IHittable
{
    [SerializeField] public AnimalData data;
    [SerializeField] public AnimalData.AnimalName animalName;
    [SerializeField][Range(0, 20)] int debugCurHp;
    [SerializeField] bool debug;

    private Animator animator;
    private int curHp;
    [SerializeField] bool isAlive;
    private UnityEvent onHit;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (debug)
            curHp = debugCurHp;
        else
            curHp = data.Animals[(int)animalName].maxHp;

        isAlive = true;
    }

    private void Update()
    {
        if(curHp <= 0)
            Unconscious();

        Debug.Log($"CurHp for Bear : {curHp}");
    }

    public void Unconscious()
    {
        if (!isAlive)
            return;

        isAlive = false;
        animator.SetBool("IsDie", true);
    }

    public void TakeHit(int damage)
    {
        if (!isAlive)
            return;

        Debug.Log("°õÀÌ ¾ÆÆÄÇÔ");
        animator.SetTrigger("IsHit");
        onHit?.Invoke();
        curHp -= damage;
    }
}

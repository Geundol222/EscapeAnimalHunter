using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimalStates
{
    public enum AnimalState { Idle, Unconscious, Getaway, Traking, Attack, Size }

    public abstract class AnimalStateMachine : MonoBehaviour, IHittable    // 공통
    {
        [SerializeField] public AnimalData data;
        [SerializeField] public AnimalData.AnimalName animalName;

        protected Animator animator;
        protected int curHp;
        protected int intRandom;
        protected float floatRandom;

        protected AnimalStateBase[] states;
        protected AnimalState curState; 

        protected void Awake()
        {
            animator = GetComponent<Animator>();
            curHp = data.Animals[(int)animalName].maxHp;

            states = new AnimalStateBase[(int)AnimalState.Size];
            states[0] = new IdleState(this);
            states[1] = new UnconsciousState(this);
        }

        protected void Start()
        {
            curState = AnimalState.Idle;
        }

        protected void Update()
        {
            states[(int)curState].Update();
        }

        protected void ChangeState(AnimalState state)
        {
            states[(int)curState].Exit();
            curState = state;
            states[(int)curState].Enter();
            states[(int)curState].Update();
        }

        public void TakeHit(int damage)
        {
            curHp -= damage;
            // TODO : Hit Animation
        }
    }

    public class HerbivoreStateMachine : AnimalStateMachine     // 초식
    {
        private new void Awake()
        {
            base.Awake();
            states[2] = new GetAwayState(this);
        }
    }

    public class CarnivoreStateMachine : AnimalStateMachine     // 육식
    {
        private new void Awake()
        {
            base.Awake();
            states[2] = new TrakingState(this);
            states[3] = new AttackState(this);
        }
    }
}

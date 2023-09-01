using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AniamlData", menuName = "Data/Animal")]
public class AnimalData : ScriptableObject
{
    public enum AnimalName { None, Bear, Moose };

    [SerializeField] private AnimalInfo[] animals;
    public AnimalInfo[] Animals { get { return animals; } }

    [Serializable]
    public class AnimalInfo
    {
        public AnimalName name;
        public string description;
        public GameObject prefab;

        public int maxHp;
        public int walkSpeed;
        public int runSpeed;
        public int damage;
    }
}
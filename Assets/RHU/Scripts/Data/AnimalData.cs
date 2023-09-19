using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AniamlData", menuName = "Data/Animal")]
public class AnimalData : ScriptableObject
{
    public enum AnimalName { Bear, Tiger, Moose, Deer };

    [SerializeField] private AnimalInfo[] animals;
    public AnimalInfo[] Animals { get { return animals; } }

    [Serializable]
    public class AnimalInfo
    {
        public AnimalName name;
        public string description;
        public GameObject prefab;

        public int maxHp;
        public int attackDamage;
        public int cost;
    }
}
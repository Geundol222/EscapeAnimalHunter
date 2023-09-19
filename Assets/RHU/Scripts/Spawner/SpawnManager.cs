using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SRandom = System.Random;
using URandom = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private AnimalData animalData;
    [SerializeField] private int maxWeight;

    private List<GameObject> AnimalsToAdd = new List<GameObject>();
    
    private List<GameObject> curExistAnimals = new List<GameObject>();
    public List<GameObject> CurExistAnimals { get { return curExistAnimals; } }

    private SRandom random = new SRandom();
    private float totalWeight = 0;
    float cumulativeWeight = 0;

    private void Awake()
    {
        AnimalsToAdd.Clear();
        curExistAnimals.Clear();
        AnimalSettingsToAdd();

        foreach(GameObject animal in AnimalsToAdd)
        {
            Debug.Log(animal.name);
        }
    }

    private void Update()
    {

    }

    private void ToTalWeight()
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)
        {
            totalWeight += animalInfo.weight;
        }
    }

    private void AnimalSettingsToAdd()
    {
        ToTalWeight();

        int randomAniaml;

        for (int i = 0; i < 9; i++)
        {
            randomAniaml = URandom.Range(0, animalData.Animals.Length);

            AnimalsToAdd.Add(animalData.Animals[randomAniaml].prefab);
            cumulativeWeight += animalData.Animals[randomAniaml].weight;

            if (cumulativeWeight < maxWeight)
                break;
        }
    }

    public void AddExistAnimal(GameObject animal)
    {
        curExistAnimals.Add(animal);
    }

    public void RemoveExistAnimal(GameObject animal)
    {
        if (curExistAnimals.Contains(animal))
            curExistAnimals.Remove(animal);
        else
            throw new ArgumentException("curExistAnimals에 해당 동물이 없음");
    }
}

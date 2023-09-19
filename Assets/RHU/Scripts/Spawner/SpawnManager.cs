using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager spawn;
    public static SpawnManager Spawn { get { return spawn; } }

    private List<GameObject> animalsToAdd = new List<GameObject>();                     // 생성될 동물의 prefab을 가지고 있음
    public List<GameObject> AnimalsToAdd { get { return animalsToAdd; } }
    
    private List<GameObject> curExistAnimals = new List<GameObject>();                  // Hierarchy상의 GameObject를 가지고 있음
    public List<GameObject> CurExistAnimals { get { return curExistAnimals; } }

    [SerializeField] private AnimalData animalData;
    [SerializeField] private Spawner spawner;

    private List<float> weights = new List<float>();
    private Random random = new Random();
    private float totalWeight = 0;

    private void Awake()
    {
        spawn = this;
        ClearVariables();
        AnimalSettingsToAdd();
    }

    private void ClearVariables()
    {
        if (animalsToAdd.Count != 0)
            animalsToAdd.Clear();

        if (curExistAnimals.Count != 0)
        {
            foreach (GameObject animal in curExistAnimals)
            {
                Destroy(animal);
                curExistAnimals.Remove(animal);
            }
        }

        totalWeight = 0;
    }

    private void AnimalSettingsToAdd()
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)
        {
            weights.Add(animalInfo.weight);
            totalWeight += animalInfo.weight;
        }

        float randomValue;
        float cumulativeWeight;

        for (int i = 0; i < 9; i++)
        {
            cumulativeWeight = 0;

            for (int j = 0; j < animalData.Animals.Length; j++)
            {
                randomValue = (float)random.NextDouble() * totalWeight;

                cumulativeWeight += animalData.Animals[j].weight;

                if (cumulativeWeight > randomValue)
                {
                    AnimalsToAdd.Add(animalData.Animals[j].prefab);

                    break;
                }
            }
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

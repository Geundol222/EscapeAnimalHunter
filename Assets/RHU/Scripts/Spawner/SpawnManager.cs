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

    private List<GameObject> animalsToAdd = new List<GameObject>();                     // ������ ������ prefab�� ������ ����
    public List<GameObject> AnimalsToAdd { get { return animalsToAdd; } }

    private List<GameObject> curExistAnimals = new List<GameObject>();
    public List<GameObject> CurExistAnimals { get { return curExistAnimals; } }

    [SerializeField] private AnimalData animalData;
    [SerializeField] private int animalCount;
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

    private void Update()
    {
        foreach(GameObject a in curExistAnimals)
        {
            Debug.Log(a.name);
        }
    }

    private void ClearVariables()
    {
        if (animalsToAdd.Count != 0)
            animalsToAdd.Clear();

        if (curExistAnimals.Count != 0)
            curExistAnimals.Clear();

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

        for (int i = 0; i < animalCount; i++)
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

    /// <summary>
    /// ChallengeManager�� ����� �κ�, ��ȭ�ϸ� �ش� ���� ����� ����ġ�� ������ ������ �ٸ� ���� ����
    /// ������ ���� �� ������ ����
    /// </summary>
    /// <param name="animal">������ ����</param>
    public void ReSpawnAniaml(GameObject animal)
    {
        curExistAnimals.Remove(animal);
        Destroy(animal);
        spawner.SpawnAnimal(animalData.Animals[UnityEngine.Random.Range(0, animalData.Animals.Length)].prefab);
    }
}

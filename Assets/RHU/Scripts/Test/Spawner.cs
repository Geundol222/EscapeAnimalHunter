using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] Transform focusPoint;      // �÷��̾��� ���� ��ġ
    [SerializeField] AnimalData animalData;
    [SerializeField] AnimalSpawnList[] animalSpawnList;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 100)] int maxRadius;            // �÷��̾ �������� ���� ����� ������ �����ϱ����� maxRadius���� minRadius�� ������ ��ġ�� ����
    [SerializeField][Range(0, 100)] int minRadius;
    private GameObject spawnPoint;
    private Random random;

    [Serializable]
    public class AnimalSpawnList
    {
        public AnimalData.AnimalName animalName;    // ������ ����
        public int spawnCount;                      // ������ ����
    }

    private void Awake()
    {
        spawnPoint = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        foreach(AnimalSpawnList spawnList in animalSpawnList)   // ����, TODO : PoolManager�� ����ϰԲ� ����, ���������� �� ������ ������ ������ �����ؾ� �� �� ����
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                GameObject prefab = FindAnimalPrefab(spawnList.animalName);

                //Instantiate(prefab, RandomSpawnPoint());        // TODO : ��ġ ������ �߰�
                Instantiate(prefab, focusPoint);
            }
        }
    }

    private GameObject FindAnimalPrefab(AnimalData.AnimalName animalName)
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)    // TODO : �ݺ��� ���� ��� �����غ���
        {
            if (animalName == animalInfo.name)
                return animalInfo.prefab;
        }

        return null;
    }

    private Transform RandomSpawnPoint()
    {
        spawnPoint.transform.Translate(new Vector3(random.Next(minRadius, maxRadius), 20, random.Next(minRadius, maxRadius)));

        //if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward)) // ������
        //{

        //}

        return spawnPoint.transform;
    }
}

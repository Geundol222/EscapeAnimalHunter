using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] Transform focusPoint;      // 플레이어의 시작 위치
    [SerializeField] AnimalData animalData;
    [SerializeField] AnimalSpawnList[] animalSpawnList;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 100)] int maxRadius;            // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRadius에서 minRadius를 제외한 위치에 생성
    [SerializeField][Range(0, 100)] int minRadius;
    private GameObject spawnPoint;
    private Random random;

    [Serializable]
    public class AnimalSpawnList
    {
        public AnimalData.AnimalName animalName;    // 지정한 동물
        public int spawnCount;                      // 생성할 개수
    }

    private void Awake()
    {
        spawnPoint = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        foreach(AnimalSpawnList spawnList in animalSpawnList)   // 생성, TODO : PoolManager를 사용하게끔 변경, 랜덤값으로 각 동물을 생성할 개수를 지정해야 할 것 같음
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                GameObject prefab = FindAnimalPrefab(spawnList.animalName);

                //Instantiate(prefab, RandomSpawnPoint());        // TODO : 위치 랜덤값 추가
                Instantiate(prefab, focusPoint);
            }
        }
    }

    private GameObject FindAnimalPrefab(AnimalData.AnimalName animalName)
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)    // TODO : 반복문 줄일 방법 생각해보기
        {
            if (animalName == animalInfo.name)
                return animalInfo.prefab;
        }

        return null;
    }

    private Transform RandomSpawnPoint()
    {
        spawnPoint.transform.Translate(new Vector3(random.Next(minRadius, maxRadius), 20, random.Next(minRadius, maxRadius)));

        //if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward)) // 개발중
        //{

        //}

        return spawnPoint.transform;
    }
}

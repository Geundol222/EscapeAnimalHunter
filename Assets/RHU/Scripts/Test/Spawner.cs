using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Transform focusPoint;      // 플레이어의 시작 위치
    [SerializeField] AnimalData animalData;
    [SerializeField] AnimalSpawnList[] animalSpawnList;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] int maxRadius;            // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRadius에서 minRadius를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] int minRadius;

    [Header("Etc")]
    [SerializeField] LayerMask groundLayer;

    [Serializable]
    public class AnimalSpawnList
    {
        public AnimalData.AnimalName animalName;    // 지정한 동물
        public int spawnCount;                      // 생성할 개수
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (AnimalSpawnList spawnList in animalSpawnList)   // 생성, TODO : PoolManager를 사용하게끔 변경, 랜덤값으로 각 동물을 생성할 개수를 지정해야 할 것 같음
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                RandomSpawnPoint();
                GameObject prefab = FindAnimalPrefab(spawnList.animalName);
                RaycastHit hit = GroundCheck();
                spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, hit.point.y, spawnPoint.transform.position.z);
                GameManager.Resource.Instantiate(prefab, spawnPoint.transform.position, Quaternion.Euler(0, Random.Range(-180, 180), 0));
            }
        }
    }

    private GameObject FindAnimalPrefab(AnimalData.AnimalName animalName)   // Data에서 해당 동물의 Prefab을 가져오는 함수
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)    // TODO : 반복문 줄일 방법 생각해보기
        {
            if (animalName == animalInfo.name)
                return animalInfo.prefab;
        }

        Debug.Log("Not Found Animal Prefab");
        return null;
    }

    private void RandomSpawnPoint()     // 스폰될 위치를 지정된 범위 내에서 랜덤하게 뽑아줌
    {
        spawnPoint.transform.position = focusPoint.transform.position;      // 기본 위치로 초기화

        int randomX = Random.Range(minRadius, maxRadius);
        int randomZ = Random.Range(minRadius, maxRadius);

        int randomOperator = Random.Range(0, 2);        // +, - 를 랜덤하게 출력

        switch (randomOperator)
        {
            case 0:
                randomX = -randomX;
                break;

            default:
                break;
        }

        randomOperator = Random.Range(0, 2);

        switch (randomOperator)
        {
            case 0:
                randomZ = -randomZ;
                break;

            default:
                break;
        }

        spawnPoint.transform.Translate(randomX, 500, randomZ);
    }

    private RaycastHit GroundCheck()    // Ray에 부딛힌 땅의 위치를 반환
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(spawnPoint.transform.position, Vector3.down, out hitInfo, 1000, groundLayer))
            return hitInfo;

        hitInfo = new RaycastHit();
        Debug.Log("Not Found Ground Layer");
        return hitInfo;
    }
}

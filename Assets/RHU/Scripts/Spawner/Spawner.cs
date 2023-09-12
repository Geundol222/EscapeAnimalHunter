using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Transform focusPoint;                      // 플레이어의 시작 위치
    [SerializeField] AnimalSpawnList[] animalSpawnList;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] int maxRadius;             // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRadius에서 minRadius를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] int minRadius;

    private int groundLayer;

    [Serializable]
    public class AnimalSpawnList
    {
        public AnimalData.AnimalName animalName;                // 지정한 동물
        public int spawnCount;                                  // 생성할 개수
    }

    private void Awake()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (AnimalSpawnList spawnList in animalSpawnList)   // 생성
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                RandomSpawnPoint();
                RaycastHit hit = GroundCheck();
                spawnPoint.transform.position = new Vector3(spawnPoint.transform.position.x, hit.point.y, spawnPoint.transform.position.z);
                GameManager.Resource.Instantiate<GameObject>($"Prefabs/Animals/{spawnList.animalName}", spawnPoint.transform.position, Quaternion.Euler(0, Random.Range(-180, 180), 0), true);
            }
        }
    }

    private void RandomSpawnPoint()                                         // 스폰될 위치를 지정된 범위 내에서 랜덤하게 뽑아줌
    {
        spawnPoint.transform.position = focusPoint.transform.position;      // 기본 위치로 초기화

        int randomX = Random.Range(minRadius, maxRadius);
        int randomZ = Random.Range(minRadius, maxRadius);

        int randomOperator = Random.Range(0, 2);                            // +, - 를 랜덤하게 출력

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

    private RaycastHit GroundCheck()                                        // Ray에 부딛힌 땅의 위치를 반환
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(spawnPoint.transform.position, Vector3.down, out hitInfo, 1000, 1 << groundLayer))
            return hitInfo;

        hitInfo = new RaycastHit();

        return hitInfo;
    }
}

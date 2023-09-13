using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] Transform focusPoint;                      // 플레이어의 시작 위치
    [SerializeField] AnimalSpawnArray[] animalSpawnArray;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] int maxRange;           // 플레이어를 기준으로 도넛 모양의 범위에 생성하기위해 maxRange에서 minRange를 제외한 위치에 생성
    [SerializeField][Range(0, 1000)] int minRange;
    [SerializeField] LayerMask groundLayer;

    private List<GameObject> curExistAnimals = new List<GameObject>();

    [Serializable]
    public class AnimalSpawnArray
    {
        public AnimalData.AnimalName animalName;                // 지정한 동물
        public int spawnCount;                                  // 생성할 개수
    }

    private void Start()
    {
        StartSpawn();
    }

    private void Update()
    {
        //RePosition();
    }

    private void StartSpawn()
    {
        foreach (AnimalSpawnArray spawnList in animalSpawnArray)                  // 생성
        {
            for (int i = 0; i < spawnList.spawnCount; i++)
            {
                RandomSpawnPoint();
                RaycastHit hitInfo = GroundCheck();
                GameObject animal = GameManager.Resource.Instantiate<GameObject>($"Prefabs/Animals/{spawnList.animalName}",
                    hitInfo.point, Quaternion.Euler(0, Random.Range(-180, 180), 0), true);
                curExistAnimals.Add(animal);
            }
        }
    }

    private void RandomSpawnPoint()                                             // 스폰될 위치를 지정된 범위 내에서 랜덤하게 뽑아줌
    {
        transform.position = focusPoint.position;                              // 기본 위치로 초기화
        int randomX = Random.Range(minRange, maxRange);
        int randomZ = Random.Range(minRange, maxRange);
        int randomOperator = Random.Range(0, 2);                                // +, - 를 랜덤하게 출력

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
        //Vector3 a = Random.Range(minRange, maxRange) * Random.insideUnitSphere;
        //Vector3 b = Random.insideUnitSphere;
        //Debug.Log($"RandomVector3 a : {a}");
        //Debug.Log($"RandomVector3 b : {b}");
        //transform.Translate(a);
        //transform.position = new Vector3(transform.position.x, 50, transform.position.z);
        transform.Translate(randomX, 500, randomZ);
    }

    private void RePosition()
    {
        foreach (GameObject animal in curExistAnimals)
        {
            if (DistanceCheck(animal))
            {
                Debug.Log($"RePosition to {animal.name}");
                Debug.Log(Vector3.Distance(animal.transform.position, focusPoint.position));
                RandomSpawnPoint();
                RaycastHit hitInfo = GroundCheck();
                animal.transform.Translate(hitInfo.point);
            }
        }
    }

    private bool DistanceCheck(GameObject animal)
    {
        if (Vector3.Distance(animal.transform.position, focusPoint.position) > maxRange)
            return true;

        return false;
    }

    private RaycastHit GroundCheck()                                        // Ray에 부딛힌 땅의 위치를 반환
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1000, groundLayer))
            return hitInfo;

        hitInfo = new RaycastHit();

        return hitInfo;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(focusPoint.position, minRange);
        Gizmos.DrawWireSphere(focusPoint.position, maxRange);
    }
}

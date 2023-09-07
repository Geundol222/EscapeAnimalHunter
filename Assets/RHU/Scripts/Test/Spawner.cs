using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("SpawnData")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Transform focusPoint;      // �÷��̾��� ���� ��ġ
    [SerializeField] AnimalData animalData;
    [SerializeField] AnimalSpawnList[] animalSpawnList;

    [Header("SpawnRange")]
    [SerializeField][Range(0, 1000)] int maxRadius;            // �÷��̾ �������� ���� ����� ������ �����ϱ����� maxRadius���� minRadius�� ������ ��ġ�� ����
    [SerializeField][Range(0, 1000)] int minRadius;

    [Header("Etc")]
    [SerializeField] LayerMask groundLayer;

    [Serializable]
    public class AnimalSpawnList
    {
        public AnimalData.AnimalName animalName;    // ������ ����
        public int spawnCount;                      // ������ ����
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        foreach (AnimalSpawnList spawnList in animalSpawnList)   // ����, TODO : PoolManager�� ����ϰԲ� ����, ���������� �� ������ ������ ������ �����ؾ� �� �� ����
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

    private GameObject FindAnimalPrefab(AnimalData.AnimalName animalName)   // Data���� �ش� ������ Prefab�� �������� �Լ�
    {
        foreach (AnimalData.AnimalInfo animalInfo in animalData.Animals)    // TODO : �ݺ��� ���� ��� �����غ���
        {
            if (animalName == animalInfo.name)
                return animalInfo.prefab;
        }

        Debug.Log("Not Found Animal Prefab");
        return null;
    }

    private void RandomSpawnPoint()     // ������ ��ġ�� ������ ���� ������ �����ϰ� �̾���
    {
        spawnPoint.transform.position = focusPoint.transform.position;      // �⺻ ��ġ�� �ʱ�ȭ

        int randomX = Random.Range(minRadius, maxRadius);
        int randomZ = Random.Range(minRadius, maxRadius);

        int randomOperator = Random.Range(0, 2);        // +, - �� �����ϰ� ���

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

    private RaycastHit GroundCheck()    // Ray�� �ε��� ���� ��ġ�� ��ȯ
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(spawnPoint.transform.position, Vector3.down, out hitInfo, 1000, groundLayer))
            return hitInfo;

        hitInfo = new RaycastHit();
        Debug.Log("Not Found Ground Layer");
        return hitInfo;
    }
}

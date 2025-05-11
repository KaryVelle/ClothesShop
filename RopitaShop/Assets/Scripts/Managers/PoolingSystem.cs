using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    [SerializeField] private int maxActiveObjects;
    [SerializeField] private float spawnInterval;
    [SerializeField] private List<Transform> spawnPoints;

    private Queue<GameObject> objectPool;
    private List<GameObject> activeObjects = new List<GameObject>();

    private float spawnTimer;

    private void Awake()
    {
        InitializePool();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            TrySpawn();
            spawnTimer = 0f;
        }
    }

    private void InitializePool()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, parent: gameObject.transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    private void TrySpawn()
    {
        if (activeObjects.Count >= maxActiveObjects)
        {
            return;
        }

        if (objectPool.Count == 0)
        {
            return;
        }

        GameObject obj = objectPool.Dequeue();
        obj.SetActive(true);

        Transform spawnPoint = GetRandomSpawnPoint();
        obj.transform.position = spawnPoint.position;
        obj.transform.rotation = spawnPoint.rotation;

        activeObjects.Add(obj);
        obj.GetComponent<AutoReturnToPool>()?.Init(this);
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);

        if (activeObjects.Contains(obj))
        {
            activeObjects.Remove(obj);
        }

        objectPool.Enqueue(obj);
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            return transform;
        }
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
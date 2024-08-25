using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolingExample : MonoBehaviour
{
    // Reference to the prefab to use in the object factory
    [SerializeField] private GameObject objectPrefab;

    // Whether to use the object pool or not
    [SerializeField] private bool useObjectPool;

    // Amount of objects in the example
    [SerializeField] private int objectAmount = 10_000;

    private ObjectPool objectPool = null;
    private ObjectFactory objectFactory = null;

    // Array that holds currently spawned objects
    private GameObject[] spawnedObjects = null;

    // Objects are spawned flag
    private bool areObjectsSpawned = false;

    void Start()
    {
        // Initialise a new ObjectFactory
        objectFactory = new ObjectFactory(objectPrefab);

        if (useObjectPool)
        {
            // Initialise a new ObjectPool and populate with objects from the factory
            objectPool = new ObjectPool(CreateObjects(objectFactory, objectAmount));
        }
    }

    void Update()
    {
        // Press Space to create the object OR Escape to remove the object
        if (Input.GetKeyDown(KeyCode.Space) && areObjectsSpawned == false)
        {
            areObjectsSpawned = true;

            if (objectPool != null)
            {
                // Get the spawned objects from the pool
                spawnedObjects = GetPooledObjects(objectPool, objectAmount);
            }
            else
            {
                // Create the spawned objects
                spawnedObjects = CreateObjects(objectFactory, objectAmount);
            }
        }
        else if (Input.GetKey(KeyCode.Escape) && areObjectsSpawned == true)
        {
            areObjectsSpawned = false;

            if (objectPool != null)
            {
                // Return the objects back into the pool
                foreach (GameObject gameObject in spawnedObjects)
                {
                    objectPool.ReturnObject(gameObject);
                }

                spawnedObjects = new GameObject[] { };
            }
            else
            {
                // We have to destroy the objects otherwise we will eventually run out of memory
                foreach (GameObject gameObject in spawnedObjects)
                {
                    Destroy(gameObject);
                }

                spawnedObjects = new GameObject[] { };
            }
        }
    }

    // Creates an amount of objects with the object factory
    private GameObject[] CreateObjects(ObjectFactory factory, int amount)
    {
        GameObject[] gameObjects = new GameObject[amount];

        for (int i = 0; i < amount; i++)
        {
            gameObjects[i] = factory.Create(
                new Vector3(
                    Random.Range(-1_000f, 1_000f),
                    Random.Range(-1_000f, 1_000f),
                    Random.Range(-1_000f, 1_000f)
                ),
                Quaternion.identity,
                Random.Range(1f, 5f),
                Utilities.RandomColors[Random.Range(0, Utilities.RandomColors.Length)],
                false
            );
        }

        return gameObjects;
    }

    // Retrieve an amount of objects from the object pool
    private GameObject[] GetPooledObjects(ObjectPool pool, int amount)
    {
        GameObject[] gameObjects = new GameObject[amount];

        for (int i = 0; i < amount; i++)
        {
            gameObjects[i] = pool.RetrieveObject(
                new Vector3(
                    Random.Range(-1_000f, 1_000f),
                    Random.Range(-1_000f, 1_000f),
                    Random.Range(-1_000f, 1_000f)
                ),
                Quaternion.identity
            );
        }

        return gameObjects;
    }
}

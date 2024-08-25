using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ObjectPool
{
    // Datastructure that holds all of the currently pooled GameObjects
    private Queue<GameObject> pooledObjects;

    // Constructor takes an Array of GameObjects and pools them
    public ObjectPool(GameObject[] gameObjects)
    {
        pooledObjects = new Queue<GameObject>();

        foreach (GameObject gameObject in gameObjects)
        {
            // Disable GameObject so it doesn't waste resouces (besides memory)
            gameObject.SetActive(false);
            pooledObjects.Enqueue(gameObject);
        }
    }

    // Returns a single GameObject from the pool at a specific position
    public GameObject RetrieveObject(Vector3 position, Quaternion rotation)
    {
        // We must handle the case when no objects are left
        if (pooledObjects.Count < 1)
        {
            throw new System.Exception("There are no more object in the pool!");
        }

        // Dequeue the last gameObject in the pool
        GameObject gameObject = pooledObjects.Dequeue();

        // Update the world position / rotation
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;

        // Set gameobject to active just before we return it
        gameObject.SetActive(true);

        return gameObject;
    }

    // Returns a GameObject back to the pool
    public void ReturnObject(GameObject gameObject)
    {
        // Disable gameobject so it doesn't waste resouces (besides memory)
        gameObject.SetActive(false);

        // Add it back to the pool
        pooledObjects.Enqueue(gameObject);
    }

}

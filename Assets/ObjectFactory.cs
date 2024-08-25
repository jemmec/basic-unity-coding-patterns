using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory
{
    // The prefab this object factor instantiates (creates)
    private GameObject objectPrefab;

    public ObjectFactory(GameObject objectPrefab)
    {
        this.objectPrefab = objectPrefab;
    }

    // Creates and returns a new instance of the objectPrefab 
    public GameObject Create(Vector3 position, Quaternion rotation, float size, Color color, bool addRigidbody)
    {
        // Instantiate the primitive GameObject
        GameObject gameObject = Object.Instantiate(objectPrefab);

        // Get the Mesh render to set the Color
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material.color = color;

        // Set the size, position, and rotation
        gameObject.transform.localScale = Vector3.one * size;
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;

        // Optionally add rigid body for physics
        if (addRigidbody)
        {
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.mass = Mathf.Pow(size, 2) * 10; ;
        }

        return gameObject;
    }
}

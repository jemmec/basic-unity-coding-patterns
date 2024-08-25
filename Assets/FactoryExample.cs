using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FactoryExample : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;

    private ObjectFactory sphereFactory;

    private ObjectFactory cubeFactory;

    void Start()
    {
        // Initialise the two factories
        sphereFactory = new ObjectFactory(spherePrefab);
        cubeFactory = new ObjectFactory(cubePrefab);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateObject(sphereFactory);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            CreateObject(cubeFactory);
        }
    }

    private void CreateObject(ObjectFactory factory)
    {
        factory.Create(
            new Vector3(
                Random.Range(-10f, 10f),
                15,
                Random.Range(-10f, 10f)
            ),
            Quaternion.identity,
            Random.Range(1f, 5f),
            Utilities.RandomColors[Random.Range(0, Utilities.RandomColors.Length)],
            true
        );
    }
}

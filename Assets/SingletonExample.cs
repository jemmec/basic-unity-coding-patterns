using System.Threading;
using Unity.Collections;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    // Private static instance of the Singleton object
    private static SingletonExample instance;

    // Incrementing count
    [SerializeField] private float count = 0;

    void Awake()
    {
        // Set the static instance to this Initialised object
        instance = this;
    }

    void Update()
    {
        // Increment count by deltaTime
        count += Time.deltaTime;
        if (count > 999)
        {
            count = 0;
        }
    }

    // Gets the current Count from the Singleton instance
    public static float GetCount()
    {
        // Error: We cant access non-static member's inside a static function
        // return count;

        // Instead we have to access the count through the static instance of Singleton 
        return instance.count;
    }

}

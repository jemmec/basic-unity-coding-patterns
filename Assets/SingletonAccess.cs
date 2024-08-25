using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAccess : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro countText;

    // Called every frame
    void Update()
    {
        // Can access the Singleton's count property from anywhere
        float count = SingletonExample.GetCount();

        // Update the count text
        countText.SetText(string.Format("Count: {0:0}", count));
    }
}

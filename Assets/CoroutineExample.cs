using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Serialized reference to the cube GameObject
    [SerializeField] private GameObject cube;

    // Reference to the cube's MeshRenderer
    private MeshRenderer cubeMeshRenderer;

    // References to the running Coroutines
    private Coroutine rotateRoutine = null;
    private Coroutine inputRoutine = null;

    void Start()
    {
        // Get the cube's MeshRenderer component
        cubeMeshRenderer = cube.GetComponent<MeshRenderer>();

        // Set the cube's inital color to white
        cubeMeshRenderer.material.color = Color.white;

        // Start and assign the coroutines
        rotateRoutine = StartCoroutine(RotateRoutine());
        inputRoutine = StartCoroutine(InputRoutine());
    }

    void Update(){
        // Stop the coroutines when escape is pressed
        if(Input.GetKeyDown(KeyCode.Escape)){
            StopCoroutine(rotateRoutine);
            StopCoroutine(inputRoutine);
        }
    }

    // Corotuine function that slowly rotates the cube
    private IEnumerator RotateRoutine()
    {
        // Loop indefinitely 
        while (true)
        {
            // Rotate the cube along the X and Z axis
            cube.transform.Rotate(new Vector3(
                10f * Time.deltaTime,
                0,
                10f * Time.deltaTime
            ));

            // Pause the Coroutine until the next frame
            yield return new WaitForEndOfFrame();
        }
    }

    // Coroutine function that waits for the user to press the
    // space bar then changes the cubes color over time
    private IEnumerator InputRoutine()
    {
        while (true)
        {
            // Wait until the user has pressed the space bar
            yield return new WaitUntil(() => CheckForUserInput(KeyCode.Space));

            // Interpolate to a new random color over 1 second
            Color currentColor = cubeMeshRenderer.material.color;
            Color nextColor = Utilities.RandomColors[Random.Range(0, Utilities.RandomColors.Length)];
            for (float t = 0f; t <= 1f; t += Time.deltaTime)
            {
                cube.GetComponent<MeshRenderer>().material.color
                    = Color.Lerp(currentColor, nextColor, t);

                yield return new WaitForEndOfFrame();
            }
        }
    }

    // Checks if the specific key is currently pressed down
    private bool CheckForUserInput(KeyCode code)
    {
        if (Input.GetKeyDown(code))
        {
            return true;
        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private float speed = 15.0f;
    private float turnSpeed = 35.0f;
    private float horizontalInput;
    private float forwardInput;
    private string axis;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Vehicle")
        {
            axis = "1";
        }

        else
        {
            axis = "2";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // User input for movement
        horizontalInput = Input.GetAxis("Horizontal" + axis);
        forwardInput = Input.GetAxis("Vertical" + axis);

        // Move vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Rotate Vehicle for turning
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float forwardInput;
    public float speed = 20;
    public float xBoundry = 10;
    public float zBoundryBottom = 0;
    public float zBoundryTop = 15;
    public GameObject projectile;

    //Extra variables for bonus challenge
    public int score = 0;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Score: " + 0);
        Debug.Log("Lives: " + 3);
    }

    // Update is called once per frame
    void Update()
    {
        //Keep the player in bounds
        if (transform.position.x < -xBoundry)
        {
            transform.position = new Vector3(-xBoundry, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBoundry)
        {
            transform.position = new Vector3(xBoundry, transform.position.y, transform.position.z);
        }

        if (transform.position.z < zBoundryBottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundryBottom);
        }

        if (transform.position.z > zBoundryTop)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundryTop);
        }

        //Move player left and right
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //Move player forward and back
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * forwardInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch projectile from Player
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }
}

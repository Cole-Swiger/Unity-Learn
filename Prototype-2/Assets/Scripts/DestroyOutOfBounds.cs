using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 40;
    private float bottomBound = -10;
    private GameManager gameManager;
    //GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy food when it leavs screen.
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        //Game over if player fails to feed animal before it reaches bottom of screen.
        else if (transform.position.z < bottomBound)
        {
            Destroy(gameObject);
            gameManager.AddLives(-1);
        }
    }
}

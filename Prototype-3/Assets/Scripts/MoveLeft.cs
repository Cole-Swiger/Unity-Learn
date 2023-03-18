using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;
    private float leftBound = -15;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy obstacles off screen
        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
        
        //Only move when game isn't over.
        if(!playerController.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
}

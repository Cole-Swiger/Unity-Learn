using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Destroy food and animal when they collide
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if(otherObj.name != "Player" && gameObject.name.Contains("Food"))
        {
            Destroy(gameObject);
            other.GetComponent<AnimalHunger>().FeedAnimal(1);

            //Destroy(otherObj);
            //gameManager.AddScore(1);
        }
        else if (!gameObject.name.Contains("Food") && otherObj.name == "Player")
        {
            gameManager.AddLives(-1);
            Destroy(gameObject);
        }
    }
}

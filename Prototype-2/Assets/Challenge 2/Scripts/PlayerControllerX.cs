using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float cooldown = 1;
    private float previousTime = 0;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Time: " + (Time.time - previousTime));
            if (previousTime == 0 || Time.time - previousTime > cooldown)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                previousTime = Time.time;
            }
        }
    }
}

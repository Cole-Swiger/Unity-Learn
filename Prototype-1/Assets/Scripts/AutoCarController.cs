using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCarController : MonoBehaviour
{
    //private float minSpeed = 15.0f;
    //private float maxSpeed = 35.0f;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(15.0f, 35.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}

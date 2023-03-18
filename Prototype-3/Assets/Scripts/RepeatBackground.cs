using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        //Background repeats at halfway point, so reset position at that point.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //Reset background position to repeat.
        if(transform.position.x < (startPos.x - repeatWidth))
        {
            transform.position = startPos;
        }
    }
}

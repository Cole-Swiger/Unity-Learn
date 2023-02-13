using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowDriver : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 2, 0);
    private bool isActive = false;
    private string toggle;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Camera>().enabled = isActive;

        Debug.Log(gameObject.name);
        if (gameObject.name == "Camera_Driver")
        {
            toggle = "right ctrl";
        }

        else
        {
            toggle = "left alt";
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(toggle))
        {
            isActive = !isActive;
            gameObject.GetComponent<Camera>().enabled = isActive;
        }

        transform.position = player.transform.position + offset;
        transform.rotation = player.transform.rotation;
    }
}

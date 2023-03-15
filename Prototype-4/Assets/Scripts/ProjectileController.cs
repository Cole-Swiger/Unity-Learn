using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject enemyToFollow;
    public float speed = 20;
    private float boundry = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > Mathf.Abs(boundry) || transform.position.z > Mathf.Abs(boundry))
        {
            Destroy(gameObject);
        }

        if (enemyToFollow != null)
        {
            Vector3 moveDirection = (enemyToFollow.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(enemyToFollow.transform);
            transform.Rotate(new Vector3(90, 0, 0));
        }
        else
        {
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(90, eulerRotation.y, eulerRotation.z);
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 awayFromProj = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(awayFromProj * 20, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}

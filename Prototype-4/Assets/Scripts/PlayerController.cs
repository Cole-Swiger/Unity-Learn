using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody playerRB;
    private GameObject focalPoint;
    public bool hasBouncePowerup = false;
    public bool hasProjectilePowerup = false;
    public bool hasSmashPowerup = false;
    private bool isSmashActive = false;
    public float smashForce = 15;
    public float powerupStrength = 15;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        if (hasSmashPowerup && Input.GetKeyDown(KeyCode.Space) && !isSmashActive)
        {
            StartCoroutine(SmashRotuine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if (other.gameObject.name.Contains("Bounce"))
            {
                hasBouncePowerup = true;
            }
            else if (other.gameObject.name.Contains("Projectile"))
            {
                hasProjectilePowerup = true;
                StartCoroutine(ProjectileRotuine());
            }
            else
            {
                hasSmashPowerup = true;
            }
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasBouncePowerup = false;
        hasProjectilePowerup = false;
        hasSmashPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private IEnumerator ProjectileRotuine()
    {
        while (hasProjectilePowerup)
        {
            GameObject.Find("Spawn Manager").GetComponent<SpawnManager>().SpawnProjectiles();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator SmashRotuine()
    {
        isSmashActive = true;
        playerRB.AddForce(Vector3.up * smashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        playerRB.AddForce(Vector3.down * smashForce * 4, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasBouncePowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " and player has powerup = " + hasBouncePowerup);
        }
        
        if (collision.gameObject.CompareTag("Ground") && isSmashActive)
        {
            isSmashActive = false;
            GameObject[] enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemiesInScene)
            {
                float distanceFromPlayer = Vector3.Distance(transform.position, enemy.transform.position);
                Vector3 away = enemy.transform.position - transform.position;
                if (distanceFromPlayer <= 10)
                {
                    float power = 10 / distanceFromPlayer*2;
                    Debug.Log("Enemy: " + enemy.name);
                    Debug.Log("Power: " + power);
                    enemy.GetComponent<Rigidbody>().AddForce(away * power, ForceMode.Impulse);
                }
            }
        }
    }
}

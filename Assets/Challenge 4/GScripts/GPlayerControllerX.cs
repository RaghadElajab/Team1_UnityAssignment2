using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public bool hasShield = false; // New Shield Powerup
    public GameObject powerupIndicator;
    public GameObject shieldIndicator; // Shield Visual Indicator
    public int powerUpDuration = 5;
    public GameObject shield;
    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    private float posZ;
 

    public float goallimit = 10f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        posZ = -7 ;
        
    }
   
    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float horizontalInput = Input.GetAxis("Horizontal"); //horizontal input instead of vertical
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        powerupIndicator.transform.Rotate(Vector3.up * 150 * Time.deltaTime);

        shieldIndicator.transform.position = transform.position + new Vector3(0, 0.2f, 0); ; // Shield follows player
        shieldIndicator.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, posZ);

        if (transform.position.x > goallimit)
        {
            transform.position = new Vector3(goallimit, transform.position.y, transform.position.z);
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }
        else if (transform.position.x < -goallimit)
        {
            transform.position = new Vector3(-goallimit, transform.position.y, transform.position.z);
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            StartCoroutine(PowerupCooldown());
            hasPowerup = true;
            powerupIndicator.SetActive(true);
        }
        if (other.gameObject.CompareTag("ShieldPowerUp")) // Shield Powerup Pickup
        {
            Destroy(other.gameObject);
            StartCoroutine(ShieldCooldown());
        }
    }
    IEnumerator ShieldCooldown()
    {
        hasShield = true;
        shieldIndicator.SetActive(true); // Show shield
        shield.SetActive(true);
        yield return new WaitForSeconds(powerUpDuration); // Shield lasts 5s

        hasShield = false;
        shieldIndicator.SetActive(false); // Hide shield
        shield.SetActive(false);
    }


    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  other.gameObject.transform.position- transform.position;// part of the challenge: enemy used to shoot itself towards the player so i swapped the variables 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}

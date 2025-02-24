using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public bool hasPush = false;
    public bool hasShield = false; // New Shield Powerup
    public GameObject powerupIndicator;
    public GameObject shieldIndicator; // Shield Visual Indicator
    public int powerUpDuration = 5;
    public GameObject shield;
    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        shield.SetActive(false);
        shieldIndicator.SetActive(false); // Hide shield initially
    }

    void Update()
    {
        // Move player in focal point direction
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Update powerup & shield indicators
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);
        powerupIndicator.transform.Rotate(Vector3.up * 150 * Time.deltaTime);

        shieldIndicator.transform.position = transform.position + new Vector3(0, 0.2f, 0); ; // Shield follows player
        shieldIndicator.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    // Handle powerup pickups
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PushPowerup"))
        {
            Destroy(other.gameObject);
            hasPush = true;
        }
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

    // Powerup Countdown
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Shield Countdown
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

    // Handle enemy collisions
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}

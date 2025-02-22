using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    public float maxRadius = 10f;   // Max expansion size
    public float expansionSpeed = 20f; // Speed at which the collider grows
    public float maxForce = 50f;   // Strongest push when close
    public float minForce = 10f;   // Weakest push when far
    public GameObject pushpowerupIndicator;
    public int powerUpDuration = 5;
    private SphereCollider pushCollider;
    private PlayerControllerX playerScript;
    private bool isExpanding = false;
    private bool hasPowerup;
    private Vector3 initialPosition;
    public GameObject player;
    private Rigidbody prb;

    void Start()
    {
        prb = player.GetComponent<Rigidbody>();
        pushCollider = GetComponent<SphereCollider>();
        pushCollider.isTrigger = true;
        pushCollider.radius = 0.1f; // Start small
        pushCollider.enabled = false; // Disable initially
        playerScript = player.GetComponent<PlayerControllerX>();
    }

    void Update()
    {
       hasPowerup = playerScript.hasPush;
        transform.position = player.transform.position;
        if (hasPowerup)
        {
            pushpowerupIndicator.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) & hasPowerup)
        {
            StartCoroutine(ExpandCollider());
        }
        pushpowerupIndicator.transform.position = transform.position + new Vector3(0, -0.2f, 0);
        pushpowerupIndicator.transform.Rotate(Vector3.up * 200 * Time.deltaTime);
    }
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        playerScript.hasPush = false;
        pushpowerupIndicator.SetActive(false);
    }
    IEnumerator ExpandCollider()
    {
        prb.AddForce(Vector3.up*5, ForceMode.Impulse);
        pushCollider.enabled = true;
        isExpanding = true;
        pushCollider.radius = 0.1f;
        initialPosition = transform.position;

        while (pushCollider.radius < maxRadius)
        {
            pushCollider.radius += expansionSpeed * Time.deltaTime;
            yield return null;
        }

        pushCollider.enabled = false;
        isExpanding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Enemy") && isExpanding)
        {
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                float forceFactor = 1 - (pushCollider.radius / maxRadius); // Smaller collider = stronger push
                float force = minForce + (maxForce - minForce) * forceFactor; // Adjust force based on size

                Vector3 pushDirection = (other.transform.position - initialPosition).normalized;
                enemyRb.AddForce(pushDirection * force, ForceMode.Impulse);
            }
        }
    }
}

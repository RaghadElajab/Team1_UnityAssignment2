using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    public float maxRadius = 10f;   // Max expansion size
    public float expansionSpeed = 20f; // Speed at which the collider grows
    public float maxForce = 50f;   // Strongest push when close
    public float minForce = 10f;   // Weakest push when far

    private SphereCollider pushCollider;
    private bool isExpanding = false;
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
    }

    void Update()
    {
        transform.position = player.transform.position;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExpandCollider());
        }
    }

    IEnumerator ExpandCollider()
    {
        prb.AddForce(Vector3.up * 5, ForceMode.Impulse);
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
        if (!isExpanding || !other.CompareTag("Enemy")) return;
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

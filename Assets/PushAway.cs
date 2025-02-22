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

    void Start()
    {
        pushCollider = GetComponent<SphereCollider>();
        pushCollider.isTrigger = true;
        pushCollider.radius = 0.1f; // Start small
        pushCollider.enabled = false; // Disable initially
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExpandCollider());
        }
    }

    IEnumerator ExpandCollider()
    {
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
                float distance = Vector3.Distance(initialPosition, other.transform.position);
                float force = Mathf.Lerp(maxForce, minForce, distance / maxRadius); // Weaker force at greater distances

                Vector3 pushDirection = (other.transform.position - initialPosition).normalized;
                enemyRb.AddForce(pushDirection * force, ForceMode.Impulse);
            }
        }
    }
}

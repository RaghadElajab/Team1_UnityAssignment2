using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    private Rigidbody rb;
    private Vector3 direction;

    private float minX = -19f;
    private float maxX = 19f;
    private float minZ = -10f;
    private float maxZ = 32f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        MoveTornado();
    }

    void MoveTornado()
    {
        Vector3 newPos = transform.position + direction * movementSpeed * Time.deltaTime;

        if (newPos.x < minX)
            newPos.x = minX;
        else if (newPos.x > maxX)
            newPos.x = maxX;

        if (newPos.z < minZ)
            newPos.z = minZ;
        else if (newPos.z > maxZ)
            newPos.z = maxZ;

        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            float distance = Vector3.Distance(other.transform.position, transform.position);
            float pushForce = 50f + distance;

            Vector3 direction = (other.transform.position - transform.position).normalized;
            Vector3 push = direction * pushForce;
            playerRb.AddForce(push, ForceMode.Force);
        }
    }
}

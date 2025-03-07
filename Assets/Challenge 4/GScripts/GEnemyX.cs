using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private Vector3 playerGoal;
    private Vector3 lookDirection;
    public GSpawnManagerX spawner;
    private float shootdelay=3f;
    public GameOverController GameOver;

    private float goalMinX = -4.33f;
    private float goalMaxX = 4.809f;
    private float goalZ = -9.332f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        float randomX = Random.Range(goalMinX, goalMaxX);
        Vector3 randomGoalPosition = new Vector3(randomX, transform.position.y, goalZ);
        lookDirection = (randomGoalPosition - transform.position).normalized;
        StartCoroutine(ShootAfterDelay());
    }

    
    private IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(0);
        kick();
    }

    private void kick()
    {
        enemyRb.AddForce(lookDirection * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player Goal")
        {
            GameOver.showGameOver();

        }

        if (other.gameObject.name == "Player")
        {
            StartCoroutine(nextLevel());
        }
    }

    private IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}

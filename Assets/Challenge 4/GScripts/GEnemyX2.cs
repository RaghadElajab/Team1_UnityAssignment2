using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemyX2 : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private Vector3 playerGoal;
    private Vector3 lookDirection;
    public GSpawnManagerX spawner;
    private float shootdelay = 3f;
    public GameOverController GameOver;

    private float goalMinX = -4.33f;
    private float goalMaxX = 4.809f;
    private float goalZ = -9.332f;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        StartCoroutine(ShootAfterDelay());
    }


    private IEnumerator ShootAfterDelay()
    {

        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
                yield return new WaitForSeconds(1);

            kick();
        }
    }

    private void kick()
    {
        float randomX = Random.Range(goalMinX, goalMaxX);
        Vector3 randomGoalPosition = new Vector3(randomX, transform.position.y, goalZ);
        lookDirection = (randomGoalPosition - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player Goal")
        {
            //SoundManager.Instance.PlayBooSound();
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

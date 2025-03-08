using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemyX : MonoBehaviour
{
    protected float speed=10f;
    protected Rigidbody enemyRb;
    protected Vector3 lookDirection;
    protected float shootdelay=3f;
    public GameOverController GameOver;
    public GSpawnManagerX spawner; 
    protected float goalMinX = -4.33f;
    protected float goalMaxX = 4.809f;
    protected float goalZ = -9.332f;

    void Start()
    {

        init();
    }

    protected virtual void init()
    {
        enemyRb = GetComponent<Rigidbody>();
        if (enemyRb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            return;  // Exit if Rigidbody is not found
        }
        float randomX = Random.Range(goalMinX, goalMaxX);
        Vector3 randomGoalPosition = new Vector3(randomX, transform.position.y, goalZ);
        lookDirection = (randomGoalPosition - transform.position).normalized;
        if (lookDirection == Vector3.zero)
        {
            Debug.LogWarning("LookDirection is zero. Enemy may not be moving.");
        }
        StartCoroutine(ShootAfterDelay());
    }
    protected virtual IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(0);
        kick();
    }

    protected virtual void kick()
    {
        enemyRb.AddForce(lookDirection * speed, ForceMode.Impulse);
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player Goal")
        {
            SoundManager.Instance.PlayBooSound();
            GameOver.showGameOver();

        }
        if (other.gameObject.name == "Player")
        {
            SoundManager.Instance.PlayCheerSound();
            StartCoroutine(nextLevel());
        }
    }

    protected IEnumerator nextLevel()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}

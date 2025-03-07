using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    public SpawnManagerX spawner;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        StartCoroutine(RandomKickLoop());
    }

    private IEnumerator RandomKickLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);
            Kick();
        }
    }
    private void Kick()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        enemyRb.AddForce(randomDirection * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            //SoundManager.Instance.PlayCheerSound();
            Destroy(gameObject);
            spawner.incScore();

        }
        else if (other.gameObject.name == "Player Goal")
        {
            //SoundManager.Instance.PlayBooSound();
            spawner.decScore();

            Destroy(gameObject);
        }

    }
}

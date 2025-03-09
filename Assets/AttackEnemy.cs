using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : EnemyX
{
    private Rigidbody attackRb;

    void Start()
    {
        attackRb = GetComponent<Rigidbody>();
        StartCoroutine(RandomKickLoop());
    }

    IEnumerator RandomKickLoop()
    {
        while (true)
        {
            float waitTime=Random.Range(1f, 2f);
            yield return new WaitForSeconds(waitTime);
            Kick();
        }
    }
    void Kick()
    {
        Vector3 randomDirection= new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f)).normalized;
        attackRb.AddForce(randomDirection * speed, ForceMode.Impulse);
    }
}

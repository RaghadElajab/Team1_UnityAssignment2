using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEnemyX2 : GEnemyX
{
    void Start()
    {
        base.init(); // Initialize the base class
    }

    protected override IEnumerator ShootAfterDelay()
    {
        // Make the enemy shoot twice with a delay
        for (int i = 0; i < 2; i++)
        {
            if (i == 1)
                yield return new WaitForSeconds(1);  // Wait a second before the second kick

            kick();  // Perform the kick action
        }
    }

    protected override void kick()
    {
        float randomX = Random.Range(goalMinX, goalMaxX);
        Vector3 randomGoalPosition = new Vector3(randomX, transform.position.y, goalZ);
        lookDirection = (randomGoalPosition - transform.position).normalized;
            Vector3 forceToApply = lookDirection * speed;
         
            enemyRb.AddForce(forceToApply, ForceMode.Impulse);
     
    }


}

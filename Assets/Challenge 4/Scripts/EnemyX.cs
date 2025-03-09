using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private static GameObject playerGoal; 
    public SpawnManagerX spawner;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        if (enemyRb == null)
        {
            Debug.LogError("Rigidbody is missing from " + gameObject.name);
        }
        playerGoal = GameObject.Find("Player Goal");//i initialize the playerGoal
        if (playerGoal == null)
        {
            Debug.LogError("Player Goal not found in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Behavior();

    }
    public virtual void Behavior()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            SoundManager.Instance.PlayCheerSound();
            Destroy(gameObject);
            spawner.incScore();
           
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            SoundManager.Instance.PlayBooSound();
            Destroy(gameObject);
            spawner.decScore();

        }

    }

}

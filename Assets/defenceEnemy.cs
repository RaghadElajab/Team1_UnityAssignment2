using UnityEngine;

public class defenceEnemy : MonoBehaviour
{
    public float speed = 5f;
    public float pushStrength = 5f;
    private Rigidbody defenceRb;
    private GameObject target;
  

    private void Start()
    {
        defenceRb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        MoveTowardsGoal();
    }

    private void MoveTowardsGoal()
    {
        if (target != null)
        {
            Vector3 lookDirection = (target.transform.position - transform.position).normalized;
            defenceRb.AddForce(lookDirection * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Enemy Goal")
        {
            SoundManager.Instance.PlayCheerSound();
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            SoundManager.Instance.PlayBooSound();

            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player")) // Push player away
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 pushDirection = other.transform.position - transform.position;
                pushDirection.y = 0; // Keep push on the horizontal plane

                enemyRigidbody.AddForce(pushDirection.normalized * pushStrength, ForceMode.Impulse);
            }
        }
    }

}

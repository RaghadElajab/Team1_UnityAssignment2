using UnityEngine;

public class Goal : MonoBehaviour
{
    public SpawnManagerX spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = GetComponent<SpawnManagerX>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            spawner.decScore();
        }
    }
}

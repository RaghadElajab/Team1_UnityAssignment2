using UnityEngine;

public class defenceEnemy : EnemyX
{
    public float pushStrength = 5f;
    private Rigidbody defenceRb;
    private GameObject target;

    void Start()
    {
        defenceRb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        Behavior();
    }

    public override void Behavior()
    {
        if (target != null)
        {
            Vector3 lookDirection = (target.transform.position - transform.position).normalized;
            defenceRb.AddForce(lookDirection * speed * Time.deltaTime);
        }
    }

}

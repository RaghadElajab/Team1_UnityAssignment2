using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    public float maxRadius=10f;
    public float expansionSpeed=20f;
    public float maxForce=50f;
    public float minForce=10f;
    public GameObject pushpowerupIndicator;
    public int powerUpDuration=5;
    private SphereCollider pushCollider;
    private PlayerControllerX playerScript;
    private bool isExpanding=false;
    private bool hasPowerup;

    private Vector3 initialPosition;
    public GameObject player;
    private Rigidbody prb;

    void Start()
    {
        prb=player.GetComponent<Rigidbody>();
        pushCollider=GetComponent<SphereCollider>();
        pushCollider.radius=0.1f;
        pushCollider.enabled=false;
        playerScript=player.GetComponent<PlayerControllerX>();
    }

    void Update()
    {
       hasPowerup=playerScript.hasPush;
        transform.position=player.transform.position;
        if (hasPowerup)//check if player has correct powerup
        {
            pushpowerupIndicator.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) & hasPowerup)//if click space i start it
        {
            StartCoroutine(ExpandCollider());
        }
        //effects for the indicator
        pushpowerupIndicator.transform.position = transform.position + new Vector3(0, -0.2f, 0);
        pushpowerupIndicator.transform.Rotate(Vector3.up * 200 * Time.deltaTime);
    }
    IEnumerator PowerupCooldown()//sets everything to false after a certain amount of time
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup=false;
        playerScript.hasPush=false;
        pushpowerupIndicator.SetActive(false);
    }
    IEnumerator ExpandCollider()
    {
        prb.AddForce(Vector3.up*5, ForceMode.Impulse);//jump
        StartCoroutine(PowerupCooldown());//start cooldown
        pushCollider.enabled=true;
        isExpanding=true;
        pushCollider.radius=0.1f;//start small
        initialPosition=transform.position;//store starting pos
        while (pushCollider.radius<maxRadius)
        {
            pushCollider.radius+=expansionSpeed*Time.deltaTime;//expand it over time until it reaches max
            yield return null;
        }
        pushCollider.enabled=false;//disable it when done expanding
        isExpanding=false;
    }
    private void OnTriggerEnter(Collider other)
    {  
        if (other.CompareTag("Enemy") && isExpanding)//if it triggers with an enemy it adds a force to the rigidbody
        {
            Rigidbody eRb=other.GetComponent<Rigidbody>();
            if (eRb!=null)
            {
                float strength=1-(pushCollider.radius/maxRadius); //strength of push depends on size
                float force=minForce+(maxForce-minForce)*strength;//the force is multiplied by strength factor
                Vector3 pushDirection=(other.transform.position-initialPosition).normalized;
                eRb.AddForce(pushDirection*force, ForceMode.Impulse);
            }
        }
    }
}

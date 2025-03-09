using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private float speed = 200f;
    public GameObject player;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        transform.Rotate(0, -speed*0.5f * Time.deltaTime, 0);
        
        else if (Input.GetKey(KeyCode.RightArrow))
        transform.Rotate(0, speed*0.5f * Time.deltaTime, 0);
        

        transform.position = player.transform.position; 
    }
}

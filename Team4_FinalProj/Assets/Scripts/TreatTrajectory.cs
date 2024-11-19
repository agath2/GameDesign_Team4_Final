/* This script file adds a curved trajectory to our treat object 
 * when it is thrown by the player */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treatTrajectory : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasHit){
            trackMovement();
        }
    }

    void trackMovement(){
        Vector2 direction = rb.velocity;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // apply the angle we just calculated
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D col){
        // if the arrow hits something
        hasHit = true;
    }
}

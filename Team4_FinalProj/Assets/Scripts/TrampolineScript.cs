using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float launchForce;
    public AudioSource bounce;

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Trampoline")){
            rb.velocity = Vector2.up * launchForce;
            bounce.Play();
        }
    }


}

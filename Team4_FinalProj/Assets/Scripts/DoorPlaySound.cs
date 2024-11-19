using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlaySound : MonoBehaviour
{
    public AudioClip DoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource> ().playOnAwake = false;
		GetComponent<AudioSource> ().clip = DoorOpen;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision");
        GetComponent<AudioSource> ().Play();
    }
}

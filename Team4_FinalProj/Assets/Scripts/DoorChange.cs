
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExitSimple : MonoBehaviour
{

    public string NextLevel = "StartMenu";
    public AudioClip DoorOpen;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource> ().Play();
            SceneManager.LoadScene(NextLevel);
        }
    }

}
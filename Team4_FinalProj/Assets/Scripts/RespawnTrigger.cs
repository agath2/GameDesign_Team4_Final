using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnTrigger : MonoBehaviour{
    private Transform playerPos;
    private string sceneName;

    //void OnTriggerEnter2D(Collider2D collision) {
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

//THIS SCRIPT GOES ON AN OBJECT LOCATED BELOW THE PLAYABLE AREA

    void Start(){
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update(){

         if (transform.position.y >= playerPos.position.y){
            SceneManager.LoadScene(sceneName);
            //instantiate a particle effect
            Debug.Log("I am going back to the start");
            //gameHandler.playerGetHit(damage);
            //Vector3 pSpn2 = new Vector3(pSpawnFall.position.x, pSpawnFall.position.y, playerPos.position.z);
            //playerPos.position = pSpn2;
        }


    }



}

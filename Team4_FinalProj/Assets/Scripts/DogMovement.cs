using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float followDistance = 2f; //Stop moving towards player when at this distance
    public float startFollowDistance; //Follow Player when further than this distance
    public float moveSpeed = 8f;
    public float topSpeed = 10f;
    private float scaleX;
    public bool followPlayer = true;
    private Vector2 targetPosition;
    private bool moveToTarget = false;
    


    // Start is called before the first frame update
    void Start()
    {
        scaleX = gameObject.transform.localScale.x;
        startFollowDistance = followDistance + 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate(){
        if(moveToTarget){
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) <= 0.5f){
                    moveToTarget = false;
            }

            if (targetPosition.x > gameObject.transform.position.x){
                    gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
            } else {
                    gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
            }

        }
        else if (followPlayer){
            playerPos = player.transform.position;
            distToPlayer = Vector2.Distance(transform.position, playerPos);

            //Retreat from Player
            if (distToPlayer <= followDistance){
                    transform.position = Vector2.MoveTowards (transform.position, playerPos, -moveSpeed * Time.deltaTime);
                    //anim.SetBool("Walk", true);
            }

            // Stop following Player
            if ((distToPlayer > followDistance) && (distToPlayer < startFollowDistance)){
                    transform.position = this.transform.position;
                    //anim.SetBool("Walk", false);
            }

            // Follow Player
            else if (distToPlayer >= startFollowDistance){
                    transform.position = Vector2.MoveTowards (transform.position, playerPos, moveSpeed * Time.deltaTime);
                    //anim.SetBool("Walk", true);
            }
            
            if (player.transform.position.x > gameObject.transform.position.x){
                    gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
            } else {
                    gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
            }
        }
    }

    public void MoveToPosition(Vector2 position)
    {
        targetPosition = position;
        moveToTarget = true;
        followPlayer = false;
    }

    public void Follow(){
        moveToTarget = false;
        followPlayer = true;
    }

    public void Stay()
    {
        moveToTarget = false;
        followPlayer = false;
    }
}

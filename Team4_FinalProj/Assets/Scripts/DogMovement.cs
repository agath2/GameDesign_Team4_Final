using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float followDistance = 0.5f; 
    public float moveSpeed = 8f;
    public float topSpeed = 10f;
    private float scaleX;
    public bool followPlayer = true;
    private Vector2 targetPosition;
    private bool moveToTarget = false;
    public Animator anim;
    

    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player");

        scaleX = gameObject.transform.localScale.x;
    }

    void Update()
    {

    }

    void FixedUpdate(){

        if (moveToTarget)
        {
            MoveToTarget(targetPosition);
            if (Vector2.Distance(transform.position, targetPosition) <= 0.5f){
                anim.SetBool("Walk", false); 
                moveToTarget = false;
            }
        }
        else if (followPlayer)
        {
            HandlePlayerFollowing();
        }
    }


    private void HandlePlayerFollowing()
    {
        Vector2 playerPos = player.transform.position;
        float distToPlayer = Vector2.Distance(transform.position, playerPos);

        if (distToPlayer < followDistance)
        {
            anim.SetBool("Walk", false); 
            FlipSprite(playerPos.x > transform.position.x);
        }
        else
        {
            MoveToTarget(playerPos);
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        FlipSprite(target.x > transform.position.x);
        anim.SetBool("Walk", true);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    private void FlipSprite(bool facingRight)
    {
        transform.localScale = new Vector2(facingRight ? scaleX : -scaleX, transform.localScale.y);
    }

    public void SetTargetPosition(Vector2 position)
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
        anim.SetBool("Walk", false);
        FlipSprite(player.transform.position.x > transform.position.x);
    }

    
}
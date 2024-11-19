using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerPos;
    private float distToPlayer;
    public float followDistance = 1f; 
    public float moveSpeed = 8f;
    public float jumpForce = 6f;
    public Rigidbody2D rb;
    private float scaleX;
    public bool followPlayer = true;
    private Vector2 targetPosition;
    private bool moveToTarget = false;
    public Animator anim;
    public GameObject[] treatList;
    private MapManager mapManager;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player");
        scaleX = gameObject.transform.localScale.x;

        mapManager = FindObjectOfType<MapManager>();
    }


    void Update()
    {
        treatList = GameObject.FindGameObjectsWithTag("Treat");
        Vector2 closest = new Vector2(0, 0);
        float minDist = Mathf.Infinity;
        foreach (GameObject curr in treatList) {
            float currDist = Vector2.Distance(transform.position, curr.transform.position);
            if (currDist < 1f) {
                Destroy(curr);
                anim.SetBool("Walk", false); 
                moveToTarget = false;
                continue;
            }
            if (minDist > currDist) {
                minDist = currDist;
                closest = curr.transform.position;
            }
        }

        if (minDist != Mathf.Infinity) SetTargetPosition(closest); 
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

    public bool isStopped()
    {
        if (!moveToTarget && !followPlayer)
        {
            return true;
        }
        else
        {
            return false;
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

        if(mapManager.GetTileJumpability(transform.position)){
            if(target.y >= transform.position.y + 1){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            
        }

        if(rb.velocity.y > 0){
            anim.SetTrigger("Jump");
        }

        Vector2 newPosition = new Vector2(
            Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime).x,
            transform.position.y
        );
        transform.position = newPosition;
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

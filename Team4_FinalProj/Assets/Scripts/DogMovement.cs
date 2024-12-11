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
    public bool moveToTarget = false;
    public Animator anim;
    public GameObject[] treatList;
    private MapManager mapManager;
    public bool walking = false;

    public Vector2 lastPosition;
    public float timeSinceLastMove = 0f;
    public float stopThreshold = 0.05f; 
    public float maxIdleTime = 0.5f;

    private GameObject currentFetchable;
    public float fetchableRange = 4f;
    public bool useSelector = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();

        player = GameObject.FindWithTag("Player");
        scaleX = gameObject.transform.localScale.x;

        mapManager = FindObjectOfType<MapManager>();

        lastPosition = transform.position;
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
        TrackMovement();
        walking = anim.GetBool("Walk");
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
        return (!moveToTarget && !followPlayer);
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

        if(rb.velocity.y > 0 && walking){
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
        timeSinceLastMove = 0f;
        lastPosition = transform.position;
    }

    public void Follow(){
        moveToTarget = false;
        followPlayer = true;
        timeSinceLastMove = 0f;
        lastPosition = transform.position;
    }

    public void Stay()
    {
        moveToTarget = false;
        followPlayer = false;
        anim.SetBool("Walk", false);
        FlipSprite(player.transform.position.x > transform.position.x);
    }

    private void TrackMovement()
    {
        Debug.Log("trackMovement");
        if (!moveToTarget && !followPlayer) return;
        float distanceMoved = Mathf.Abs(transform.position.x - lastPosition.x);

        if (distanceMoved < stopThreshold)
        {
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove >= maxIdleTime)
            {
                Debug.Log("too long, sit down now");
                anim.SetBool("Walk", false);
            }
        }
        else
        {
            timeSinceLastMove = 0f; 
        }

        lastPosition = transform.position; 
    }

    public void StartFetchCoroutine(Vector2 worldPosition){
        StartCoroutine(WaitForDogToStop(worldPosition));
    }

    // Coroutine to wait for the dog to reach the target
    IEnumerator WaitForDogToStop(Vector2 targetPosition)
    {
        // Wait while the dog is moving
        while (!isStopped())
        {
            yield return null; // Wait for one frame
        }

        // Now the dog has stopped moving, check for fetchable objects
        CheckForNearbyFetchable();
    }

    void CheckForNearbyFetchable()
    {
        // Get all objects with the "Fetch" tag in the scene
        GameObject[] fetch = GameObject.FindGameObjectsWithTag("Fetch");

        // Get all objects with the "Key" tag
        GameObject[] keys = GameObject.FindGameObjectsWithTag("Key");

        // Combine both arrays into a single list
        GameObject[] fetchables = new GameObject[fetch.Length + keys.Length];
        fetch.CopyTo(fetchables, 0);
        keys.CopyTo(fetchables, fetch.Length);

        // Iterate through each fetchable item
        foreach (GameObject fetchable in fetchables)
        {
            // Calculate the distance between the dog and the fetchable object
            float distance = Vector2.Distance(transform.position, fetchable.transform.position);

            // If the fetchable is within range, pick it up
            if (distance <= fetchableRange)
            {
                Debug.Log("Fetchable item within range: " + fetchable.name);

                // Set it as the current fetchable object
                currentFetchable = fetchable;

                // Disable the object's physics interactions (if it has any), and attach it to the dog
                Rigidbody2D rb = currentFetchable.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = true; // Disable physics to make it follow the dog
                }

                // Set the object's position relative to the dog and parent it to the dog
                currentFetchable.transform.SetParent(transform); // Parent it to the dog's transform
                currentFetchable.transform.localPosition = Vector3.zero; // Position the object at the dog's transform

                currentFetchable.SetActive(true); // Ensure the object is visible after being picked up

                Debug.Log("Object picked up and now following the dog: " + currentFetchable.name);

                Follow();

                return; // Exit once we find the first fetchable object within range
            }
        }

        Debug.Log("No fetchable objects within range.");
    }


}

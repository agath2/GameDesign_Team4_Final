using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public GameHandler gameHandlerObj;
    public Animator animator;
    public Rigidbody2D rb2D;
    private bool FaceRight = true; // determine which way player is facing.
    public static float runSpeed = 10f;
    public float startSpeed = 10f;
    public bool isAlive = true;
    //public AudioSource WalkSFX;
    private Vector3 hMove;
    public float ClimbingSpeed = 1f;
    public SpriteRenderer spriteRenderer;
    private DestinationSelector destinationSelector;
    private RightClickOptions RightClick;

    void Start(){
        destinationSelector = FindObjectOfType<DestinationSelector>();
        RightClick = FindObjectOfType<RightClickOptions>();

        
        animator = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update(){
        // Prevent movement if in selection mode
        if ((destinationSelector != null && destinationSelector.isSelecting))
        {
            return;
        }
        if ((RightClick != null && RightClick.isMenuActive))
        {
            return;
        }
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

        if (isAlive == true){
            transform.position += hMove * runSpeed * Time.deltaTime;

            animator.SetFloat("xVelocity", Mathf.Abs(hMove.x));

            // if (Input.GetAxis("Horizontal") != 0){
            // //       animator.SetBool ("Walk", true);
            // //       if (!WalkSFX.isPlaying){
            // //             WalkSFX.Play();
            // //      }
            //     animator.SetBool("Walk", true);
            // } else {
            // //      animator.SetBool ("Walk", false);
            // //      WalkSFX.Stop();
            //     animator.SetBool("Walk", false);
            // }

            // Turning: Reverse if input is moving the Player right and Player faces left
            // if ((hMove.x <0 && !FaceRight) || (hMove.x >0 && FaceRight)){
            //     playerTurn();
            //     // FlipPlayer();
            // }

            if (hMove.x < 0 && FaceRight) {
                playerTurn();
            }
            else if (hMove.x > 0 && !FaceRight) {
                playerTurn();
            }
        }
    }

    void FixedUpdate(){
        //slow down on hills / stops sliding from velocity
        if (hMove.x == 0){
            rb2D.velocity = new Vector2(rb2D.velocity.x / 1.1f, rb2D.velocity.y) ;
        }
    }

    private void playerTurn(){
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
        // Vector3 theScale = transform.localScale;
        // theScale.x *= -1;
        // transform.localScale = theScale;
        spriteRenderer.flipX = !FaceRight;
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ladder"))
        {
            float verticalInput = Input.GetAxis("Vertical"); // Use "Vertical" from Input Manager

            if (verticalInput > 0)
            { // Pressing up
                Debug.Log("Climbing up the ladder");
                rb2D.velocity = new Vector2(0f, 10f);
            }
            else if (verticalInput < 0)
            { // Pressing down
                Debug.Log("Climbing down the ladder");
                rb2D.velocity = new Vector2(0f, -10f);
            }
            else
            {
                // No movement
                rb2D.velocity = new Vector2(0f, 0f);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.tag == "Ladder") {
            rb2D.velocity = new Vector2(0f, 0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Car") {
            gameHandlerObj.RestartLevel();
        }
    }
}

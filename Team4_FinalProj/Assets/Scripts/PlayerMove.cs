using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //public GameHandler gameHandlerObj;
    private Animator animator;
    private Rigidbody2D rb2D;
    private bool FaceRight = true; // determine which way player is facing.
    public static float runSpeed = 10f;
    public static float slopeSpeed = 7f;
    public bool isAlive = true;
    //public AudioSource WalkSFX;
    private Vector3 hMove;
    public float ClimbingSpeed = 1f;
    //public SpriteRenderer spriteRenderer;
    private DestinationSelector destinationSelector;
    private RightClickOptions RightClick;
    private PauseMenuController Pause;

    private AudioSource StepToPlay;
    public AudioSource[] SFX_Steps;

    public GameObject particlesDust;

    public bool topOfSlope = false;
    public bool isClimbing = false;

    void Start()
    {
        destinationSelector = FindObjectOfType<DestinationSelector>();
        RightClick = FindObjectOfType<RightClickOptions>();

        animator = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        particlesDust.SetActive(false);
    }

    void Update()
    {
         if ((Input.GetAxisRaw("Horizontal")!= 0)){
            PlaySteps();
        } else {
            StopSteps();
        }
        // Prevent movement if in selection mode
        if ((destinationSelector != null && destinationSelector.isSelecting))
        {
            return;
        }
        if ((RightClick != null && RightClick.isMenuActive))
        {
            return;
        }
        if ((Pause != null && Pause.isPaused))
        {
            return;
        }
        //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
        hMove = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);

        if (isAlive == true)
        {
            //stransform.position += hMove * runSpeed * Time.deltaTime;
            animator.SetBool("isClimbing", isClimbing);
            animator.SetFloat("xVelocity", Mathf.Abs(hMove.x));

            if (hMove.x < 0 && FaceRight)
            {
                playerTurn();
                if (rb2D.velocity.y == 0){
                    particlesDust.SetActive(true);
                }
            }
            else if (hMove.x > 0 && !FaceRight)
            {
                playerTurn();
                if (rb2D.velocity.y == 0){
                    particlesDust.SetActive(true);
                }
            }
            else if (hMove.x ==0){
                particlesDust.SetActive(false);
            }
        }
    }
    public LayerMask groundLayer; // Assign this in the Inspector
    private float slopeCheckDistance = 2f;
    private float slopeDownAngle;
    private Vector2 slopeNormalPerp;
    private bool isOnSlope;

    void SlopeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, slopeCheckDistance, groundLayer);

        if (hit)
        {
            
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            isOnSlope = slopeDownAngle != 0;
        }
        else
        {
            isOnSlope = false;
        }
    }

    void FixedUpdate()
    {
        SlopeCheck();

        if(slopeDownAngle == 0 && topOfSlope){
            topOfSlope = false;
            // if(Input.GetButtonDown("Jump")){
            //     rb2D.velocity = new Vector2(hMove.x * runSpeed, rb2D.velocity.y);
            // }
            // else{
            //     rb2D.velocity = new Vector2(hMove.x * runSpeed, -runSpeed);
            // }
            if(!Input.GetButtonDown("Jump")){
                rb2D.velocity = new Vector2(hMove.x * runSpeed, -runSpeed);
            }
            
        } 
        else if (slopeDownAngle != 0){
            topOfSlope = true;
            rb2D.velocity = new Vector2(hMove.x * slopeSpeed, rb2D.velocity.y);
        } else {
            topOfSlope = false;
            rb2D.velocity = new Vector2(hMove.x * runSpeed, rb2D.velocity.y);
        }

        // if (isOnSlope && hMove.x != 0)
        // {
        //     // Debug.Log("isOnSlope and hMove");
        //     rb2D.velocity = new Vector2(runSpeed * slopeNormalPerp.x * Mathf.Sign(hMove.x), runSpeed * slopeNormalPerp.y);
        // }
        // else
        // {
        //     // Debug.Log(rb2D.velocity.x + ", " + rb2D.velocity.y);
        //     rb2D.velocity = new Vector2(hMove.x * runSpeed, rb2D.velocity.y);
        // }
    }

    private void playerTurn()
    {
        // NOTE: Switch player facing label
        FaceRight = !FaceRight;

        // NOTE: Multiply player's x local scale by -1.
         Vector3 theScale = transform.localScale;
         theScale.x *= -1;
         transform.localScale = theScale;
        //spriteRenderer.flipX = !FaceRight;
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
                isClimbing = true;
                // if(!animator.GetBool("isClimbing")){
                //     animator.SetBool("isClimbing", true);
                // }
            }
            else if (verticalInput < 0)
            { // Pressing down
                Debug.Log("Climbing down the ladder");
                rb2D.velocity = new Vector2(0f, -10f);
                isClimbing = true;
                // if(!animator.GetBool("isClimbing")){
                //     animator.SetBool("isClimbing", true);
                // }
            }
            else
            {
                // No movement
                rb2D.velocity = new Vector2(0f, 0f);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            rb2D.velocity = new Vector2(0f, 0f);
            isClimbing = false;
        }
    }

    public void PlaySteps(){
        if ((StepToPlay != null) && (StepToPlay.isPlaying)){
            return;
        } else {
            int StepNum = Random.Range(0, SFX_Steps.Length);
            StepToPlay = SFX_Steps[StepNum];
            StepToPlay.Play();
        }
    }

    public void StopSteps(){
        if ((StepToPlay != null) && (StepToPlay.isPlaying)){
            // StepToPlay.Stop();
            StepToPlay = null;
        }
    }
    
}

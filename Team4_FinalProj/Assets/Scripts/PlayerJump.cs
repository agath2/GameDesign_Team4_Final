using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public bool canJump = false;
    public int jumpTimes = 0;
    public bool isAlive = true;
    private DestinationSelector destinationSelector;
    private RightClickOptions RightClick;


    //public AudioSource JumpSFX;

    void Start(){
        RightClick = FindObjectOfType<RightClickOptions>();

        destinationSelector = FindObjectOfType<DestinationSelector>();

        anim = gameObject.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed!");
        }

        if (destinationSelector != null && destinationSelector.isSelecting)
        {
            return;
        }
        if ((RightClick != null && RightClick.isMenuActive))
        {
            return;
        }

        if ((IsGrounded()) && (jumpTimes <= 1)){ // for single jump only
            canJump = true;
        }  else {
            canJump = false;
        }

        if ((Input.GetButtonDown("Jump")) && (canJump) && (isAlive == true)) {
            Jump();
        }

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isJumping", !IsGrounded());
    }

    public void Jump() {
        jumpTimes += 1;
        rb.velocity = Vector2.up * jumpForce;
        anim.SetTrigger("Jump");
        // JumpSFX.Play();

        //Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.2f, groundLayer);
        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 0.2f, enemyLayer);
        if ((groundCheck != null) || (enemyCheck != null)) {
            //Debug.Log("I am trouching ground!");
            jumpTimes = 0;
            return true;
        }
        return false;
    }
}

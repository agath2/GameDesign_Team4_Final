using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private float speed = 2f;
    private bool moveToA = true;
    public Transform moveTargetA;
    public Transform moveTargetB;
    private Vector3 mTA;
    private Vector3 mTB;

    // If the platform is a creature that should turn around as it moves back and forth, set true:
    private bool isHorizontal = false;

    private bool isPaused = false;

    void FixedUpdate()
    {
        if (isPaused) return; // Stop movement when paused.

        mTA = new Vector3(moveTargetA.position.x, moveTargetA.position.y, 0);
        mTB = new Vector3(moveTargetB.position.x, moveTargetB.position.y, 0);
        float step = speed * Time.deltaTime; // calculate distance to move

        if (moveToA)
        {
            transform.position = Vector3.MoveTowards(transform.position, mTA, step);
            if (Vector3.Distance(moveTargetA.position, transform.position) < 1)
            {
                StartCoroutine(PauseAtTarget());
                moveToA = false;
                turn();
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, mTB, step);
            if (Vector3.Distance(moveTargetB.position, transform.position) < 1)
            {
                StartCoroutine(PauseAtTarget());
                moveToA = true;
                turn();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dog")
        {
            other.collider.transform.SetParent(transform); // so Player moves with platform
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Dog")
        {
            other.collider.transform.SetParent(null); // Player not parented when off platform
        }
    }

    void turn()
    {
        if (isHorizontal)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    IEnumerator PauseAtTarget()
    {
        isPaused = true; // Stop the platform's movement
        yield return new WaitForSeconds(3f); // Wait for 5 seconds
        isPaused = false; // Resume movement
    }
}

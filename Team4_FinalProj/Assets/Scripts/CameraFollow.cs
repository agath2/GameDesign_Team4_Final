using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2DLERP : MonoBehaviour
{
    private GameObject target;
    public float camSpeed = 4.0f;

    public Transform minXBoundary;
    public Transform maxXBoundary;

    private Camera cam;
    private float halfCamWidth;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        cam = GetComponent<Camera>();

        // Calculate half the camera's width in world units
        halfCamWidth = cam.orthographicSize * cam.aspect;
    }

    void FixedUpdate()
    {
        // Get the target's position using LERP for smooth movement
        Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)target.transform.position, camSpeed * Time.fixedDeltaTime);

        // Calculate the clamped X position with boundaries adjusted by half the camera width
        float minX = minXBoundary.position.x + halfCamWidth;
        float maxX = maxXBoundary.position.x - halfCamWidth;
        float clampedX = Mathf.Clamp(pos.x, minX, maxX);

        // Keep the Y position as the player's position (can add vertical constraints if needed)
        float clampedY = pos.y;

        // Set the camera's position with the clamped X value
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    public Vector2 direction; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 armPos = transform.position;

        // calculate the direction
        direction = mousePos - armPos;

        FaceMouse();
    }

    // take care of rotation of arm
    void FaceMouse(){
        transform.right = direction;
    }
}

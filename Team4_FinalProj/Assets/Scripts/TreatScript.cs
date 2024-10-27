using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatScript : MonoBehaviour
{
    public float LaunchForce;

    public GameObject Treat; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            // when shift is pressed we throw a treat
            Throw();
        }
    }

    void Throw(){
        GameObject TreatIns = Instantiate(Treat, transform.position, transform.rotation);

        // apply force to the treat we just created
        TreatIns.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
    }
}

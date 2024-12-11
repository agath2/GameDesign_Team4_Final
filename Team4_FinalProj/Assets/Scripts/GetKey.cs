using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetKey : MonoBehaviour
{

    public GameObject keyInInventory;
    public bool hasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        keyInInventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            hasKey = true;
            // Destroy(collider.gameObject);
            keyInInventory.SetActive(true);
        }
    }

    public bool checkIfHasKey(){
        if(hasKey){
            keyInInventory.SetActive(false);
        }
        return hasKey;
    }
}

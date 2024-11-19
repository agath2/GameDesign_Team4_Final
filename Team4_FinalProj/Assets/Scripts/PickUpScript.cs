using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private InventoryScript inventory;
    public GameObject itemButton;
    // Start is called before the first frame update
    void Start()
    {
       inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryScript>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            for(int i = 0; i < inventory.slots.Length; i++){
                if(inventory.isFull[i] == false){
                    // Item can be added to the inventory 
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    // destroy the object the player just picked up
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}

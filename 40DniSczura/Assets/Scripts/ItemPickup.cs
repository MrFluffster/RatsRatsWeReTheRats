using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{

    [SerializeField]
    

    private bool playerInRange;
    public int itemID;

    // Use this for initialization
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerInRange && Input.GetButtonDown("Fire1"))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void PickUp()
    {
        PlayerController.instance.AddItem(itemID);
        Destroy(gameObject);
    }

}
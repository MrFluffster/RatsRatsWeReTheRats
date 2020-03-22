using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public bool tankControls;

    public static PlayerController instance;

    public Rigidbody2D rigidBody;
    public Animator animator;
    public float rotationSpeed = 1f;
    public float moveSpeed, backSpeed;

    public float ratAngle;

    public int[] inventory;
    //public GameObject tail;
    //private float lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Making sure ONLY one player is alive
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX, moveY;

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (tankControls)
        {            
            rigidBody.rotation += moveX * rotationSpeed;

            if (moveY != 0)
            {
                if (moveY > 0)
                {
                    rigidBody.velocity = DegreeToVector2(rigidBody.rotation + 90f) * moveY * moveSpeed;
                }
                else
                {
                    rigidBody.velocity = DegreeToVector2(rigidBody.rotation + 90f) * moveY * backSpeed;
                }
            }
            else rigidBody.velocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = new Vector2(moveX, moveY);
            if (direction.magnitude >= 0.5f)
            {
                ratAngle = Mathf.Rad2Deg * Mathf.Atan2(moveY, moveX) - 135f;
                rigidBody.velocity = direction.normalized * moveSpeed;               
                rigidBody.MoveRotation(ratAngle);
                animator.SetBool("Moving", true);
                //tail.transform.SetPositionAndRotation(tail.transform.position, new Quaternion(tail.transform.rotation.x, tail.transform.rotation.y, tail.transform.rotation.z + 1f/* + lastRotation - ratAngle*/, tail.transform.rotation.w));
                //Debug.Log(lastRotation - ratAngle);
                //lastRotation = ratAngle;
            }
            else
            {
                rigidBody.velocity = Vector2.zero;
                animator.SetBool("Moving", false);
                //tail.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -ratAngle, transform.rotation.w);
                //rigidBody.SetRotation(ratAngle);
            }
        }       
    }

    private void SortInventory()
    {
        Array.Sort(inventory);
        Array.Reverse(inventory);
    }

    public bool HasItems()
    {
        SortInventory();
        int counter = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == 0)
            {
                counter++;
            }
        }

        if (counter < inventory.Length)
        {
            return true;
        }
        else return false;
    }

    public void AddItem(int itemID)
    {
        SortInventory();
        for(int i = 0; i < inventory.Length; i++)
        {
            if(inventory[i] == 0)
            {
                inventory[i] = itemID;
                break;
            }
        }
        SortInventory();
    }

    public void RemoveItems(int itemID)
    {
        SortInventory();
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == itemID)
            {
                inventory[i] = 0;               
            }
        }
        SortInventory();
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}

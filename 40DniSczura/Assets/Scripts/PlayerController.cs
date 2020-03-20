using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool tankControls;

    public Rigidbody2D rigidBody;
    public float rotationSpeed = 1f;
    public float moveSpeed, backSpeed;

    public float ratAngle;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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
                ratAngle = Mathf.Rad2Deg * Mathf.Atan2(moveY, moveX) - 90f;
                rigidBody.velocity = direction.normalized * moveSpeed;               
                rigidBody.MoveRotation(ratAngle);
            }
            else
            {
                rigidBody.velocity = Vector2.zero;
                //rigidBody.SetRotation(ratAngle);
            }
        }       
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

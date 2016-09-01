using UnityEngine;
using System.Collections;

public class Jump : GameObjectBehaviour
{
    float PressedTime;
    public float JumpAdd, MaxPressTime;
    public Vector2 JumpForce;
    public bool CanJump;
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && CanJump)
        {
            CanJump = false;
            body.AddForce(JumpForce * 10);
            PressedTime = Time.time + MaxPressTime;
        }
        else if (Input.GetButton("Jump"))
        {
            if (Time.time < PressedTime)
            {
                body.AddForce(Vector2.up * JumpAdd);
            }
        }else if (Input.GetButtonUp("Jump"))
        {
            body.AddForce(Vector2.down * body.velocity.y * 30);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground") && body.velocity.y <= 0)
        {
            CanJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground"))
        {
            CanJump = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Walk Properties")]
    [SerializeField] private float velocity;

    [Header("Jump properties")]
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private int maxQtdeJump;
    private bool isJumping = false;
    private int qtdeJump = 0;

    [Header("Dash Properties")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private int maxDashInAir;
    private int qtdeDash = 0;
    private float lastDashTime;

    [Header("Components")]
    private Rigidbody2D rig;
    private Animator anim;

    #region "Start And Update"

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Jump();
        Dash();
    }

    #endregion

    #region "Movements"

    private void Move()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        rig.velocity = new Vector2(dirX * velocity, rig.velocity.y);

        //If your c# is in 9.0 you could use switch/case statement
        if (rig.velocity.x > 0)
        {
            anim.SetFloat("Horizontal", 1);
        }
        else if (rig.velocity.x < 0)
        {
            anim.SetFloat("Horizontal", -1);
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && qtdeJump < maxQtdeJump)
        {
            rig.velocity = new Vector2(0, jumpStrength);
            qtdeJump++;

            if (qtdeJump > 1)
            {
                anim.SetBool("DoubleJump", true);
            }
        }

        anim.SetFloat("Vertical", rig.velocity.y);
    }

    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && (Time.deltaTime - lastDashTime > dashCoolDown))
        {
            Debug.Log("Acctual Dash");
            if ((isJumping && qtdeDash > maxQtdeJump) || !isJumping)
            {
                
                if (rig.velocity.x == 0)
                {
                    rig.velocity = new Vector2(dashSpeed, rig.velocity.y);
                    qtdeDash++;
                }
                else
                {
                    float dirX = Input.GetAxisRaw("Horizontal");
                    rig.velocity = new Vector2(dirX * dashSpeed, rig.velocity.y);
                }

                lastDashTime = Time.deltaTime;
            }
        }
    }

    #endregion

    #region "Collisions"

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            qtdeJump = 0;
            anim.SetBool("DoubleJump", false);
            isJumping = true;
        }
        else if(collision.gameObject.CompareTag("Walls"))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            anim.SetBool("WallJump", true);
            qtdeJump = 0;

            ContactPoint2D contact = collision.GetContact(0);

            //player (otherCollider) is to the right of the wall
            if (contact.otherCollider.gameObject.transform.position.x > contact.point.x)
            {
                anim.SetFloat("WallJumpValue", -1);
            }
            else
            {
                anim.SetFloat("WallJumpValue", 1);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Walls"))
        {
            anim.SetBool("WallJump", false);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = true;
        }
    }

    #endregion
}

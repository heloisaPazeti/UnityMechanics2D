using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.U2D.Common;
using UnityEditor.U2D.IK;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private float dashForce = 24f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCoolDown = 1f;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Components")]
    private Animator anim;
    private Rigidbody2D rig;
    private TrailRenderer tr;

    #region "Start And Update"

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if(!isDashing)
        {
            Move();
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && (canDash || !isJumping))
            StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        float direction;
        float originalGravity = rig.gravityScale;

        canDash = false;
        isDashing = true;
        rig.gravityScale = 0;

        if (rig.velocity.x > 0)
            direction = 1;
        else
            direction = -1;

        rig.velocity = new Vector2(direction * dashForce, rig.velocity.y);
        tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    /*
    private IEnumerator Dash()
    {
        //Vector2 newVelocity;
        Vector2 newImpulse;
        canDash = false;

        if (rig.velocity.x != 0)
            newImpulse = new Vector2(rig.velocity.x * dashForce, rig.velocity.y);
        else
            newImpulse = new Vector2(dashForce, rig.velocity.y);

        rig.AddForce(newImpulse, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashCoolDown);

        //canDash = true;

        if (!isJumping)
            canDash = true;
        else
        {
            qtdeDash++;

            if (qtdeDash < maxQtdeJump)
                canDash = true;
        }
    }*/

    #endregion

    #region "Collisions"

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            qtdeJump = 0;
            isJumping = false;
            anim.SetBool("DoubleJump", false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Properties")]
    [SerializeField] private float velocity;
    [SerializeField] private float jumpStrength;
    [SerializeField] private int maxQtdeJump;
    private bool isJumping = false;
    private int qtdeJump = 0;

    [Header("Components")]
    private Rigidbody2D rig;
    private Animator anim;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(dirX * velocity, rig.velocity.y);

        if (Input.GetButtonDown("Jump") && qtdeJump < maxQtdeJump)
        {
            rig.velocity = new Vector2(0, jumpStrength);
            isJumping = true;
            qtdeJump++;

            if(qtdeJump > 1)
            {
                anim.SetBool("DoubleJump", true);
            }
        }

        if (rig.velocity.x > 0)
        {
            anim.SetFloat("Horizontal", 1);
        }  
        else if (rig.velocity.y < 0)
        {
            anim.SetFloat("Horizontal", -1);
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
        }
            
        anim.SetFloat("Vertical", rig.velocity.y);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            qtdeJump = 0;
            anim.SetBool("DoubleJump", false);
            anim.SetBool("WallJump", false);
        }
        else if(collision.gameObject.CompareTag("Walls") && isJumping)
        {
            anim.SetBool("WallJump", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Properties")]
    [SerializeField] private float velocity;
    [SerializeField] private float jumpStrength;

    [Header("Components")]
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Properties")]
    private bool isJumping = false;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(dirX * velocity, rig.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.velocity = new Vector2(0, jumpStrength);
            isJumping = true;
        }

        if (rig.velocity.x > 0)
        {
            anim.SetFloat("Horizontal", 1);
        }  
        else
        {
            anim.SetFloat("Horizontal", -1);
        }
            
        anim.SetFloat("Vertical", rig.velocity.y);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            anim.SetBool("WallJump", false);
        }
        else if(collision.gameObject.CompareTag("Walls") && isJumping)
        {
            anim.SetBool("WallJump", true);
        }
    }
}

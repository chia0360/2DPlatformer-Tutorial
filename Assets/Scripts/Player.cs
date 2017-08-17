using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidBody;

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;
    private bool run;

    private bool facingRight;

	// Use this for initialization
	void Start ()
    {
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
        ResetValues();
	}

    private void HandleMovement(float horizontal)
    {
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y); // Consist of x = -1, y = 0 values.
        }

        if (run && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            myAnimator.SetBool("run", true);
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            myAnimator.SetBool("run", false);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidBody.velocity = Vector2.zero;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            run = false;
        }
        
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void ResetValues()
    {
        attack = false;
        run = false;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Player))]
public class Player : MonoBehaviour
{
	public enum MovementMode {OnGround, OnAir, OnClimbing};
	private MovementMode PlayerMode;

    public XboxController controller;

    public float i;

    [HideInInspector]
    public static GameObject reference;

    [HideInInspector]
    public bool Death = false;

    //Velocity Variables ground
    [Header("MoviminentoNormal")]
    [SerializeField]
    float groundAccel;
    [SerializeField]
    float groundMaxSpeed;
    [SerializeField]
    float crouchedMaxSpeed;
    [SerializeField]
    float groundFriction;
    [SerializeField]
    float gravityOnGround;
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    public static bool grounded;

    [Header("OnAir")]
    [SerializeField]
    float gravityOnAir;
    [SerializeField]
    float airAccel;
    [SerializeField]
    float maxVericalGlideSpeed;
    [SerializeField]
    float airGlideAccel;
    [SerializeField]
    float airFriction;
    [SerializeField]
    float maxJumpVelocity;
    [SerializeField]
    float minJumpVelocity;
    [SerializeField]
    float timeLeft2Jump = 2.0f;
    float instTimeLeft2Jump;

    [Header("OnClimbing")]
	public float ClimbingSpeed;
    [SerializeField]
    float grabXInterpolation;
    [SerializeField]
    float climbSpeed;
    [HideInInspector]
    public bool climbing;

    Scr_ObjetoTrepar grabbedTransform;

    [Header("Otros")]
    public ContactFilter2D cf2d;
    private ContactPoint2D[] contacts = new ContactPoint2D[16];
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    Collider2D groundChecker;
    private float velocityYSmoothing;
    [SerializeField]
    Animator anim;

    bool crouch = false;
    bool planeo = false;
    bool golpePurificante = false;
    bool slide = false;

    bool tieneParaguas = false;

    bool canStandUp = true;

    bool canJump = true;

    bool planear = true;

    public bool canMove = true;

    private void Awake()
    {
        if (reference == null)
            reference = gameObject;

        else //if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
    }

    private bool m_FacingRight = false;
    float contador;
    float crouchTime;
    Vector2 standSize;
    Vector2 crouchSize;
    Vector2 StartSize;
    Vector2 endSize;
    //inputs
    float axisX;
    float axisY;
    bool jumpPressed;
    bool jumpReleased;
    bool jumpPressedBefore;
    bool crouchButtonPress;
    private void Update()
    {
        //axisX = XCI.GetAxis(XboxAxis.LeftStickX, controller);
        axisX = (Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickX, controller)) < Mathf.Abs(Input.GetAxisRaw("Horizontal"))) ? Input.GetAxisRaw("Horizontal") : XCI.GetAxis(XboxAxis.LeftStickX, controller);
        //axisY = XCI.GetAxis(XboxAxis.LeftStickY, controller);
        axisY = (Mathf.Abs(XCI.GetAxis(XboxAxis.LeftStickY, controller)) < Mathf.Abs(Input.GetAxisRaw("Vertical"))) ? Input.GetAxisRaw("Vertical") : XCI.GetAxis(XboxAxis.LeftStickY, controller);

        jumpPressed = (Input.GetButton("Jump") || XCI.GetButton(XboxButton.A));
        jumpReleased = (Input.GetButtonUp("Jump") || XCI.GetButtonUp(XboxButton.A));

        slide = (rb2d.GetContacts(cf2d, contacts) <= 0);

        crouchButtonPress = (Input.GetKey(KeyCode.S) || XCI.GetButton(XboxButton.LeftBumper)) && canMove;

        if (crouchButtonPress)//(Input.GetKey(KeyCode.S) && canMove == true)
        {
            crouch = true;
        }
        else if (!canStandUp && canMove)//((Input.GetKeyUp(KeyCode.S) && canStandUp == false && canMove == true))
        {
            crouch = true;
        }
        else if (canStandUp && canMove)
        {
            crouch = false;
        }

        //Esto hay que hacerlo en la animacion
        //if (!crouch)
        //{
        //    GetComponent<BoxCollider2D>().size = Vector3.Lerp(GetComponent<BoxCollider2D>().size, new Vector3(0.45f, 0.85f, 1), 0.2f);
        //    GetComponent<BoxCollider2D>().offset = Vector3.Lerp(GetComponent<BoxCollider2D>().offset, new Vector3(0, 0.4256001f, 0), 0.2f);
        //}
        //else
        //{
        //    GetComponent<BoxCollider2D>().size = Vector3.Lerp(GetComponent<BoxCollider2D>().size, new Vector3(0.6f, 0.6f, 1), 0.2f);
        //    GetComponent<BoxCollider2D>().offset = Vector3.Lerp(GetComponent<BoxCollider2D>().offset, new Vector3(0, 0.31f, 0), 0.2f);
        //}

        anim.SetBool("crouch", crouch);

        //Input Golpe purificante
        if ((Input.GetMouseButtonDown(1) || XCI.GetButtonDown(XboxButton.B)) && canMove)//(Input.GetMouseButtonDown(1) && canMove == true)
            golpePurificante = true;
        else if (canMove)
            golpePurificante = false;
        
        anim.SetFloat("velocityX", rb2d.velocity.x);
        anim.SetBool("golpePurificante", golpePurificante);

        tieneParaguas = Scr_TieneParaguas.paraguas;

        canStandUp = gameObject.GetComponentInChildren<Scr_CrouchCheck>().canStandUp;

        canJump = Scr_EmpujarTirar.canJump;

        planear = Scr_QuitarPlaneo.planear;

        if (rb2d.velocity.x > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (rb2d.velocity.x < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

private void FixedUpdate()
    {
		Grounded();
        
		Vector2 move = Vector2.zero;

		if (!Death) {
			switch (PlayerMode) {
            case MovementMode.OnGround:
				UpdateGround ();
				break;
			case MovementMode.OnAir:
				UpdateAir ();
				break;
			case MovementMode.OnClimbing:
				UpdateClimb ();
				break;
			}
		}

        //if (Input.GetKey(KeyCode.LeftControl))
        //    crouch = true;
        //else
        //    crouch = false;

        //if (Input.GetMouseButtonDown(1))
        //    golpePurificante = true;
        //else
        //    golpePurificante = false;

        //anim.SetBool("grounded", grounded);
        //anim.SetBool("crouch", crouch);
        //anim.SetFloat("velocityX", rb2d.velocity.x);
        //anim.SetBool("golpePurificante", golpePurificante);
    }

    private void UpdateGround()
    {
        //Setear velocidad máxima
        float speedToUse = (crouchButtonPress && !slide) ? crouchedMaxSpeed : groundMaxSpeed;

		float xSpeed = 0;
		if (Mathf.Abs(axisX) > 0.1f && !slide && canMove)//(Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f && !slide && canMove)
        {
            //Apply acceleration
            xSpeed = rb2d.velocity.x + axisX * groundAccel * Time.deltaTime;//xSpeed = rb2d.velocity.x + Input.GetAxis("Horizontal") * groundAccel * Time.deltaTime;
        }
        else if (canMove)
        {
            //Apply Friction
            xSpeed = rb2d.velocity.x * (1 - groundFriction * Time.deltaTime);
        }

        xSpeed = Mathf.Clamp(xSpeed, - speedToUse ,speedToUse );

        float g = !slide && Mathf.Abs(axisX) > 0.1f ? 0 : gravityOnGround;//!slide && Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f ? 0 : gravityOnGround;

        float ySpeed = rb2d.velocity.y + g * Time.deltaTime;

		//Chequear Salto
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;

		if (jumpDown && grounded && !crouch && canJump && canMove)
		{
			ySpeed = ySpeed + maxJumpVelocity;
		}

		rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (!grounded && canMove)
			PlayerMode = MovementMode.OnAir;

		if (Mathf.Abs(axisY) > 0.1f && grabbedTransform != null && canMove)//(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null && canMove == true)
        {
			PlayerMode = MovementMode.OnClimbing;
		}
	}

    public bool ignoreJumpDepress;

	private void UpdateAir(){
		//Setear velocidad máxima
		float speedToUse = groundMaxSpeed;
		float xSpeed = 0;
        //jumpPressed = (Input.GetButton("Jump") || XCI.GetButton(XboxButton.A));
        jumpPressedBefore = jumpPressed;
        float aceleationToUse = planeo ? airGlideAccel : airAccel;

        if (instTimeLeft2Jump > 0)
            instTimeLeft2Jump -= Time.deltaTime;

        if (Mathf.Abs(axisX) > 0.1f && canMove)//(Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f && canMove == true)
        {
            //Apply acceleration
            xSpeed = rb2d.velocity.x + axisX * aceleationToUse * Time.deltaTime;//xSpeed = rb2d.velocity.x + Input.GetAxis("Horizontal") * aceleationToUse * Time.deltaTime;
        }
        else if (canMove)
        {
            //Apply Friction
            xSpeed = rb2d.velocity.x * (1 - airFriction * Time.deltaTime);
        }
        xSpeed = Mathf.Clamp(xSpeed, - speedToUse ,speedToUse );

		float gravityToUse = rb2d.velocity.y > 0 ? gravityOnAir : gravityOnGround;
        //if (rb2d.velocity.y > 0)
        //    gravityToUse = gravityOnAir;

        float ySpeed = rb2d.velocity.y + gravityToUse * Time.deltaTime;

        //Chequear Salto
        if (jumpReleased && rb2d.velocity.y > minJumpVelocity && !ignoreJumpDepress && canMove)//(Input.GetButtonUp("Jump") && rb2d.velocity.y > minJumpVelocity && !ignoreJumpDepress && canMove)
        {
			ySpeed = minJumpVelocity;
		}

        if (ySpeed < 0)
            ignoreJumpDepress = false;
        
        if(tieneParaguas && planear)
        {
            if (jumpPressed && rb2d.velocity.y < 0)
            {
                ySpeed = Mathf.SmoothDamp(rb2d.velocity.y, maxVericalGlideSpeed, ref velocityYSmoothing, .1f);
                planeo = true;
                anim.SetBool("planeando", planeo);
            }
            else
            {
                planeo = false;
                anim.SetBool("planeando", planeo);
            }
        }
                
        rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (grounded)
			PlayerMode = MovementMode.OnGround;

		if (Mathf.Abs(axisY) > 0.1f && grabbedTransform != null && canMove)//(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null && canMove == true)
        {
			PlayerMode = MovementMode.OnClimbing;
		}
	}

	private void UpdateClimb()
    {
		//jumpPressed = Input.GetButton("Jump");
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;
		if (jumpDown || grabbedTransform == null && canMove)
        {
			//rb2d.velocity = maxJumpVelocity * Vector2.up;
			PlayerMode = MovementMode.OnAir;
            rb2d.velocity = new Vector2(rb2d.velocity.x,  maxJumpVelocity);
        }
        else if(transform.position.y >= grabbedTransform.upPoint.position.y || transform.position.y <= grabbedTransform.downPoint.position.y || grounded && Input.GetAxis ("Vertical") < 0 && canMove == true)
			PlayerMode = MovementMode.OnGround;

        else if (canMove)
        {
            Vector2 climb = axisY * climbSpeed * Vector2.up;//Vector2 climb = Input.GetAxis ("Vertical") * climbSpeed * Vector2.up;
            rb2d.velocity = climb;
			transform.position =  new Vector3( Mathf.Lerp (rb2d.position.x, grabbedTransform.transform.position.x, Time.deltaTime * grabXInterpolation) , rb2d.position.y, transform.position.z);
		}
	}

    ContactFilter2D cf = new ContactFilter2D();
    Collider2D[] cols = new Collider2D[1];

    private void Grounded()
    {
        // grounded = groundChecker.IsTouchingLayers(m_WhatIsGround);

        cf.layerMask = m_WhatIsGround;
        cf.useLayerMask = true;

        if (groundChecker.OverlapCollider (cf, cols) > 0)
        {
            grounded = true;
            instTimeLeft2Jump = timeLeft2Jump;
        }
        else
        {
            grounded = false;
        }

        anim.SetBool("grounded", grounded);
    }

	public void OnClimb (Scr_ObjetoTrepar stair, bool enter)
    {
		if (enter)
        {
			grabbedTransform = stair;
		}
        else if (grabbedTransform == stair)
        {
			grabbedTransform = null;
		}	
	}
}
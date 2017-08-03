using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class Player : MonoBehaviour
{
	public enum MovementMode {OnGround, OnAir, OnClimbing};
	private MovementMode PlayerMode;

    public float i;

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

    private bool grounded;

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
    bool jumpPressed;
    bool jumpPressedBefore;
    [SerializeField]
    float maxJumpVelocity;
    [SerializeField]
    float minJumpVelocity;

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

    private void Update()
    {
        slide = (rb2d.GetContacts(cf2d, contacts) <= 0);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouch = true;

            //GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.25f);
            //GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.6f);

        }
        else
        {
            crouch = false;

            GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.6f);

        }

        anim.SetBool("crouch", crouch);

        if (Input.GetMouseButtonDown(1))
            golpePurificante = true;
        else
            golpePurificante = false;
        
        anim.SetFloat("velocityX", rb2d.velocity.x);
        anim.SetBool("golpePurificante", golpePurificante);
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

	private void UpdateGround(){

		//Setear velocidad máxima
		float speedToUse = groundMaxSpeed;
		if(Input.GetKey(KeyCode.LeftControl) && !slide)
        {
			speedToUse = crouchedMaxSpeed;
		}
		float xSpeed = 0;
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f && !slide)
        {
            //Apply acceleration
            xSpeed = rb2d.velocity.x + Input.GetAxis("Horizontal") * groundAccel * Time.deltaTime;
        }
        else
            //Apply Friction
            xSpeed = rb2d.velocity.x * (1 - groundFriction * Time.deltaTime);

        xSpeed = Mathf.Clamp(xSpeed, - speedToUse ,speedToUse );

        Debug.Log(!slide && Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f);

        float g = !slide && Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f ? 0 : gravityOnGround;

        float ySpeed = rb2d.velocity.y + g * Time.deltaTime;

		//Chequear Salto
		jumpPressed = Input.GetButton("Jump");
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;

		if (jumpDown && grounded)
		{
			ySpeed = maxJumpVelocity;
		}

		rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (!grounded)
			PlayerMode = MovementMode.OnAir;

		if(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null){
			PlayerMode = MovementMode.OnClimbing;
		}
	}

	private void UpdateAir(){
		//Setear velocidad máxima
		float speedToUse = groundMaxSpeed;
		float xSpeed = 0;
        jumpPressed = Input.GetButton("Jump");
        jumpPressedBefore = jumpPressed;
        float aceleationToUse = planeo ? airGlideAccel : airAccel;

        if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f)
        {
            //Apply acceleration
            xSpeed = rb2d.velocity.x + Input.GetAxis("Horizontal") * aceleationToUse * Time.deltaTime;
        }
        else
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
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > minJumpVelocity)
		{
			ySpeed = minJumpVelocity;
		}
        
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
        
        rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (grounded)
			PlayerMode = MovementMode.OnGround;

		if(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null){
			PlayerMode = MovementMode.OnClimbing;
		}
	}

	private void UpdateClimb()
    {
		jumpPressed = Input.GetButton("Jump");
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;
		if (jumpDown || grabbedTransform == null)
        {
			//rb2d.velocity = maxJumpVelocity * Vector2.up;
			PlayerMode = MovementMode.OnAir;
		}
        else if(transform.position.y >= grabbedTransform.upPoint.position.y || transform.position.y <= grabbedTransform.downPoint.position.y || grounded && Input.GetAxis ("Vertical") < 0)
			PlayerMode = MovementMode.OnGround;

        else
        {
			Vector2 climb = Input.GetAxis ("Vertical") * climbSpeed * Vector2.up;
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
        }
        else
        {
            grounded = false;
        }

        anim.SetBool("grounded", grounded);
    }

	public void OnClimb (Scr_ObjetoTrepar stair, bool enter){
		if (enter) {
			grabbedTransform = stair;
		} else if (grabbedTransform == stair) {
			grabbedTransform = null;
		}	
	}
}
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
    bool jumpPressed;
    bool jumpPressedBefore;
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

    bool canStandUp = false;

    bool canJump = true;

    bool planear = true;

    bool canMove = true;

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        slide = (rb2d.GetContacts(cf2d, contacts) <= 0);

        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;

        }

        if (Input.GetKeyUp(KeyCode.S) && canStandUp == true)
        {
            crouch = false;
            
        }

        if (!crouch)
        {
            GetComponent<BoxCollider2D>().size = Vector3.Lerp(GetComponent<BoxCollider2D>().size, new Vector3(0.45f, 0.85f, 1) , 0.2f);
            GetComponent<BoxCollider2D>().offset = Vector3.Lerp(GetComponent<BoxCollider2D>().offset, new Vector3(0, 0.4256001f, 0), 0.2f) ;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = Vector3.Lerp(GetComponent<BoxCollider2D>().size, new Vector3(0.6f, 0.6f, 1), 0.2f);
            GetComponent<BoxCollider2D>().offset = Vector3.Lerp(GetComponent<BoxCollider2D>().offset, new Vector3(0, 0.31f, 0), 0.2f);
        }

        anim.SetBool("crouch", crouch);

        if (Input.GetMouseButtonDown(1))
            golpePurificante = true;
        else
            golpePurificante = false;
        
        anim.SetFloat("velocityX", rb2d.velocity.x);
        anim.SetBool("golpePurificante", golpePurificante);

        tieneParaguas = Scr_TieneParaguas.paraguas;

        canStandUp = Scr_CrouchCheck.canStandUp;

        canJump = Scr_EmpujarTirar.canJump;

        if (gameObject.transform.position.x > -25)
        {
            tieneParaguas = true;
        }

        planear = Scr_QuitarPlaneo.planear;
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
		if(Input.GetKey(KeyCode.S) && !slide)
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

        //Debug.Log(!slide && Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f);

        float g = !slide && Input.GetAxis("Horizontal") < 0.1f && Input.GetAxis("Horizontal") > -0.1f ? 0 : gravityOnGround;

        float ySpeed = rb2d.velocity.y + g * Time.deltaTime;

		//Chequear Salto
		jumpPressed = Input.GetButton("Jump");
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;

		if (jumpDown && grounded && !crouch && canJump)
		{
			ySpeed = ySpeed + maxJumpVelocity;
            Debug.Log(ySpeed);
		}

		rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (!grounded)
			PlayerMode = MovementMode.OnAir;

		if(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null){
			PlayerMode = MovementMode.OnClimbing;
		}
	}

    public bool ignoreJumpDepress;

	private void UpdateAir(){
		//Setear velocidad máxima
		float speedToUse = groundMaxSpeed;
		float xSpeed = 0;
        jumpPressed = Input.GetButton("Jump");
        jumpPressedBefore = jumpPressed;
        float aceleationToUse = planeo ? airGlideAccel : airAccel;

        if (instTimeLeft2Jump > 0)
            instTimeLeft2Jump -= Time.deltaTime;

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
        if (Input.GetButtonUp("Jump") && rb2d.velocity.y > minJumpVelocity && !ignoreJumpDepress)
		{
			ySpeed = minJumpVelocity;
		}

        if (ySpeed < 0)
            ignoreJumpDepress = false;

        if(tieneParaguas == true && planear == true)
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
            rb2d.velocity = new Vector2(rb2d.velocity.x,  maxJumpVelocity);
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
            instTimeLeft2Jump = timeLeft2Jump;
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
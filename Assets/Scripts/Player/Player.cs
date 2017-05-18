using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum MovementMode {OnGround, OnAir, OnClimbing};
	public MovementMode PlayerMode;

    public bool Death = false;

    public float maxHorizontalSpeed = 7;
    public float maxVericalGlideSpeed = 0;
    float accelerationTimeAirborne = .3f;
    float accelerationTimeGrounded = .01f;
    float acceleration;
    [Range(0, 1)] [Tooltip("Reduccion de la velocidad de movimiento normal cuando esta agachado")]
    public float CrouchSpeed = .25f;

    private Vector2 Velocity;

    private bool grounded;
    const float k_GroundedRadius = .1f;                // Radius of the overlap circle to determine if grounded
	[SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;

    private SpriteRenderer spriteRenderer;
	private Animator animator;
	[SerializeField]
    private Rigidbody2D rb2d;

    private float m_mainGravityScale;
    private float m_glideGravityScale;
	[SerializeField]
    float maxJumpVelocity;
	[SerializeField]
    float minJumpVelocity;
    float velocityXSmoothing;
    float velocityYSmoothing;

	public bool climbing;
	public float ClimbingSpeed;

    // Use this for initialization
    void Start()
	{
    }

    private void Update()
	{     
    }


    private void FixedUpdate()
    {
		Grounded();

		Velocity = Vector2.zero;
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
		/*
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(rb2d.velocity.x) / maxHorizontalSpeed);*/
    }

	//Velocity Variables ground
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
	float gravityOnAir;
	[SerializeField]
	float airAccel;
	[SerializeField]
	float airFriction;
	[SerializeField]

	bool jumpPressed;
	bool jumpPressedBefore;

	private void UpdateGround(){
		//Setear velocidad máxima
		float speedToUse = groundMaxSpeed;
		if(Input.GetKey(KeyCode.LeftControl)){
			speedToUse = crouchedMaxSpeed;
		}
		float xSpeed = 0;
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f)
			//Apply acceleration
			xSpeed = rb2d.velocity.x + Input.GetAxis ("Horizontal") * groundAccel * Time.deltaTime;
		else
			//Apply Friction
			xSpeed = rb2d.velocity.x * (1 - groundFriction * Time.deltaTime);
		xSpeed = Mathf.Clamp(xSpeed, - speedToUse ,speedToUse );
		float ySpeed = rb2d.velocity.y + gravityOnGround * Time.deltaTime;

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
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.1f)
			//Apply acceleration
			xSpeed = rb2d.velocity.x + Input.GetAxis ("Horizontal") * airAccel * Time.deltaTime;
		else {
			//Apply Friction
			xSpeed = rb2d.velocity.x * (1 - airFriction * Time.deltaTime);
		}
		xSpeed = Mathf.Clamp(xSpeed, - speedToUse ,speedToUse );
		Debug.Log (xSpeed);

		float gravityToUse = gravityOnGround;
		if(rb2d.velocity.y > 0)
			gravityToUse = gravityOnAir;
		float ySpeed = rb2d.velocity.y + gravityToUse * Time.deltaTime;

		//Chequear Salto
		jumpPressed = Input.GetButton("Jump");
		jumpPressedBefore = jumpPressed;
		if (!jumpPressed && rb2d.velocity.y > minJumpVelocity)
		{
			ySpeed = minJumpVelocity;
		}

		rb2d.velocity = xSpeed*Vector2.right + ySpeed*Vector2.up;

		if (grounded)
			PlayerMode = MovementMode.OnGround;

		if(Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f && grabbedTransform != null){
			PlayerMode = MovementMode.OnClimbing;
		}
	}

	Scr_ObjetoTrepar grabbedTransform;
	[SerializeField]
	float grabXInterpolation;
	[SerializeField]
	float climbSpeed;

	private void UpdateClimb(){

		jumpPressed = Input.GetButton("Jump");
		bool jumpDown = jumpPressed && !jumpPressedBefore;
		jumpPressedBefore = jumpPressed;
		if (jumpDown || grabbedTransform == null) {
			rb2d.velocity = maxJumpVelocity * Vector2.up;
			PlayerMode = MovementMode.OnAir;
		}else if(transform.position.y >= grabbedTransform.upPoint.position.y || transform.position.y <= grabbedTransform.downPoint.position.y || grounded && Input.GetAxis ("Vertical") < 0){
			PlayerMode = MovementMode.OnGround;
		}else{
			Vector2 climb = Input.GetAxis ("Vertical") * climbSpeed * Vector2.up;
			rb2d.velocity = climb;
			transform.position =  new Vector3( Mathf.Lerp (rb2d.position.x, grabbedTransform.transform.position.x, Time.deltaTime * grabXInterpolation) , rb2d.position.y, transform.position.z);
		}
	}
	[SerializeField]
	Collider2D groundChecker;

    private void Grounded()
    {
		grounded = groundChecker.IsTouchingLayers(m_WhatIsGround);
    }

	public void OnClimb (Scr_ObjetoTrepar stair, bool enter){
		if (enter) {
			grabbedTransform = stair;
		} else if (grabbedTransform == stair) {
			grabbedTransform = null;
		}	
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 position = transform.position + Vector3.up * maxJumpHeight;
        Gizmos.DrawLine(position + Vector2.left * .25f, position + Vector2.right * .25f);

        position = transform.position + Vector3.up * minJumpHeight;
        Gizmos.DrawLine(position + Vector2.left * .25f, position + Vector2.right * .25f);
    }
}

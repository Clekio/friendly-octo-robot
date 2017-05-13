using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float maxSpeed = 7;
    float accelerationTimeAirborne = .3f;
    float accelerationTimeGrounded = .01f;
    float acceleration;
    [Range(0, 1)] [Tooltip("Reduccion de la velocidad de movimiento normal cuando esta agachado")]
    public float CrouchSpeed = .25f;

    private Vector2 Velocity;

    private bool grounded;
    const float k_GroundedRadius = .02f;                // Radius of the overlap circle to determine if grounded
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float GlideTime = 1.2f;

    private SpriteRenderer spriteRenderer;
	private Animator animator;
    private Rigidbody2D rb2d;

    private float m_mainGravityScale;
    private float m_glideGravityScale;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    // Use this for initialization
    void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        m_WhatIsGround = Physics2D.GetLayerCollisionMask(gameObject.layer);

        m_mainGravityScale = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2) ;
        rb2d.gravityScale = m_mainGravityScale / Physics2D.gravity.y;
        m_glideGravityScale = -(6 * maxJumpHeight) / Mathf.Pow(GlideTime, 8);
        maxJumpVelocity = Mathf.Abs(m_mainGravityScale) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(m_mainGravityScale) * minJumpHeight);
    }

    private void Update()
	{
        Velocity = Vector2.zero;
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && grounded)
		{
            rb2d.velocity = Vector2.up * maxJumpVelocity;
        }
		else if (Input.GetButtonUp("Jump"))
		{
            if (rb2d.velocity.y > minJumpVelocity)
            {
                rb2d.velocity = Vector2.up * minJumpVelocity;
            }
		}

        if (Input.GetButton("Jump") && rb2d.velocity.y < 0)
            rb2d.gravityScale = m_glideGravityScale / Physics2D.gravity.y;
        else
            rb2d.gravityScale = m_mainGravityScale / Physics2D.gravity.y;

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite)
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(rb2d.velocity.x) / maxSpeed);

        float m_targetVelocityX = move.x * maxSpeed;
        
        //Velocidad de movimiento lateral cuando el jugador esta...
        if (Input.GetKey(KeyCode.LeftControl) && grounded)              //...agachado.
            Velocity.x = Mathf.SmoothDamp(rb2d.velocity.x, m_targetVelocityX * CrouchSpeed, ref velocityXSmoothing, accelerationTimeGrounded);

        else if (grounded)                         //...en el suelo.
            Velocity.x = Mathf.SmoothDamp(rb2d.velocity.x, m_targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);

        else if (Input.GetButton("Jump") && rb2d.velocity.y < 0)    //...con el paraguas.
            Velocity.x = Mathf.SmoothDamp(rb2d.velocity.x, m_targetVelocityX, ref velocityXSmoothing, accelerationTimeGrounded);

        else if (!grounded)                                         //...callendo.
            Velocity.x = Mathf.SmoothDamp(rb2d.velocity.x, m_targetVelocityX, ref velocityXSmoothing, accelerationTimeAirborne);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Velocity.x, rb2d.velocity.y);
        Grounded();
    }

    private void Grounded()
    {
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
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

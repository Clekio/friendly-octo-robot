using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public enum MovementMode { OnGround, OnAir, OnClimbing };
    private MovementMode PlayerMode = MovementMode.OnAir;

    [HideInInspector]
    public Vector3 velocity;
    float velocityXSmoothing;
    float velocityYSmoothing;

    Vector2 directionalInput;

    private bool Dead = false;
    private bool canMove = true;

    [Header("Referencias")]
    public Controller2D controller;
    public PlayerInput input;
    [SerializeField]
    Animator anim;

    void Start()
    {
        if(!controller)
            controller = GetComponent<Controller2D>();
        if (!input)
            input = GetComponent<PlayerInput>();

        PlayerClimbInfo.empty = true;

        gravityOnGround = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravityOnGround) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravityOnGround) * minJumpHeight);
        secondJumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(gravityOnGround) * secondJumpHeight);
    }

    void FixedUpdate()
    {
        directionalInput = input.Direction();

        if (!Dead && canMove)
        {
            switch (PlayerMode)
            {
                case MovementMode.OnGround:
                    UpdateGround();
                    break;
                case MovementMode.OnAir:
                    UpdateAir();
                    break;
                case MovementMode.OnClimbing:
                    UpdateClimb();
                    break;
            }
        }

        UpdateAnimations();

        controller.Move(velocity * Time.deltaTime, directionalInput);
    }

    #region OnGround

    [Header("OnGround")]
    [SerializeField]
    [Range(0,10)][Tooltip("Velocidad maxima del juegador cuando esta en el suelo")]
    float groundSpeed = 6;
    [SerializeField]
    [Range(0,2)][Tooltip("Tiempo que se tarda en alcanzar la velozidad maxima en el suelo")]
    float timeGround = 0.1f;
    [SerializeField]
    [Range(0,10)][Tooltip("Velocidad maxima del juegador agachado en el suelo")]
    float crouchedSpeed = 4;
    [SerializeField]
    [Range(0,2)][Tooltip("Tiempo que se tarda en alcanzar la velocidad maxima agachado")]
    float timeCrouch = 0.15f;
    private float gravityOnGround;

    public static bool grounded;
    private void UpdateGround()
    {
        Vector2 speedToUse = Vector2.zero;
        bool crouch = input.Crouch();

        //Horizontal Movement
        float targetVelocityX = input.Horizontal() * ((crouch) ? crouchedSpeed : groundSpeed);
        float smoothTime = (crouch) ? timeCrouch : timeGround;
        speedToUse.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, smoothTime);

        // Vertical Movement
        if (controller.collisions.above || controller.collisions.below)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                speedToUse.y = velocity.y + controller.collisions.slopeNormal.y * - 10 * Time.deltaTime;
            }
            else
            {
                speedToUse.y = 0;
            }
        }

        if (input.JumpHold() && controller.collisions.below && !crouch)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (Mathf.Abs(input.Horizontal()) != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    SetMovementAir();
                    speedToUse.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                    speedToUse.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                }
            }
            else
            {
                SetMovementAir();
                speedToUse.y = maxJumpVelocity;
            }
        }

        speedToUse.y += (gravityOnGround * Time.deltaTime);
        //speedToUse.y += velocity.y + (gravityOnGround * Time.deltaTime);

        if (!controller.collisions.below)
            SetMovementAir();

        if (Mathf.Abs(input.Vertical()) > 0.5f && !PlayerClimbInfo.empty && !crouch)
            SetMovementClimb();

        velocity = speedToUse;
    }

    private void SetMovementGround()
    {
        PlayerMode = MovementMode.OnGround;
    }
#endregion

    #region On Air

    [Header("OnAir")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    [SerializeField][Range(-100,0)]
    float gravityOnAir;

    float maxJumpVelocity;
    float minJumpVelocity;

    [Space]
    bool planeando = false;
    public bool tieneParaguas = true;
    public float glideFriction;

    bool canSecondJump;
    [SerializeField]
    float secondJumpHeight = 2;
    float secondJumpSpeed = 20;
    [SerializeField]
    [Range(0, 5)]
    float timeToSecondJump = 1;

    [Space]
    [SerializeField]
    [Range(0,10)][Tooltip("velocidad HORIZONTAL maxima en el salto")]
    float airSpeed = 3;
    [SerializeField]
    [Range(0, 2)][Tooltip("Tiempo que tarda en alcanzar la velocidad HORIZONTAL maxima en el salto")]
    float airTime = 0.3f;
    
    [SerializeField]
    [Range(-10, 0)][Tooltip("Velocidad VERTICAL maxima de planeo")]
    float yGlideSpeed = -2;
    [SerializeField]
    [Range(0,2)][Tooltip("Tiempo que tarda en alcanzar la velocidad VERICAL maxima de planeo")]
    float yGlideTime = 0.2f;

    [SerializeField]
    [Range(0, 10)][Tooltip("Velocidad HORIZONTAL maxima de planeo")]
    float xGlideSpeed = 5;
    [SerializeField]
    [Range(0, 2)][Tooltip("Tiempo que tarda en alcanzar la velocidad HORIZONTAL maxima de planeo")]
    float xTimeGlide = 0.2f;

    private void UpdateAir()
    {
        Vector2 speedToUse = Vector2.zero;

        //Horizontal Movement
        float targetVelocityX = input.Horizontal() * ((planeando) ? xGlideSpeed : airSpeed);
        float smoothTime = (planeando) ? xTimeGlide : airTime;
        speedToUse.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, smoothTime);

        //Vertical Movement
        float gravityToUse = velocity.y < 0 ? gravityOnAir : gravityOnGround;
        speedToUse.y = velocity.y + gravityToUse * Time.deltaTime;

        if (input.JumpUp() && velocity.y > minJumpVelocity)
            speedToUse.y = minJumpVelocity;

        if (tieneParaguas)
        {
            if (input.JumpHold() && velocity.y < 0)
            {
                speedToUse.y = Mathf.SmoothDamp(velocity.y, yGlideSpeed, ref velocityYSmoothing, yGlideTime);
                planeando = true;
                //anim.SetBool("planeando", planeo);
            }
            else if (planeando && input.JumpUp())
                Invoke("ResetPlaneo", timeToSecondJump);

            if (input.JumpDown() && canSecondJump && planeando)
            {
                speedToUse.y = secondJumpSpeed;
                canSecondJump = false;
            }
        }

        velocity = speedToUse;

        if (controller.collisions.below)
            SetMovementGround();

        if (Mathf.Abs(input.Vertical()) > 0.5f && !PlayerClimbInfo.empty)
            SetMovementClimb();
    }

    public void SetMovementAir()
    {
        PlayerMode = MovementMode.OnAir;
        canSecondJump = true;
    }

    void ResetPlaneo()
    {
        planeando = false;
        //anim.SetBool("planeando", planeo);
    }
#endregion

    #region Climbing

    [Header("Climb")]
    [SerializeField]
    float grabXInterpolation;
    [SerializeField]
    float climbSpeed;
    [HideInInspector]
    public ClimbInfo PlayerClimbInfo;
    private void UpdateClimb()
    {
        if (input.JumpHold())
        {
            SetMovementAir();
            velocity.y = minJumpVelocity;
        }
        else if (Mathf.Abs(input.Horizontal()) > 0.5f || PlayerClimbInfo.empty)
        {
            SetMovementGround();
        }

        else
        {
            velocity.y = input.Vertical() * climbSpeed;
            velocity.x = 0;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, PlayerClimbInfo.downPoint.x, Time.deltaTime * grabXInterpolation), transform.position.y, 0);
        }
    }

    private void SetMovementClimb()
    {
        PlayerMode = MovementMode.OnClimbing;
        //Debug.Log(PlayerMode);
    }
    #endregion

    #region Anmiaciones

    private bool m_FacingRight = false; 
    private void UpdateAnimations()
    {
        anim.SetBool("crouch", input.Crouch());
        anim.SetFloat("velocityX", velocity.x);
        anim.SetBool("golpePurificante", input.GolpePurificante());
        anim.SetBool("planeando", planeando);
        anim.SetBool("grounded", controller.collisions.below);

        if (velocity.x > 0 && !m_FacingRight)
            Flip();
        else if (velocity.x < 0 && m_FacingRight)
            Flip();
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
    #endregion

    [System.Serializable]
    public class ClimbInfo
    {
        public bool empty = true;
        public Vector3 upPoint;

        public Vector3 downPoint;
    }
}
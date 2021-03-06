﻿using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(PlayerCrouch))]
public class Player : MonoBehaviour
{
    public enum MovementMode { OnGround, OnAir, OnClimbing };
    [HideInInspector]
    public MovementMode PlayerMode = MovementMode.OnAir;
    
    private Vector3 velocity;
    private Vector3 newVelocity;
    float velocityXSmoothing;
    float velocityYSmoothing;

    private bool overrrideVelocity = false;

    Vector2 directionalInput;

    private bool Dead = false;
    private bool canMove = true;
    [HideInInspector]
    public bool crouch = false;

    [Header("Referencias")]
    public Controller2D controller;
    //public PlayerInput input;
    [SerializeField]
    Animator anim;
    public Runas runas;

    [SerializeField]
    public InputDevice input;

    [HideInInspector]
    public static Player Instance;

    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);

        input = InputManager.ActiveDevice;

        if (!controller)
            controller = GetComponent<Controller2D>();

        PlayerClimbInfo.empty = true;

        gravityOnGround = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravityOnGround) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravityOnGround) * minJumpHeight);
        secondJumpSpeed = Mathf.Sqrt(2 * Mathf.Abs(gravityOnGround) * secondJumpHeight);

        InputManager.OnActiveDeviceChanged += inputDevice => input = inputDevice;
    }
    
    void Update()
    {
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

            if (input.LeftBumper.WasPressed)
                runas.StartMagia(input.Name == "Keyboard/Mouse", transform.position);
        }

        UpdateAnimations();

        //if (overrrideVelocity)
        //{
        //    overrrideVelocity = false;
        //    Debug.Log(newVelocity);
        //    controller.Move(newVelocity, crouch && input.Action1.WasPressed);
        //}
        //else

        if (overrrideVelocity)
        {
            velocity = newVelocity;
            overrrideVelocity = false;
            SetMovementAir();
        }

        controller.Move(velocity * Time.deltaTime, crouch && input.Action1.WasPressed);
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

        //Horizontal Movement
        float targetVelocityX = input.Direction.X * ((crouch) ? crouchedSpeed : groundSpeed);
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

        if ((input.Action1.WasPressed) && controller.collisions.below && !crouch)
        {
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (input.Direction.Raw.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    SetMovementAir();
                    speedToUse.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                    speedToUse.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                }
            }
            else if (!input.Direction.Down)
            {
                SetMovementAir();
                speedToUse.y = maxJumpVelocity;
            }
        }

        speedToUse.y += (gravityOnGround * Time.deltaTime);
        //speedToUse.y += velocity.y + (gravityOnGround * Time.deltaTime);

        if (!controller.collisions.below)
            SetMovementAir();

        if (input.Direction.Down && !PlayerClimbInfo.empty && !crouch)
            SetMovementClimb();

        velocity = speedToUse;
    }

    private void SetMovementGround()
    {
        PlayerMode = MovementMode.OnGround;
        //Debug.Log(PlayerMode);
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
        float targetVelocityX = input.Direction.X * ((planeando) ? xGlideSpeed : airSpeed);
        float smoothTime = (planeando) ? xTimeGlide : airTime;
        speedToUse.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, smoothTime);

        //Vertical Movement
        float gravityToUse = velocity.y < 0 ? gravityOnAir : gravityOnGround;
        speedToUse.y = velocity.y + gravityToUse * Time.deltaTime;

        if (input.Action1.WasReleased && velocity.y > minJumpVelocity)
            speedToUse.y = minJumpVelocity;

        if (tieneParaguas)
        {
            if ((input.Action1.IsPressed) && velocity.y < 0)
            {
                speedToUse.y = Mathf.SmoothDamp(velocity.y, yGlideSpeed, ref velocityYSmoothing, yGlideTime);
                planeando = true;
                //anim.SetBool("planeando", planeo);
            }
            else if (planeando && (input.Action1.WasReleased))
                Invoke("ResetPlaneo", timeToSecondJump);

            if ((input.Action1.WasPressed) && canSecondJump && planeando)
            {
                speedToUse.y = secondJumpSpeed;
                canSecondJump = false;
            }
        }

        if (controller.collisions.above)
            speedToUse.y = 0;

        if (controller.collisions.below && !overrrideVelocity)
            SetMovementGround();

        if (input.Direction.Raw.y != 0/*Mathf.Abs(input.Vertical()) > 0.5f*/ && !PlayerClimbInfo.empty)
            SetMovementClimb();

        velocity = speedToUse;
    }

    public void SetMovementAir()
    {
        PlayerMode = MovementMode.OnAir;
        canSecondJump = true;
        //input.Crouch(true);
        //Debug.Log(PlayerMode);
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
        if (input.Action1.WasPressed)
        {
            SetMovementAir();
            velocity.y = minJumpVelocity;
        }
        else if (input.Direction.Raw.x != 0/*Mathf.Abs(input.Vertical()) > 0.5f*/ || PlayerClimbInfo.empty)
        {
            SetMovementGround();
        }

        else
        {
            velocity.y = input.Direction.Y * climbSpeed;
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
        //anim.SetBool("crouch", crouch);
        //anim.SetFloat("velocityX", velocity.x);
        //anim.SetBool("golpePurificante", input.Action2.WasPressed);
        //anim.SetBool("planeando", planeando);
        //anim.SetBool("grounded", controller.collisions.below);

        anim.SetBool("Move", Mathf.Abs(velocity.x) > 0.1f);
        anim.SetFloat("Speed", Mathf.Lerp(0.5f, 1, Mathf.Abs(velocity.x) / groundSpeed));

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

    /// <summary>
    /// Sobreescribe la velocidad del player.
    /// </summary>
    /// <param name="x"> Variable x de la velocidad, si es 0 se mantiene la anterior del personaje</param>
    /// <param name="y"> Variable y de la velocidad, si es 0 se mantiene la anterior del personaje</param>
    public void addSpeed(float x = 0, float y = 0)
    {
        newVelocity.x = (x == 0) ? velocity.x : x;

        newVelocity.y = (y == 0) ? velocity.y : y;

        overrrideVelocity = true;
    }

    [System.Serializable]
    public class ClimbInfo
    {
        public bool empty = true;
        public Vector3 upPoint;

        public Vector3 downPoint;
    }
}
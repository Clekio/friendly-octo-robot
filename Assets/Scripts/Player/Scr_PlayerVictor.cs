using UnityEngine;

[RequireComponent(typeof(Scr_Controller2DVictor))]
[RequireComponent(typeof(Scr_PlayerInputVictor))]
public class Scr_PlayerVictor : MonoBehaviour {

    [Header("Configuración")]
    public float MoveSpeed = 6.0f;
    [Range(0, 1)]
    public float CrouchSpeed = .25f;                  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    public float MaxJumpHeight = 4.0f;
    public float MinJumpHeight = 1.0f;
	public float MaxSetaImpulse;
    [Tooltip("Tiempo en alcanzar la altura maxima del salto")]
    public float TimeToJumpApex = .4f;
    [Tooltip("tiempo que tardara en caer al suelo cuando ")]
    public float GlideTime = 1;
    private float m_accelerationTimeAirborne = .4f;
    private float m_accelerationTimeGrounded = .1f;

    private float m_oldTiempoEnAire;

    private float m_mainGravity;            
    private float m_glideGravity;           // Valor de la gravedad cunando estas callendo con el paraguas
    private float m_maxJumpVelocity;
    private float m_minJumpVelocity;
    public static Vector3 Velocity;
    private float m_velocityXSmoothing;
    private Transform m_ceilingCheck;       // A position marking where to check for ceilings.
    const float k_CeilingRadius = .01f;     // Radius of the overlap circle to determine if the player can stand up.

    private Animator m_anim;

    private Scr_Controller2DVictor m_controller;

    [HideInInspector]
    public Vector2 DirectionalInput;
    [HideInInspector]
    public bool CrouchInputValue;


	public GameObject viento;

	void OnMouseDown(){
		
		//Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

		Debug.Log ("pene");

		Instantiate (viento, new Vector2(Input.mousePosition.x, Input.mousePosition.y), Quaternion.identity) ;

	}

    void Start()
    {
        m_controller = GetComponent<Scr_Controller2DVictor>();

        m_anim = GetComponent<Animator>();

        m_mainGravity = -(2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        m_glideGravity = -(6 * MaxJumpHeight) / Mathf.Pow(GlideTime, 8);
        m_maxJumpVelocity = Mathf.Abs(m_mainGravity) * TimeToJumpApex;
        m_minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(m_mainGravity) * MinJumpHeight);
    }

    private void OnEnable()
    {
        scr_Respawn.IsDeath = false;
        //anim.SetBool("isDeath", isDeath);
    }



    void Update()
    {
        CalculateVelocity();

        setAnimation();

        m_controller.Move(Velocity * Time.deltaTime, DirectionalInput);

        if (m_controller.Collisions.above || m_controller.Collisions.below)
        {
            if (m_controller.Collisions.slidingDownMaxSlope)
            {
                Velocity.y += m_controller.Collisions.slopeNormal.y * -m_mainGravity * Time.deltaTime;
            }
            else
            {
                Velocity.y = 0;
            }
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        DirectionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (m_controller.Collisions.below && !m_controller.Collisions.slidingDownMaxSlope)
        {
            Velocity.y = m_maxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        if (Velocity.y > m_minJumpVelocity)
        {
            Velocity.y = m_minJumpVelocity;
        }
    }

    float Gravity()
    {
        if (Input.GetKey(KeyCode.Space) && Velocity.y < 0)
        {
            return m_glideGravity;
        }
        else return m_mainGravity;
    }

    void CalculateVelocity()
    {
        //StandUp();
        float targetVelocityX = DirectionalInput.x * MoveSpeed;

        if (CrouchInputValue && m_controller.Collisions.below)
            Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX* CrouchSpeed, ref m_velocityXSmoothing, m_accelerationTimeGrounded);
        else if (m_controller.Collisions.below)
            Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref m_velocityXSmoothing, m_accelerationTimeGrounded);
        else if (Input.GetKey(KeyCode.Space) && Velocity.y < 0)
            Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref m_velocityXSmoothing, m_accelerationTimeGrounded);
        else if (!m_controller.Collisions.below)
            Velocity.x = Mathf.SmoothDamp(Velocity.x, targetVelocityX, ref m_velocityXSmoothing, m_accelerationTimeAirborne);

        Velocity.y += Gravity() * Time.deltaTime;
    }

    private void StandUp()
    {
        // If crouching, check to see if the character can stand up
        if (!CrouchInputValue && m_anim.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_ceilingCheck.position, k_CeilingRadius, m_controller.CapaColision))
            {
                CrouchInputValue = true;
            }
        }

        // Set the crouch animation.
        m_anim.SetBool("Crouch", CrouchInputValue);
    }

    void setAnimation()
    {
        m_anim.SetBool("landed", m_controller.Collisions.below);
        m_anim.SetFloat("hSpeed", Mathf.Abs(Velocity.x / MoveSpeed));
        m_anim.SetFloat("vSpeed", Mathf.Abs(Velocity.y / MoveSpeed));
    }

	public void OnSetaOn()
	{
		if (Velocity.y<MaxSetaImpulse)
		{
			Velocity.y = MaxSetaImpulse;
		} else {
			OnSetaOff();
		}
	}

	public void OnSetaOff()
	{
		if (Velocity.y > m_minJumpVelocity)
		{
			Velocity.y = m_minJumpVelocity;
		}
	}
}

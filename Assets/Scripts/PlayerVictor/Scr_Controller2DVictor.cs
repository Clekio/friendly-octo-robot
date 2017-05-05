using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Scr_Controller2DVictor : MonoBehaviour
{

    [Header("Configuración")]

    [Tooltip("La capa donde actuará la colisión del jugador.")]
    public LayerMask CapaColision;

    private const float m_skinWidth = .015f;
    private int m_horizontalRayCount = 4;
    private int m_verticalRayCount = 4;

    [Tooltip("El ángulo máximo de un elemento del escenario que podrá subir el jugador.")]
    public float AnguloMaxSubida = 80;

    private float m_horizontalRaySpacing;
    private float m_verticalRaySpacing;

    private BoxCollider2D m_collider;
    private RaycastOrigins m_raycastOrigins;
    public CollisionInfo Collisions;
    [HideInInspector]
    public Vector2 PlayerInput;

    public virtual void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
        Collisions.faceDir = 1;
    }

    public void Move(Vector2 velocity, Vector2 input)
    {
        UpdateRaycastOrigins();
        Collisions.Reset();
        Collisions.velocityOld = velocity;
        PlayerInput = input;

        if (velocity.y < 0)
        {
            DescendSlope(ref velocity);
        }
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + m_skinWidth;

        for (int i = 0; i < m_horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? m_raycastOrigins.bottomLeft : m_raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (m_horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CapaColision);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

                if (i == 0 && slopeAngle <= AnguloMaxSubida)
                {
                    if (Collisions.descendingSlope)
                    {
                        Collisions.descendingSlope = false;
                        velocity = Collisions.velocityOld;
                    }
                    float distanceToSlopeStart = 0;
                    if (slopeAngle != Collisions.slopeAngleOld)
                    {
                        distanceToSlopeStart = hit.distance - m_skinWidth;
                        velocity.x -= distanceToSlopeStart * directionX;
                    }
                    ClimbSlope(ref velocity, slopeAngle, hit.normal);
                    velocity.x += distanceToSlopeStart * directionX;
                }

                if (!Collisions.climbingSlope || slopeAngle > AnguloMaxSubida)
                {
                    velocity.x = (hit.distance - m_skinWidth) * directionX;
                    rayLength = hit.distance;

                    if (Collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(Collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }

                    Collisions.left = directionX == -1;
                    Collisions.right = directionX == 1;
                }
            }
        }
    }

    void VerticalCollisions(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + m_skinWidth;

        for (int i = 0; i < m_verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? m_raycastOrigins.bottomLeft : m_raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (m_verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, CapaColision);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                if (hit.collider.tag == "Through")
                {
                    if (directionY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                    if (Collisions.fallingThroughPlatform)
                    {
                        continue;
                    }
                    if (PlayerInput.y == -1)
                    {
                        Collisions.fallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", .5f);
                        continue;
                    }
                }

                velocity.y = (hit.distance - m_skinWidth) * directionY;
                rayLength = hit.distance;

                if (Collisions.climbingSlope)
                {
                    velocity.x = velocity.y / Mathf.Tan(Collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                }

                Collisions.below = directionY == -1;
                Collisions.above = directionY == 1;
            }
        }

        if (Collisions.climbingSlope)
        {
            float directionX = Mathf.Sign(velocity.x);
            rayLength = Mathf.Abs(velocity.x) + m_skinWidth;
            Vector2 rayOrigin = ((directionX == -1) ? m_raycastOrigins.bottomLeft : m_raycastOrigins.bottomRight) + Vector2.up * velocity.y;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CapaColision);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != Collisions.slopeAngle)
                {
                    velocity.x = (hit.distance - m_skinWidth) * directionX;
                    Collisions.slopeAngle = slopeAngle;
                }
            }
        }
    }

    void ClimbSlope(ref Vector2 velocity, float slopeAngle, Vector2 slopeNormal)
    {
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            Collisions.below = true;
            Collisions.climbingSlope = true;
            Collisions.slopeAngle = slopeAngle;
            Collisions.slopeNormal = slopeNormal;
        }
    }

    void DescendSlope(ref Vector2 moveAmount)
    {

        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(m_raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + m_skinWidth, CapaColision);
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(m_raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + m_skinWidth, CapaColision);
        if (maxSlopeHitLeft ^ maxSlopeHitRight)
        {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount);
            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount);
        }

        if (!Collisions.slidingDownMaxSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x);
            Vector2 rayOrigin = (directionX == -1) ? m_raycastOrigins.bottomRight : m_raycastOrigins.bottomLeft;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, CapaColision);

            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if (slopeAngle != 0 && slopeAngle <= AnguloMaxSubida)
                {
                    if (Mathf.Sign(hit.normal.x) == directionX)
                    {
                        if (hit.distance - m_skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x))
                        {
                            float moveDistance = Mathf.Abs(moveAmount.x);
                            float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x);
                            moveAmount.y -= descendmoveAmountY;

                            Collisions.slopeAngle = slopeAngle;
                            Collisions.descendingSlope = true;
                            Collisions.below = true;
                            Collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }
        }
    }

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
    {

        if (hit)
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            if (slopeAngle > AnguloMaxSubida)
            {
                moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);

                Collisions.slopeAngle = slopeAngle;
                Collisions.slidingDownMaxSlope = true;
                Collisions.slopeNormal = hit.normal;
            }
        }

    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = m_collider.bounds;
        bounds.Expand(m_skinWidth * -2);

        m_raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        m_raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        m_raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        m_raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = m_collider.bounds;
        bounds.Expand(m_skinWidth * -2);

        m_horizontalRayCount = Mathf.Clamp(m_horizontalRayCount, 2, int.MaxValue);
        m_verticalRayCount = Mathf.Clamp(m_verticalRayCount, 2, int.MaxValue);

        m_horizontalRaySpacing = bounds.size.y / (m_horizontalRayCount - 1);
        m_verticalRaySpacing = bounds.size.x / (m_verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    void ResetFallingThroughPlatform()
    {
        Collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public float slopeAngle, slopeAngleOld;
        public Vector2 slopeNormal;
        public Vector2 velocityOld;
        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset()
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
}

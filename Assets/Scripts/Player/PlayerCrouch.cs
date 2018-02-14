using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    private Rect _standCollider;

    /// <value> StandCollider property gets/sets the value of the rect field, _standCollider</value>
    public Rect StandCollider
    {
        get
        {
            Rect r = _standCollider;

            r.position = transform.TransformPoint(r.position);
            //r.size = Vector2.Scale(_grabCollider.size, transform.localScale);
            r.size = new Vector2(Mathf.Abs(r.size.x * transform.localScale.x), Mathf.Abs(r.size.y * transform.localScale.y));

            return r;
        }

        set { _standCollider = value; }
    }
    
    public LayerMask collisionMask;

    [Space]
    [SerializeField]
    private BoxCollider2D playerColldier;

    [SerializeField]
    private AnimationCurve SizeInterpolation;

    private float currentCrouchTime;
    [SerializeField]
    private float crouchTime;
    private bool preFrameCoruch = false;
    private bool setRay = false;
    
    private Vector2 standSize;
    [SerializeField]
    private Vector2 crouchSize;

    private Vector2 startSize;
    private Vector2 endSize;
    
    private Vector2 standOffset;
    private Vector2 crouchOffset;

    private Vector2 startOffset;
    private Vector2 endOffset;
    
    void Start()
    {
        currentCrouchTime = crouchTime;

        endSize = playerColldier.size;
        endOffset = playerColldier.offset;

        standSize = playerColldier.size;
        standOffset = playerColldier.offset;

        crouchOffset.x = standOffset.x;
        crouchOffset.y = crouchSize.y / 2;
    }

    bool crouch = false;
    void Update()
    {
        CanCrouch(ref crouch);
        if (!preFrameCoruch && crouch)
        {
            startSize = playerColldier.size;
            endSize = crouchSize;

            startOffset = playerColldier.offset;
            endOffset = crouchOffset;

            currentCrouchTime = 0;

            setRay = true;
        }
        else if (preFrameCoruch && !crouch)
        {
            startSize = playerColldier.size;
            endSize = standSize;

            startOffset = playerColldier.offset;
            endOffset = standOffset;

            currentCrouchTime = 0;

            setRay = true;
        }

        currentCrouchTime = Mathf.Clamp(currentCrouchTime + Time.deltaTime, 0, crouchTime);

        //lerp
        float perc = currentCrouchTime / crouchTime;
        perc = SizeInterpolation.Evaluate(perc);

        playerColldier.size = Vector2.Lerp(startSize, endSize, perc);
        playerColldier.offset = Vector2.Lerp(startOffset, endOffset, perc);

        preFrameCoruch = crouch;

        if (setRay && perc == 1)
        {
            player.controller.CalculateRaySpacing();
            setRay = false;
        }
    }

    private void CanCrouch(ref bool c)
    {
        if (player.PlayerMode == Player.MovementMode.OnAir)
            c = false;
        else if (player.input.Direction.Down)//player.input.Direction.Y < -0.7f)
            c = true;
        else if (c)
        {
            Collider2D coll = Physics2D.OverlapBox(StandCollider.position, StandCollider.size, 0, collisionMask);
            if (!coll || coll.CompareTag("Through"))
                c = false;
        }

        player.crouch = c;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Colors.LawnGreen;
        //Gizmos.DrawWireCube(transform.TransformPoint(standUp.position), Vector3.Scale(standUp.size, transform.localScale));//standUp.size);
        Gizmos.DrawWireCube(StandCollider.position, StandCollider.size);
        Gizmos.color = Colors.Green;
        Gizmos.DrawWireCube(transform.TransformPoint(new Vector2(0, crouchSize.y / 2)), Vector3.Scale(crouchSize, transform.localScale));
    }
}
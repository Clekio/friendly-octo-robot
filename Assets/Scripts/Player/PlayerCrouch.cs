using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    private Rect standUp;
    [SerializeField]
    private LayerMask Ground;

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

    [SerializeField]
    private Vector2 standSize;
    [SerializeField]
    private Vector2 crouchSize;
    private Vector2 startSize;
    private Vector2 endSize;

    [SerializeField]
    private Vector2 standOffset;
    [SerializeField]
    private Vector2 crouchOffset;
    private Vector2 startOffset;
    private Vector2 endOffset;
    
    void Start()
    {
        currentCrouchTime = crouchTime;

        endSize = playerColldier.size;
        endOffset = playerColldier.offset;
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
        else if (player.input.Direction.Down)
            c = true;
        else if (c)
        {
            Collider2D coll = Physics2D.OverlapBox((Vector3)standUp.position + transform.position, standUp.size / 2, Ground);
            if (coll == null || coll.CompareTag("Through"))
                c = false;
        }

        player.crouch = c;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Colors.LawnGreen;
        Gizmos.DrawWireCube((Vector3)standUp.position + transform.position, standUp.size);
    }
}
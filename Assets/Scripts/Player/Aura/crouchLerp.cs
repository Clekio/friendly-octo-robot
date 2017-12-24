using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XboxCtrlrInput;

public class crouchLerp : MonoBehaviour
{
    [SerializeField]
    Player playerRef;

    [SerializeField]
    private BoxCollider2D playerColldier;

    [SerializeField]
    private AnimationCurve SizeInterpolation;

    private float currentCrouchTime;
    [SerializeField]
    private float crouchTime;
    private bool preFrameCoruch = false;

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

    // Use this for initialization
    void Start ()
    {
        currentCrouchTime = crouchTime;

        //standSize = playerColldier.size;
        //crouchSize = standSize / 2;

        //standOffset = playerColldier.offset;
        //crouchOffset = standOffset / 2;

        endSize = playerColldier.size;
        endOffset = playerColldier.offset;
    }

    void Update ()
    {
        if (!preFrameCoruch && playerRef.crouch)//(Input.GetKeyDown(KeyCode.S) || XCI.GetButtonDown(XboxButton.LeftBumper)) && playerRef.canMove)
        {
            startSize = playerColldier.size;
            endSize = crouchSize;

            startOffset = playerColldier.offset;
            endOffset = crouchOffset;

            currentCrouchTime = 0;
        }
        else if (preFrameCoruch && !playerRef.crouch) //(Input.GetKeyUp(KeyCode.S) || XCI.GetButtonUp(XboxButton.LeftBumper)) && playerRef.canMove)
        {
            startSize = playerColldier.size;
            endSize = standSize;

            startOffset = playerColldier.offset;
            endOffset = standOffset;

            currentCrouchTime = 0;
        }

        currentCrouchTime = Mathf.Clamp(currentCrouchTime + Time.deltaTime, 0, crouchTime);

        //lerp
        float perc = currentCrouchTime / crouchTime;
        perc = SizeInterpolation.Evaluate(perc);

        playerColldier.size = Vector2.Lerp(startSize, endSize, perc);
        playerColldier.offset = Vector2.Lerp(startOffset, endOffset, perc);

        preFrameCoruch = playerRef.crouch;
    }
}

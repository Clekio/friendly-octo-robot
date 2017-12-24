using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAura : MonoBehaviour
{
    [SerializeField]
    private GameControllerInputs controllerInput;

    public float Horizontal()
    {
        return (controllerInput.LeftDirectional_Horizontal < Input.GetAxisRaw("Horizontal")) ? Input.GetAxisRaw("Horizontal") : controllerInput.LeftDirectional_Horizontal;
    }

    public float Vertical()
    {
        return (controllerInput.LeftDirectional_Vertical < Mathf.Abs(Input.GetAxisRaw("Vertical"))) ? Input.GetAxisRaw("Vertical") : controllerInput.LeftDirectional_Vertical;
    }

    public bool Crouch()
    {
        //return (Input.GetAxisRaw("Vertical") < -0.5f || controllerInput.LeftDirectional_Vertical < -0.7f);
        if (controllerInput.DPad_Down || controllerInput.LeftDirectional_asDownButton || Input.GetAxisRaw("Vertical") < -0.5f)
            return true;
        else
            return false;
    }

    public bool JumpDown()
    {
        return (Input.GetButtonDown("Jump") || controllerInput.A_button_down);
    }

    public bool JumpHold()
    {
        return (Input.GetButton("Jump") || controllerInput.A_button_hold);
    }

    public bool JumpUp()
    {
        return (Input.GetButtonUp("Jump") || controllerInput.A_button_up);
    }

    public bool Magia()
    {
        return (Input.GetKey(KeyCode.L) || controllerInput.X_button_hold);
    }

    public bool GolpePurificante()
    {
        return Input.GetMouseButtonDown(1) || controllerInput.B_button_hold;
    }
}

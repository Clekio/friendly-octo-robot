using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputAura : MonoBehaviour
{
    //[SerializeField][Range(-1,0)]
    //private float crouchInput = -0.5f;

    private InputDevice inputDevice;

    private void Awake()
    {
        inputDevice = InputManager.ActiveDevice;
    }

    public float Horizontal()
    {
        //return (controllerInput.LeftDirectional_Horizontal < Input.GetAxisRaw("Horizontal")) ? Input.GetAxisRaw("Horizontal") : controllerInput.LeftDirectional_Horizontal;
        return inputDevice.Direction.X;
    }

    public float Vertical()
    {
        //return (controllerInput.LeftDirectional_Vertical < Mathf.Abs(Input.GetAxisRaw("Vertical"))) ? Input.GetAxisRaw("Vertical") : controllerInput.LeftDirectional_Vertical;
        return inputDevice.Direction.Y;
    }

    public bool Crouch()
    {
        //return (Input.GetAxisRaw("Vertical") < -0.5f || controllerInput.LeftDirectional_Vertical < -0.7f);
        //if (controllerInput.DPad_Down || controllerInput.LeftDirectional_asDownButton || Input.GetAxisRaw("Vertical") < -0.5f)
        //    return true;
        //else
        //    return false;
        return (inputDevice.Direction.Y < -0.5f);
    }

    public bool JumpDown()
    {
        //return (Input.GetButtonDown("Jump") || controllerInput.A_button_down);
        return inputDevice.Action1.WasPressed;
    }

    public bool JumpHold()
    {
        //return (Input.GetButton("Jump") || controllerInput.A_button_hold);
        return inputDevice.Action1.IsPressed;
    }

    public bool JumpUp()
    {
        //return (Input.GetButtonUp("Jump") || controllerInput.A_button_up);
        return inputDevice.Action1.WasReleased;
    }

    public bool Magia()
    {
        //return (Input.GetKey(KeyCode.L) || controllerInput.X_button_hold);
        return inputDevice.LeftBumper;
    }

    public bool GolpePurificante()
    {
        //return Input.GetMouseButtonDown(1) || controllerInput.B_button_hold;
        return inputDevice.Action2;
    }
}

using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Rect standUp;
    [SerializeField]
    private LayerMask Ground;

    private InputDevice inputDevice;

    private void Awake()
    {
        inputDevice = InputManager.ActiveDevice;
    }

    public Vector2 Direction()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public float Horizontal()
    {
        return inputDevice.Direction.X;
    }

    public float Vertical()
    {
        return inputDevice.Direction.Y;
    }

    bool c = false;
    public bool Crouch()
    {
        if (Input.GetKey(KeyCode.Q))
            c = true;
        else if (c)
        {
            Collider2D coll = Physics2D.OverlapBox((Vector3)standUp.position + transform.position, standUp.size/2, Ground);
            if (coll == null)
                c = false;
        }
        
        return c;
    }

    public bool JumpDown()
    {
        return inputDevice.Action1.WasPressed;
    }

    public bool JumpHold()
    {
        return inputDevice.Action1.IsPressed;
    }

    public bool JumpUp()
    {
        return inputDevice.Action1.WasReleased;
    }

    public bool Magia()
    {
        return inputDevice.LeftBumper;
    }

    public bool GolpePurificante()
    {
        return inputDevice.Action2;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Colors.LawnGreen;
        Gizmos.DrawWireCube((Vector3)standUp.position + transform.position, standUp.size);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterActions : MonoBehaviour
{
    public Player p;

    public bool JumpWasPress()
    {
        return p.input.Action1.WasPressed || p.input.Direction.Y > 0.7f;
    }

    public bool JumpIsPressed()
    {
        return p.input.Action1.IsPressed || p.input.Direction.Y > 0.7f;
    }

    public bool JumpWasReleased()
    {
        return p.input.Action1.WasReleased || p.input.Direction.Y > 0.7f;
    }
}

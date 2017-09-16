using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CheckpointManager : MonoBehaviour
{
    Vector3 lastCheckpoint;

	void Update ()
    {
        lastCheckpoint = Scr_PlayerCheckpoint.lastCheckpoint;
	}
}
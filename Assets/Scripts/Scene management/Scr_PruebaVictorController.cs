using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PruebaVictorController : MonoBehaviour
{
    GameObject player;

	void Start ()
    {
        player = GameObject.Find("Aura");

        player.transform.position = new Vector3(-16, 197, 0);
	}
}
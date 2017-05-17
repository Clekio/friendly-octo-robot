using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaFlotante : MonoBehaviour
{
    public bool move;
	
	void Update () {
        move = Scr_MenhirPlatsFlotantes.activarPlataformas;
    }
}
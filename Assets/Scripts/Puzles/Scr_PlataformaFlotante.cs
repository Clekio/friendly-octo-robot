using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlataformaFlotante : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update () {
        bool move = Scr_MenhirPlatsFlotantes.activarPlataformas;
        anim.SetBool("Move", move);
    }
}
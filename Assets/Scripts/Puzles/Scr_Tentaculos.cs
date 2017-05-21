using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tentaculos : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    bool tentaculos;
    bool purificacion;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        tentaculos = Scr_Menhir.activarTentaculos;
        purificacion = Scr_TriggerTentaculos.purificacionPosible;
        anim.SetBool("Tentáculos", tentaculos);
        anim.SetBool("Purificación", purificacion);
    }
}
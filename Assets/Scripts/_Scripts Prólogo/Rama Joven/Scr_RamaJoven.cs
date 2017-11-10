using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaJoven : MonoBehaviour
{
    [SerializeField]
    int impulso;      // Fuerza con la que se impulsa al jugador

    Animator anim;
    GameObject player;

    int estado;

    private void Start()
    {
        player = GameObject.Find("Aura");
        
        anim = gameObject.GetComponentInParent<Animator>();
    }

    private void Update()
    {
        estado = gameObject.GetComponentInChildren<Scr_DetectorRamaJoven>().estado;

        //anim.SetInteger("Estado", estado);

        if (estado == 3)
        {
            Lanzamiento();
        }
    }

    void Lanzamiento()
    {
        Rigidbody2D rb2d = player.gameObject.GetComponent<Rigidbody2D>();

        if (!rb2d.isKinematic)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, impulso);

            //anim.SetInteger("Estado", 3);
        }
    }
}
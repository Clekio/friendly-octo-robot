using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RamaJoven : MonoBehaviour
{
    [SerializeField]
    int impulso = 35;      // Fuerza con la que se impulsa al jugador

    public Animator anim;

    [SerializeField]
    private Rect trigger;

    int estado;

    private void Start()
    {
    }

    private void Update()
    {
        estado = gameObject.GetComponentInChildren<Scr_DetectorRamaJoven>().estado;

        anim.SetInteger("Estado", estado);

        if (estado == 3)
        {
            Player.Instance.addSpeed(0, impulso);
            //Lanzamiento();
        }
    }

    void Lanzamiento()
    {
        
        //Rigidbody2D rb2d = player.gameObject.GetComponent<Rigidbody2D>();

        //if (!rb2d.isKinematic)
        //{
        //    rb2d.velocity = new Vector2(rb2d.velocity.x, impulso);

        //    //anim.SetInteger("Estado", 3);
        //}
    }
}
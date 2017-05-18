using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionAbajo : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public bool colisionAbajo = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       // anim.SetBool("ColisionAbajo", colisionAbajo);

        if (colisionAbajo == true)
        {
            colisionAbajo = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            colisionAbajo = true;
        }
    }
}
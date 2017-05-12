using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_thunderEffects : MonoBehaviour {

    public bool Enemigo;
    public bool Estalactita;

    private Rigidbody2D m_rb;
    private Renderer rend;
    // Update is called once per frame
    void Update () {
        m_rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
    }

    public void Accion()
    {
        if (Enemigo)
        {
            Debug.Log("0");
            StartCoroutine(MyCoroutine());
        }
        else if (Estalactita)
        {
            m_rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    IEnumerator MyCoroutine()
    {
        //This is a coroutine
        Debug.Log("1");
        rend.material.color = Color.yellow;
		gameObject.GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(3);
		gameObject.GetComponent <Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;//Wait one frame

        Debug.Log("2");
        rend.material.color = Color.white;
    }
}

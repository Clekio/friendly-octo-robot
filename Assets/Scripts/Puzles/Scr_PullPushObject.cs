using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PullPushObject : MonoBehaviour // FALTA HACER QUE CUANDO SE ESTÁ ARRASTRANDO UN OBJETO, EL JUGADOR NO SE PUEDA AGACHAR NI PUEDA SALTAR
{
    [SerializeField]
    GameObject player;

    bool playerCollision = false;     // Es true cuando el player está en contacto con el objeto a mover
    float xPos;                       // Posición en X del objeto a mover
    bool move = false;                // Es true cuando el objeto se puede mover

    private void Start()
    {
        xPos = transform.position.x;
    }

    void Update()
    {
        /*if (playerCollision == true && move == false)
        {
            transform.position = new Vector3(xPos, transform.position.y);
        }*/

        if (playerCollision == true && Input.GetKeyDown(KeyCode.E))
        {
            transform.GetComponent<FixedJoint2D>().enabled = true;
            transform.GetComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();

            move = true;
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            transform.GetComponent<FixedJoint2D>().enabled = false;

            xPos = transform.position.x;
            move = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = false;
        }
    }
}
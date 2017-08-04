using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTrigger : MonoBehaviour {

    // VERSION 1: SE TIRA Y SE EMPUJA CON LA E Y NO SE MUEVE AL CHOCAR

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject feedback;

    bool playerCollision = false;     // Es true cuando el player está en contacto con el objeto a mover
    float xPos;                       // Posición en X del objeto a mover
    bool move = false;                // Es true cuando el objeto se puede mover

    private void Start()
    {
        xPos = transform.position.x;
    }

    void Update()
    {
        if (playerCollision == true && !transform.GetComponent<FixedJoint2D>().enabled)
        {
            feedback.SetActive(true);
        }
        else
        {
            feedback.SetActive(false);
        }

        if (playerCollision == true && Input.GetKey(KeyCode.E))
        {
            transform.GetComponent<FixedJoint2D>().enabled = true;
            transform.GetComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }

        else if (transform.GetComponent<FixedJoint2D>().enabled && !Input.GetKey(KeyCode.E))
        {
            transform.GetComponent<FixedJoint2D>().enabled = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCollision = false;
        }
    }
}

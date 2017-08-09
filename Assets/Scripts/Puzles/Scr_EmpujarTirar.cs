using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_EmpujarTirar : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject helpText;

    bool playerInRange = false;

    public static bool canJump = true;

    private void Update()
    {
        if (playerInRange == true && !transform.GetComponent<FixedJoint2D>().enabled)
        {
            helpText.SetActive(true);
        }
        else
        {
            helpText.SetActive(false);
        }

        if (playerInRange == true && Input.GetKey(KeyCode.LeftControl))
        {
            transform.GetComponent<FixedJoint2D>().enabled = true;
            transform.GetComponent<FixedJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();

            canJump = false;
        }

        else if (transform.GetComponent<FixedJoint2D>().enabled && !Input.GetKey(KeyCode.LeftControl))
        {
            transform.GetComponent<FixedJoint2D>().enabled = false;

            canJump = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
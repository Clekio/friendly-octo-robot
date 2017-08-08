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
    float xPos;

    public static bool canJump = true;

    private void Start()
    {
        xPos = transform.position.x;
    }

    private void Update()
    {
        if (playerInRange == true && !transform.GetComponent<FixedJoint2D>().enabled)
        {
            helpText.SetActive(true);

            //transform.position = new Vector3(xPos, transform.position.y);
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

            xPos = transform.position.x;

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
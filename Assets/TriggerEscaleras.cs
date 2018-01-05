using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEscaleras : MonoBehaviour
{
    [SerializeField]
    private BuildEscalera escalera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();

        if (p)
        {
            p.PlayerClimbInfo.downPoint = transform.position;
            p.PlayerClimbInfo.upPoint = escalera.targetPosition;
            p.PlayerClimbInfo.empty = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();

        if (p)
            p.PlayerClimbInfo.empty = true;
    }
}

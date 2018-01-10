using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveCaja : MonoBehaviour
{
    public LayerMask collisionMask;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private Rect grabColl;

    private Vector3 center;

    [Header("Referencias")]
    [SerializeField]
    private Player player;
    [SerializeField]
    private Controller2D controller;
    
    void Update ()
    {
        if (player.input.Action4.WasPressed)
        {
            Collider2D hit = Physics2D.OverlapBox(transform.TransformPoint(grabColl.position), Vector3.Scale(grabColl.size, transform.localScale), 0, collisionMask);
            if (hit)
            {
                controller.box = hit.GetComponent<BoxController>();
            }
        }
        if (controller.box)
        {
            float _dis = maxDistance * Mathf.Abs(transform.localScale.x);
            Vector3 _pos = transform.position + Vector3.up;
            if (player.input.Action4.WasReleased || Mathf.Abs(Vector3.Distance(controller.box.transform.position, _pos)) > _dis)
            {
                Debug.Log("Player Input: " + player.input.Action4.WasReleased);
                Debug.Log("Distance: " + (Vector3.Distance(controller.box.transform.position, _pos) > _dis));
                Debug.Log("Max Distance: " + _dis);
                Debug.Log("obj Distance: " + Vector3.Distance(controller.box.transform.position, _pos));
                controller.box = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Colors.DarkCyan;
        Gizmos.DrawWireCube(transform.TransformPoint(grabColl.position), Vector3.Scale(grabColl.size, transform.localScale));
        Gizmos.color = Colors.Cyan;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, maxDistance * Mathf.Abs(transform.localScale.x));
    }
}

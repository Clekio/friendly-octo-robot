using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveCaja : MonoBehaviour
{
    public LayerMask collisionMask;

    [SerializeField]
    private float maxDistance;

    [SerializeField]
    private Rect _grabCollider;

    /// <value> GrabCollider property gets/sets the value of the rect field, _grabColl</value>
    public Rect GrabCollider
    {
        get
        {
            Rect r = _grabCollider;

            r.position = transform.TransformPoint(r.position);
            //r.size = Vector2.Scale(_grabCollider.size, transform.localScale);
            r.size = new Vector2 (Mathf.Abs(r.size.x * transform.localScale.x), Mathf.Abs(r.size.y * transform.localScale.y));

            return r;
        }

        set { _grabCollider = value; }
    }

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
            Collider2D hit = Physics2D.OverlapBox(GrabCollider.position, GrabCollider.size, 0, collisionMask);
            if (hit)
            {
                Debug.Log("Hay caja");
                controller.box = hit.GetComponent<BoxController>();
            }
        }
        if (controller.box)
        {
            float _dis = maxDistance * Mathf.Abs(transform.localScale.x);
            Vector3 _pos = transform.position + (Vector3.up / 2);
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
        //Gizmos.DrawWireCube(transform.TransformPoint(_grabCollider.position), Vector3.Scale(_grabCollider.size, transform.localScale));
        Gizmos.DrawWireCube(GrabCollider.position, GrabCollider.size);
        Gizmos.color = Colors.Cyan;
        Gizmos.DrawWireSphere(transform.position + (Vector3.up/2), maxDistance * Mathf.Abs(transform.localScale.x));
    }
}

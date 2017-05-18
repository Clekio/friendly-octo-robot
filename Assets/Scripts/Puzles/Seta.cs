using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Seta : MonoBehaviour {

    public float alturaDelSalto;

    private void OnTriggerEnter2D (Collider2D other)
    {
        Rigidbody2D otherRb2d = other.gameObject.GetComponent<Rigidbody2D>();
        if (otherRb2d)
            otherRb2d.velocity = new Vector2(0, Mathf.Sqrt(2 * Mathf.Abs(otherRb2d.gravityScale * Physics2D.gravity.y) * alturaDelSalto));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 jumpPosition = transform.position + (Vector3.up * alturaDelSalto);
        Gizmos.DrawLine(jumpPosition + (Vector3.left * .5f), jumpPosition + (Vector3.right * .5f));
    }
}

using UnityEngine;

public class Seta : MonoBehaviour
{
    public float FuerzaDelImpulso;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.addSpeed(0, FuerzaDelImpulso);
        }
    }
}

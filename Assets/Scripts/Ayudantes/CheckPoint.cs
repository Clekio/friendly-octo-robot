using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private Vector3 m_spawnPosition;

    private void Start()
    {
        m_spawnPosition = GetComponent<Transform>().position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLive>().RespawnPosition = transform.position;
            Debug.Log("Spawn position: " + other.gameObject.GetComponent<PlayerLive>().RespawnPosition);
        }
        
    }
}

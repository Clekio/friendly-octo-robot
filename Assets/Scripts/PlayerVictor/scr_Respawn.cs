using UnityEngine;

public class scr_Respawn : MonoBehaviour {

    public static Vector3 RespawnPosition;
    public static bool IsDeath = false;
    public float RespawnTime = 1.0f;
    public static float Lives = 3.0f;

    private static float m_respawnTimeLeft = 0.0f;
    private Transform m_playerTransform;

    private void Awake()
    {
        m_playerTransform = GetComponent<Scr_Controller2DVictor>().transform;
        RespawnPosition = m_playerTransform.position;
    }

    private void Update()
    {
        if (IsDeath)
            m_respawnTimeLeft += Time.deltaTime;
            if (m_respawnTimeLeft >= RespawnTime)
            {
                Scr_PlayerVictor.Velocity = Vector3.zero;
                m_playerTransform.position = RespawnPosition;
                IsDeath = false;
            }
    }

    public static void KillPlayer()
    {
        --Lives;
        IsDeath = true;
        m_respawnTimeLeft = 0;
    }

    private void revive()
    {
        
        
    }
}

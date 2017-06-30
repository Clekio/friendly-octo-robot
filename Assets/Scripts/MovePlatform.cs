using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovePlatform : MonoBehaviour {

    public Vector2[] LocalWaypoints;

    public float m_speed = 2f;
    private bool m_move = false;
    [Range(0, 2)]
    public float m_easeAmount = 1f;

    private GameObject m_playerReference;
    [HideInInspector]
    public Vector2[] m_globalWaypoints;
    private int m_fromWaypointIndex;
    private float m_percentBetweenWaypoints;

    private Vector3 Velocity;

    private void Start()
    {
        m_globalWaypoints = new Vector2[LocalWaypoints.Length];
        for (int i = 0; i < LocalWaypoints.Length; i++)
        {
            m_globalWaypoints[i] = LocalWaypoints[i] + (Vector2)transform.position;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        m_move = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.transform.Translate(Velocity, Space.World);
    }

    private void FixedUpdate()
    {
        if (m_move)
        {
            Velocity = CalculatePlatformMovement();

            //m_playerReference.transform.Translate(Velocity, Space.World);
            transform.Translate(Velocity, Space.World);
        }
    }

    float Ease(float x)
    {
        float intanteEase = m_easeAmount + 1;
        return Mathf.Pow(x, intanteEase) / (Mathf.Pow(x, intanteEase) + Mathf.Pow(1 - x, intanteEase));
    }

    Vector3 CalculatePlatformMovement()
    {

        m_fromWaypointIndex %= m_globalWaypoints.Length;
        int toWaypointIndex = (m_fromWaypointIndex + 1) % m_globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(m_globalWaypoints[m_fromWaypointIndex], m_globalWaypoints[toWaypointIndex]);
        m_percentBetweenWaypoints += Time.deltaTime * m_speed / distanceBetweenWaypoints;
        m_percentBetweenWaypoints = Mathf.Clamp01(m_percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(m_percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(m_globalWaypoints[m_fromWaypointIndex], m_globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (m_percentBetweenWaypoints >= 1)
        {
            m_percentBetweenWaypoints = 0;
            m_fromWaypointIndex++;

            if (m_fromWaypointIndex >= m_globalWaypoints.Length - 1)
            {

                m_fromWaypointIndex = 0;
                System.Array.Reverse(m_globalWaypoints);
                m_move = false;
            }
        }
        return newPos - transform.position;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (LocalWaypoints != null)
        {
            Gizmos.color = Color.black;

            for (int i = 0; i < LocalWaypoints.Length; i++)
            {
                Vector2 globalWaypointPos = (Application.isPlaying) ? m_globalWaypoints[i] : LocalWaypoints[i] + (Vector2)transform.position;
                Gizmos.DrawWireCube(globalWaypointPos, transform.localScale);
                if (i + 1 < LocalWaypoints.Length)
                {
                    Vector2 prePosition = (Application.isPlaying) ? m_globalWaypoints[i + 1] : LocalWaypoints[i + 1] + (Vector2)transform.position;
                    Gizmos.DrawLine(prePosition, globalWaypointPos);
                }
            }
        }
    }
#endif
}

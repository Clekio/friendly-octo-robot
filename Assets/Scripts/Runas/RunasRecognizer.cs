using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class recognizerRunas : MonoBehaviour
{
    [System.Serializable]
    public struct Runa
    {
        public string Name;
        public List<Vector2> dirList;
    }
    [Header("Lista de Runas")]
    public MagiaList listaDeMagias;

    public bool debug;

    protected List<Vector2> optimacedPointList;
    protected List<Vector2> normalizedPointList;
    protected List<Vector2> simplifyPointList;
    protected List<Vector2> globalPointList;

    protected Vector2 m_magicPosition;
    protected string m_magicName;
    protected float m_magicAngle;
    protected Vector2 m_magicScale;

    #region Constantes
    const float pointDistance = 1f;
    const int lineDistance = 40;
    const float minimumAngle = 135;
    #endregion

    protected void StartRecognizer(List<Vector2> PointsList)//, List<Vector2> DeltaList)
    {
        //DebugLine(PointsList, Colors.LightPink);
        if (PointsList != null && PointsList.Count > 0)
        {
            optimacedPointList = optimizeGesture(PointsList);

            if (optimacedPointList.Count < 3)
                return;

            globalPointList = GetGlobalPoints(optimacedPointList);
            GetMagicMesurements(globalPointList);

            normalizedPointList = NormalizeRune(optimacedPointList);

            simplifyPointList = SimplifyRune(normalizedPointList);

            MatchRune(simplifyPointList);
        }
    }

    protected List<Vector2> optimizedDirection;
    protected List<Vector2> optimizedDistance;
    private List<Vector2> optimizeGesture(List<Vector2> pointsList)//, List<Vector2> deltaList)
    {
        //Quitamos los puntos que no creen un cambio de direccion.
        optimizedDirection = new List<Vector2>();
        Vector2 pointA = pointsList[0];

        for (int i = 1; i < pointsList.Count - 1; ++i)
        {
            if (i >= pointsList.Count - 2 && Vector2.Distance(pointsList[i], pointA) > lineDistance)
            {
                optimizedDirection.Add(pointA);
                optimizedDirection.Add(pointsList[i]);
                
                continue;
            }

            if (Vector2.Distance(pointsList[i], pointA) < pointDistance || Vector2.Distance(pointsList[i], pointsList[i + 1]) < pointDistance)
                continue;

            Vector2 AB = pointsList[i] - pointA;
            Vector2 CB = pointsList[i] - pointsList[i + 1];
            float angle = Vector2.Angle(AB, CB);

            if (angle <= minimumAngle)
            {
                if (Vector2.Distance(pointsList[i], pointA) > lineDistance)
                {
                    optimizedDirection.Add(pointA);
                    optimizedDirection.Add(pointsList[i]);
                }
                pointA = pointsList[i];
            }
        }

        if (optimizedDirection.Count < 3)
            return optimizedDirection;

        optimizedDistance = new List<Vector2>();
        optimizedDistance.Add(optimizedDirection[0]);

        for (int i = 2; i < optimizedDirection.Count-1; i +=2)
        {
            if (Vector2.Distance(optimizedDirection[i], optimizedDirection[i-1]) < pointDistance)
                optimizedDistance.Add(optimizedDirection[i]);

            else
            {
                bool b = false;
                Vector2 v = GetIntersectionPointCoordinates(optimizedDirection[i - 2], optimizedDirection[i - 1], optimizedDirection[i], optimizedDirection[i + 1],out b);

                if (b)
                    optimizedDistance.Add(v);
            }
        }
        optimizedDistance.Add(optimizedDirection[optimizedDirection.Count - 1]);

        if (debug)
        {
            DebugExtension.DebugLine(optimizedDirection, Colors.IndianRed, 5);
            DebugExtension.DebugLine(optimizedDistance, Colors.Red, 5);

            foreach (Vector2 v in optimizedDistance)
                DebugExtension.DebugWireSphere(v, Colors.DarkRed, 10, 5);
        }

        return optimizedDistance;
    }

    private List<Vector2> NormalizeRune(List<Vector2> pointsList)
    {
        List<Vector2> normaliced = new List<Vector2>(pointsList);

        //Normalizamos la posicion
        Vector2 c = normaliced[0];
        for (int i = 0; i < normaliced.Count; i++)
        {
            normaliced[i] = normaliced[i] - c;
        }
        if (debug)
            DebugExtension.DebugLine(normaliced, Colors.LightGreen, 5);

        //Normalizamos la rotacion
        //Sacamos el angulo que hay que rotarlo del original
        float angle = Vector2.Angle(pointsList[1] - pointsList[0], Vector2.up);
        if ((pointsList[1] - pointsList[0]).x < 0)
        {
            angle = 360 - angle;
        }
        //Rotamos la normalizada
        for (int i = 0; i < normaliced.Count; i++)
        {
            normaliced[i] = Rotate(normaliced[i], angle);
        }
        if (debug)
            DebugExtension.DebugLine(normaliced, Colors.Green, 5);

        //Normalizamos la Inversion
        if ((normaliced[2] - normaliced[1]).x < 0)
        {
            for (int i = 0; i < normaliced.Count; i++)
            {
                normaliced[i] = new Vector2(-normaliced[i].x, normaliced[i].y);
            }
            if (debug)
                DebugExtension.DebugLine(normaliced, Colors.DarkGreen, 5);
        }

        return normaliced;
    }

    private List<Vector2> SimplifyRune(List<Vector2> pointList)
    {
        List<Vector2> simplified = new List<Vector2>();

        for (int i = 1; i < pointList.Count; i++)
        {
            Vector2 dir = pointList[i - 1] - pointList[i];
            float angulo = Vector2.Angle(Vector2.left, dir);
            Vector2 dirBasica = new Vector2(0, 0);

            if (angulo <= 22.5f)
                dirBasica = new Vector2(1, 0);
            else if (angulo > 22.5f && angulo <= 67.5f)
                dirBasica = new Vector2(1, 1);
            else if (angulo > 67.5f && angulo <= 112.5f)
                dirBasica = new Vector2(0, 1);
            else if (angulo > 112.5f && angulo <= 157.5f)
                dirBasica = new Vector2(-1, 1);
            else if (angulo > 157.5f)
                dirBasica = new Vector2(-1, 0);

            if (pointList[i].y < pointList[i - 1].y)
                dirBasica.y *= -1;

            simplified.Add(dirBasica);
        }
        Vector2 vP = new Vector2(0, 0);
        for (int i = 0; i < simplified.Count; i++)
        {
            if (debug) Debug.DrawLine(vP, simplified[i] + vP, Colors.LightBlue, 2, false);
            vP = vP + simplified[i];
        }

        return simplified;
    }

    private void MatchRune(List<Vector2> dirList)
    {
        for (int i = 0; i < listaDeMagias.magias.Count; i++)
        {
            if (CompareList(dirList, listaDeMagias.magias[i].runeVector2))
            {
                SpawnMagic(listaDeMagias.magias[i], m_magicPosition, m_magicAngle, m_magicScale);
                break;
            }
        }
    }

    protected abstract void SpawnMagic(Magia name, Vector2 position, float angle, Vector2 scale);

    private List<Vector2> GetGlobalPoints(List<Vector2> pointList)
    {
        List<Vector2> globalPoints = new List<Vector2>();

        foreach (Vector2 v2 in pointList)
            globalPoints.Add(Camera.main.ScreenToWorldPoint(new Vector3(v2.x, v2.y, -Camera.main.transform.position.z)));

        return globalPoints;
    }

    private void GetMagicMesurements(List<Vector2> pointList)
    {
        //Get direction
        m_magicAngle = Vector2.Angle(Vector2.left, pointList[0] - pointList[3]);

        //Get Center
        Vector2 center = Vector2.zero;
        foreach (Vector2 v in pointList)
        {
            center += v;
        }
        center = center / pointList.Count;
        m_magicPosition = center;

        //Get Scale
        m_magicScale = Vector2.zero;
        foreach (Vector2 v in pointList)
        {
            Vector2 vec = new Vector2(Mathf.Abs(v.x - center.x), Mathf.Abs(v.y - center.y));

            if (vec.x > m_magicScale.x)
                m_magicScale.x = vec.x;

            if (vec.y > m_magicScale.y)
                m_magicScale.y = vec.y;
        }
        m_magicScale = m_magicScale * 2;
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    private bool CompareList(List<Vector2> Input, List<Vector2> Pattern)
    {
        int inputIndex = 0;

        foreach (Vector2 v in Pattern)
        {
            bool found = false;
            while (!found)
            {
                if (inputIndex == Input.Count)
                {
                    return false;
                }
                if (Input[inputIndex] == v)
                {
                    found = true;
                }
                ++inputIndex;
            }
        }
        return true;
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        return Vector2.Angle(Vector2.right, diference);
    }

    /// <summary>
    /// Gets the coordinates of the intersection point of two lines.
    /// </summary>
    /// <param name="A1">A point on the first line.</param>
    /// <param name="A2">Another point on the first line.</param>
    /// <param name="B1">A point on the second line.</param>
    /// <param name="B2">Another point on the second line.</param>
    /// <param name="found">Is set to false of there are no solution. true otherwise.</param>
    /// <returns>The intersection point coordinates. Returns Vector2.zero if there is no solution.</returns>
    private Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2, out bool found)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

        if (tmp == 0)
        {
            // No solution!
            found = false;
            return Vector2.zero;
        }

        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

        found = true;

        return new Vector2(B1.x + (B2.x - B1.x) * mu, B1.y + (B2.y - B1.y) * mu);
    }
}

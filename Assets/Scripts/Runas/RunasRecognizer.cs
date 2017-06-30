using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recognizerRunas : MonoBehaviour
{
    [System.Serializable]
    public struct Runa
    {
        public string Name;
        public List<Vector2> dirList;
    }
    [Header("Lista de Runas")]
    public List<Runa> lista;

    protected List<Vector2> optimacedPointList;
    protected List<Vector2> normalizedPointList;
    protected List<Vector2> simplifyPointList;
    protected List<Vector2> globalPointList;

    protected Vector2   m_magicPosition;
    protected string    m_magicName;
    protected float     m_magicAngle;
    protected Vector2   m_magicScale;

    float distanceMargin    = 0.05f;
    //float mouseDeltaNeeded  = 0.05f;
    float minimumAngle      = 10;

    public void StartRecognizer(List<Vector2> PointsList)//, List<Vector2> DeltaList)
    {
        optimacedPointList = optimizeGesture(PointsList);//, DeltaList);

        if (optimacedPointList.Count < 3)
            //texto.text = "ERROR";
            return;

        globalPointList = GetGlobalPoints(optimacedPointList);

        GetMagicMesurements(globalPointList);

        normalizedPointList = NormalizeRune(optimacedPointList);

        simplifyPointList   = SimplifyRune(normalizedPointList);

        m_magicName         = MatchRune(simplifyPointList);
    }

    private List<Vector2> optimizeGesture(List<Vector2> pointsList)//, List<Vector2> deltaList)
    {
        //Si la lista de puntos tiene menos de 3 puntos ya es muy simple.
        if (pointsList.Count < 3)
            return pointsList;

        //Quitamos puntos que esten muy cerca o que se hayan pintado muy rapido sin detenerse.
        List<Vector2> optimizedDistance = new List<Vector2>();
        optimizedDistance.Add(pointsList[0]);

        for (int i=1; i < pointsList.Count; ++i)
        {
            Vector2 lastPoint = optimizedDistance[optimizedDistance.Count - 1];
            if (Vector2.Distance(lastPoint, pointsList[i]) > distanceMargin)// && (deltaList[i] - deltaList[i-1]).magnitude < mouseDeltaNeeded)
            {
                optimizedDistance.Add(pointsList[i]);
            }
        }

//DebugLine(optimizedDistance, Colors.DarkRed);

        //Quitamos los puntos que no creen un cambio de direccion.
        List<Vector2> optimizedDirection = new List<Vector2>();
        optimizedDirection.Add(pointsList[0]);

        for (int i = 1; i < optimizedDistance.Count - 1; ++i)
        {
            Vector2 lastPoint = optimizedDirection[optimizedDirection.Count - 1];
            float anguloAB = AngleBetweenVector2(optimizedDistance[i], lastPoint);
            float anguloAC = AngleBetweenVector2(optimizedDistance[i + 1], lastPoint);

            if (Mathf.Abs(anguloAB - anguloAC) > minimumAngle)
            {
                optimizedDirection.Add(optimizedDistance[i]);
            }
        }
        optimizedDirection.Add(optimizedDistance[optimizedDistance.Count - 1]);

//DebugLine(optimizedDirection, Colors.Red);

        return optimizedDirection;
    }

    private List<Vector2> NormalizeRune (List<Vector2> pointsList)
    {
        List<Vector2> normaliced = new List<Vector2>(pointsList);

    //Normalizamos la posicion
        Vector2 c = normaliced[0];
        for (int i = 0; i < normaliced.Count; i++)
        {
            normaliced[i] = normaliced[i] - c;
        }
//DebugLine(normaliced, Colors.DarkGreen);

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
//DebugLine(normaliced, Colors.Green);

    //Normalizamos la Inversion
        if ((normaliced[2] - normaliced[1]).x < 0)
        {
            for (int i = 0; i < normaliced.Count; i++)
            {
                normaliced[i] = new Vector2(-normaliced[i].x, normaliced[i].y);
            }
//DebugLine(normaliced, Colors.LightGreen);
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
        //Vector2 vP = new Vector2(0, 0);
        //for (int i = 0; i < simplified.Count; i++)
        //{
        //    Debug.DrawLine(vP, simplified[i] + vP, Colors.LightBlue, 2, false);
        //    vP = vP + simplified[i];
        //}

        return simplified;
    }

    private string MatchRune (List<Vector2> dirList)
    {
        string runaName = "Error";

        for (int i = 0; i < lista.Count; i++)
        {
            if (CompareList(dirList, lista[i].dirList))
            {
                runaName = lista[i].Name;
                break;
            }
        }
        //Debug.Log(runaName);

        return runaName;
    }

    private List<Vector2> GetGlobalPoints(List<Vector2> pointList)
    {
        List<Vector2> globalPoints = new List<Vector2>();

        foreach (Vector2 v2 in pointList)
            globalPoints.Add(Camera.main.ScreenToWorldPoint(new Vector3(v2.x, v2.y, - Camera.main.transform.position.z)));

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

        foreach(Vector2 v in Pattern)
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

#if UNITY_EDITOR
    public void DebugLine(List<Vector2> Linea, Color color)
    {
        //Draw the lines compositions
        if (Linea != null)
        {
            Vector2 vP = Linea[0];
            for (int i = 1; i < Linea.Count; i++)
            {
                Debug.DrawLine(vP, Linea[i], color);
                vP = Linea[i];
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (optimacedPointList != null)
            DebugLine(optimacedPointList, Colors.Red);

        if (normalizedPointList != null)
            DebugLine(normalizedPointList, Colors.Green);

        if (globalPointList != null)
            DebugLine(globalPointList, Colors.Pink);

        if (simplifyPointList != null)
        {
            Vector2 vP = new Vector2(0, 0);
            for (int i = 0; i < simplifyPointList.Count; i++)
            {
                Debug.DrawLine(vP, simplifyPointList[i] + vP, Colors.Blue);
                vP = vP + simplifyPointList[i];
            }
        }
    }
#endif
}

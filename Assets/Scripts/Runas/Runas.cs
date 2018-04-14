using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
//using XboxCtrlrInput;

public class Runas : recognizerRunas
{
    List<Vector2> m_pointList = new List<Vector2> ();

    [Header("Puntero")]
    [SerializeField]
    private float velocidadPuntero;
    private Vector2 posPuntero;
    private GameObject magicParticles;
    
    public particleRunas particulasR;

    //bool drawing = false;

    [Header("Magias")]
    public bool Agua = false;
    public bool Viento = false;
    public bool Trueno = false;

    public void StartMagia(bool b, Vector3 playerPos)
    {
        if (!Agua && !Viento && !Trueno)
            return;
        
        deltaTotal = 0;
        deltaCount = 0;
        m_pointList.Clear();
        
        if (b)
        {
            particulasR.startMovement(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)));
            lastPositon = Input.mousePosition;
        }

        else
        {
            posPuntero = playerPos + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * 3;
            particulasR.startMovement((Vector3)posPuntero + Vector3.forward * 20);
            lastPositon = Camera.main.WorldToScreenPoint(posPuntero);
        }
        StartCoroutine(DoMagia(b));
    }
    
    private IEnumerator DoMagia(bool b)
    {
        while (InputManager.ActiveDevice.LeftBumper.IsPressed)
        {
            if (b)
            {
                deltaTotal += Vector2.Distance(lastPositon, (Vector2)Input.mousePosition);
                deltaCount++;
                lastPositon = Input.mousePosition;
                
                m_pointList.Add(Input.mousePosition);

                particulasR.move(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z)));
            }
            else
            {
                posPuntero += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * velocidadPuntero * Time.deltaTime;

                particulasR.move((Vector3)posPuntero + Vector3.forward * 20);

                Vector3 screenPosPuntero = Camera.main.WorldToScreenPoint(posPuntero);

                deltaTotal += Vector2.Distance(lastPositon, (Vector2)screenPosPuntero);
                deltaCount++;
                lastPositon = screenPosPuntero;

                m_pointList.Add(screenPosPuntero);
            }
            yield return null;
        }
        particulasR.endMove();

        //Reconocer la Runa
        StartRecognizer(m_pointList);
    }

    protected override void SpawnMagic(Magia magia, Vector2 position, float angle, Vector2 scale)
    {
        if (magia.Name == "wind" && !Viento || magia.Name == "water" && !Agua || magia.Name == "thunder" && !Trueno)
            return;

        GameObject effect;

        effect = Instantiate(magia.Effect, new Vector3(position.x, position.y, 0), Quaternion.identity) as GameObject;

        if (magia.Name == "wind")
        {
            effect.transform.localScale = scale;
            effect.GetComponent<AreaEffector2D>().forceAngle = angle;
            Destroy(effect, 4);
        }
        else if (magia.Name == "water")
            effect.transform.rotation = Quaternion.Euler(90, 90, 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(posPuntero, 0.25f);

        if (m_pointList != null && m_pointList.Count > 1)
            DebugExtension.DebugLine(m_pointList, Colors.LightBlue, Colors.DarkBlue);

        //if (optimacedPointList != null && optimacedPointList.Count > 0)
        //    DebugExtension.DebugLine(optimacedPointList, Colors.Red, Colors.IndianRed);

        //if (normalizedPointList != null && normalizedPointList.Count > 0)
        //    DebugExtension.DebugLine(normalizedPointList, Colors.LightGreen, Colors.Green);

        //if (simplifyPointList != null && simplifyPointList.Count > 0)
        //{
        //    Vector2 vP = new Vector2(0, 0);
        //    for (int i = 0; i < simplifyPointList.Count; i++)
        //    {
        //        Debug.DrawLine(vP, simplifyPointList[i] + vP, Colors.Blue);
        //        vP = vP + simplifyPointList[i];
        //    }
        //}
    }
#endif
}

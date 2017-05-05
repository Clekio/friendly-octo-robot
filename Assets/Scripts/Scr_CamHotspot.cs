using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CamHotspot : MonoBehaviour {


    public Transform ObjectToFocus;
    public Transform Player;
    public GameObject gameCamera;
    public float focusSize;
    public float speed;
    public bool GizmoEnabled;

    Camera cameraComp;
    Scr_CamMov scrCamara;
    float oldOffsetX;
    float oldOffsetY;
    bool permiso;
    float oldSize;
    float pmX;
    float pmY;
    float aspectRatioY;

    void Start ()
    {
        permiso = false;
        cameraComp = gameCamera.GetComponent<Camera>();
        scrCamara = gameCamera.GetComponent<Scr_CamMov>();
        oldSize = cameraComp.fieldOfView;
        oldOffsetX = scrCamara.OffsetX;
        oldOffsetY = scrCamara.OffsetY;
    }


    void Update ()
    {
        if (permiso)
        {
            scrCamara.OffsetX = ((Player.position.x + ObjectToFocus.position.x) / 2) - Player.position.x;
            scrCamara.OffsetY = ((Player.position.y + ObjectToFocus.position.y) / 2) - Player.position.y;
            if (cameraComp.fieldOfView < focusSize)
            {
                cameraComp.fieldOfView = Mathf.Lerp(cameraComp.fieldOfView, focusSize, Time.deltaTime * speed);
            }
        }
        if (permiso == false && cameraComp.fieldOfView > oldSize)
        {
            cameraComp.fieldOfView = Mathf.Lerp(cameraComp.fieldOfView, oldSize, Time.deltaTime * speed);
        }
    }


    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.tag == "Player")
        {
            permiso = true;
        }
    }

    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            permiso = false;
            scrCamara.OffsetX = oldOffsetX;
            scrCamara.OffsetY = oldOffsetY;
        }
    }

    void OnDrawGizmos()
    {
        if (GizmoEnabled)
        {
            pmX = (transform.position.x + ObjectToFocus.position.x) / 2;
            pmY = (transform.position.y + ObjectToFocus.position.y) / 2;
            aspectRatioY = (focusSize * 9) / 16;
            Gizmos.color = new Color(1, 0, 0, 0.5F);
            Gizmos.DrawCube (new Vector2 (pmX, pmY), new Vector2(focusSize/2, aspectRatioY/2));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRunas : MonoBehaviour {

    public float rateOverDistance = 5;

    public ParticleSystem pSystem;

    private Vector3 pos;
    private Vector3 lastPos;

    private float posDelta;
    private float minDelta;

    private Vector2 v2;

    private void Awake()
    {
        var main = pSystem.main;
        main.customSimulationSpace = Camera.main.transform;
    }

    // Use this for initialization
    void Start ()
    {
        transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        
        pSystem.Play(true);
        StartCoroutine(followMouse());

        lastPos = transform.position;
        minDelta = 1 / rateOverDistance;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.localPosition;
        
        posDelta += (pos - lastPos).magnitude;
        if (posDelta >= minDelta)
        {
            StartCoroutine(DoEmit(posDelta / minDelta, lastPos, pos));
            posDelta = 0;
        }

        lastPos = pos;
    }

    private IEnumerator DoEmit(float n, Vector3 pInit, Vector3 pfin)
    {
        int nP = Mathf.RoundToInt(n);
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.applyShapeToPosition = true;

        for (int i = 0; i < nP; i++)
        {
            //v2 = Vector3.Lerp(pInit, pfin, (float)i / nP);
            //emitParams.position = new Vector3 (v2.x, v2.y, 30);
            emitParams.position = Camera.main.transform.InverseTransformPoint(Vector3.Lerp(pInit, pfin, (float)i / nP));
            pSystem.Emit(emitParams, 1);
        }

        yield return null;
    }

    private IEnumerator followMouse()
    {
        while (Input.GetMouseButton(0))
        {
            transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            yield return null;
        }

        //paramos el sistema de parículas y lo borramos.
        pSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        Destroy(gameObject, 3);
    }
}

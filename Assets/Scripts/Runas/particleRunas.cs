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

    ParticleSystem.Particle[] allParticles;

    const float minTime = 0.05f;
    const float maxTime = 0.2f;

    // Use this for initialization
    void Start ()
    {
        var main = pSystem.main;
        main.customSimulationSpace = Camera.main.transform;
        minDelta = 1 / rateOverDistance;

        allParticles = new ParticleSystem.Particle[pSystem.main.maxParticles];
    }

    public void startMovement(Vector3 pos)
    {
        transform.position = pos;
        pSystem.Play(true);

        lastPos = pos;
    }

    public void move(Vector3 pos)
    {
        //pos = transform.localPosition;
        transform.localPosition = pos;

        posDelta += (pos - lastPos).magnitude;
        if (posDelta >= minDelta)
        {
            int nP = Mathf.RoundToInt(posDelta / minDelta);
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.applyShapeToPosition = true;

            for (int i = 0; i < nP; i++)
            {
                emitParams.position = Camera.main.transform.InverseTransformPoint(Vector3.Lerp(lastPos, pos, (float)i / nP));
                pSystem.Emit(emitParams, 1);
            }

            posDelta = 0;
        }

        lastPos = pos;
    }

    public void endMove()
    {
        pSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        int numParticles = pSystem.GetParticles(allParticles);

        for (int i = 0; i < numParticles; i++)
        {
            ParticleSystem.Particle particle = allParticles[i];

            particle.remainingLifetime = particle.startLifetime * Random.Range(minTime, maxTime);

            allParticles[i] = particle;
        }

        pSystem.SetParticles(allParticles, numParticles);
    }

    //private IEnumerator Disperse()
    //{
    //    int numParticles = pSystem.GetParticles(allParticles);
    //    for (float i = 1; i>0; i -= Time.deltaTime)
    //    {
    //        for (int e = 0; e < numParticles; e++)
    //        {
    //            ParticleSystem.Particle particle = allParticles[e];

    //            particle.remainingLifetime = i;

    //            allParticles[e] = particle;
    //        }

    //        pSystem.SetParticles(allParticles, numParticles);
    //        yield return null;
    //    }
    //}

    //private IEnumerator DoEmit(float n, Vector3 pInit, Vector3 pfin)
    //{
    //    int nP = Mathf.RoundToInt(n);
    //    var emitParams = new ParticleSystem.EmitParams();
    //    emitParams.applyShapeToPosition = true;

    //    for (int i = 0; i < nP; i++)
    //    {
    //        //v2 = Vector3.Lerp(pInit, pfin, (float)i / nP);
    //        //emitParams.position = new Vector3 (v2.x, v2.y, 30);
    //        emitParams.position = Camera.main.transform.InverseTransformPoint(Vector3.Lerp(pInit, pfin, (float)i / nP));
    //        pSystem.Emit(emitParams, 1);
    //        yield return null;
    //    }
    //}

    //private IEnumerator followMouse()
    //{
    //    while (Input.GetMouseButton(0))
    //    {
    //        transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
    //        yield return null;
    //    }

    //    //paramos el sistema de parículas y lo borramos.
    //    pSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    //    Destroy(gameObject, 3);
    //}
}

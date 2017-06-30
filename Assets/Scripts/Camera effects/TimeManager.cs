using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TimeManager : MonoBehaviour {

    public float slowdownFactor = 0.05f;
    public float unSlowdownTime = 1f;

    private float refTime = 0;

    //Shader
    public Shader CurShader;
    [Range(0, 1)]
    public float GrayScaleAmount = 0.8f;
    private float grayScale = 0f;

    Material curMaterial;

    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(CurShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DoSlowmotion();

        if (Input.GetMouseButtonUp(0))
            StartCoroutine(UndoSlowmotion());
    }

    private void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!CurShader && !CurShader.isSupported)
            enabled = false;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (CurShader != null)
        {
            material.SetFloat("_LuminosityAmount", grayScale);
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    private void OnDisable()
    {
        if (curMaterial)
            DestroyImmediate(curMaterial);
    }

    public void DoSlowmotion()
    {
        StopCoroutine(UndoSlowmotion());
        grayScale = GrayScaleAmount;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    IEnumerator UndoSlowmotion()
    {
        while (Time.timeScale != 1)
        {
            grayScale -= (GrayScaleAmount / unSlowdownTime) * Time.unscaledDeltaTime;
            grayScale = Mathf.Clamp(grayScale, 0f, GrayScaleAmount);
            Time.timeScale += (1f / unSlowdownTime) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            yield return null;
        }
    }
}

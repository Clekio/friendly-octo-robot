using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BuildEscalera))]
public class EscaleraInspector : Editor
{
    protected virtual void OnSceneGUI()
    {
        BuildEscalera escalera = target as BuildEscalera;

        Handles.color = new Color(1, 1, 1, 0.5f);

        float size = HandleUtility.GetHandleSize(escalera.targetPosition) * 0.5f;
        float snap = 0.5f;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.Slider(escalera.targetPosition, Vector3.up, size, Handles.ConeHandleCap, snap);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(escalera, "Change Look At Target Position");
            escalera.targetPosition = newTargetPosition;
            escalera.SetEscaleras();
        }
    }
}

using UnityEditor;

[CustomEditor(typeof(MagiaList))]
public class MagiaInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        MagiaList mTest = (MagiaList)target;
        SerializedProperty list = serializedObject.FindProperty("magias");
        EditorList.Show(list, EditorListOption.ListLabel | EditorListOption.Buttons);

        serializedObject.ApplyModifiedProperties();
    }
}

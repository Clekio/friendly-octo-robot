using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Magia))]
public class MagiaDrawer : PropertyDrawer
{
    private Vector2 margen = new Vector2(1,2);
    private float elementHeith = 16f;
    private float nRuneDirInLine = 3f;
    private float margenInferior = 5f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.indentLevel = 0;
        position.x += 15;
        position.width -= 15;
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentRect = EditorGUI.PrefixLabel(position, label);

        Rect nameRect = contentRect;
        nameRect.width *= 0.25f;
        nameRect.height = elementHeith;
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);

        Rect objRect = contentRect;
        objRect.x += nameRect.width + margen.x;
        objRect.width *= 0.75f;
        objRect.height = elementHeith;
        EditorGUIUtility.labelWidth = 20f;
        EditorGUI.PropertyField(objRect, property.FindPropertyRelative("Effect"), new GUIContent("FX"));
        
        Rect runeRect = contentRect;
        runeRect.y += nameRect.height + margen.y;
        Show(runeRect, property.FindPropertyRelative("Rune"));

        EditorGUI.EndProperty();
    }

    public void Show(Rect listRect, SerializedProperty list)
    {
        Rect listLableRect = listRect;
        listLableRect.height = elementHeith;
        EditorGUI.PropertyField(listLableRect, list);
        listRect.y += listLableRect.height;

        if (list.isExpanded)
        {
            Rect elementRect = new Rect(listRect.position, new Vector2(listRect.width/ nRuneDirInLine, elementHeith));
            Rect pRec = elementRect;
            for (int i = 0; i < list.arraySize; i++)
            {
                int c = i % 3;
                
                EditorGUI.PropertyField(pRec, list.GetArrayElementAtIndex(i), GUIContent.none);

                if (c == 0)
                    pRec.x += elementRect.width;
                else if (c == 1)
                    pRec.x += elementRect.width;
                if (c == 2)
                {
                    pRec.y += elementRect.height;
                    pRec.x = listRect.x;
                }
            }
            if (list.arraySize > 0)
            {
                pRec.width /= 2;
                if (GUI.Button(pRec, "-", EditorStyles.miniButtonLeft))
                {
                    int oldSize = list.arraySize;
                    list.DeleteArrayElementAtIndex(list.arraySize - 1);
                    if (list.arraySize == oldSize)
                    {
                        list.DeleteArrayElementAtIndex(list.arraySize - 1);
                    }
                }
                pRec.x += pRec.width;
            }
            if (GUI.Button(pRec, "+", EditorStyles.miniButtonRight))
            {
                list.InsertArrayElementAtIndex(list.arraySize);
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float unitHeight = elementHeith + margen.y;
        float totalHeight = (unitHeight) * 2;

        SerializedProperty list = property.FindPropertyRelative("Rune");

        if (list.isExpanded)
        {
            int nLineas = Mathf.CeilToInt((list.arraySize + 1) / nRuneDirInLine);
            totalHeight += unitHeight * nLineas;
        }

        totalHeight += margenInferior;

        return totalHeight;
    }
}

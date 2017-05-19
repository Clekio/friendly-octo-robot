using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota : MonoBehaviour
{
    //Define Enum
    public enum NoteColor { Blanco, Rojo, Verde, Azul, Amarillo, Negro };

    //This is what you need to show in the inspector.
    public NoteColor noteColor;

    public string Autor;

    //TextAreaAttribute(int minLines, int maxLines);
    [TextArea(10, 20)]
    public string Texto;

    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, .5f);
        switch (noteColor)
        {
            case NoteColor.Blanco:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_White", true);
                break;
            case NoteColor.Rojo:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_Red", true);
                break;
            case NoteColor.Verde:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_Green", true);
                break;
            case NoteColor.Azul:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_Blue", true);
                break;
            case NoteColor.Amarillo:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_Yellow", true);
                break;
            case NoteColor.Negro:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_Black", true);
                break;
            default:
                Gizmos.DrawIcon(transform.position, "NoteGizmo_White", true);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Magia
{
    public string Name;
    public GameObject Effect;
    public runeDir[] Rune;
    [HideInInspector]
    public List<Vector2> runeVector2
    {
        get
        {
            List < Vector2 > r = new List<Vector2>();

            for (int i = 0; i<Rune.Length; i++)
            {
                switch (Rune[i])
                {
                    case runeDir.Up:
                        r.Add(Vector2.up);
                        break;
                    case runeDir.UpRight:
                        r.Add(new Vector2(1, 1));
                        break;
                    case runeDir.Right:
                        r.Add(Vector2.right);
                        break;
                    case runeDir.DownRight:
                        r.Add(new Vector2(1, -1));
                        break;
                    case runeDir.Down:
                        r.Add(Vector2.down);
                        break;
                    case runeDir.DownLeft:
                        r.Add(new Vector2(-1, -1));
                        break;
                    case runeDir.Left:
                        r.Add(Vector2.left);
                        break;
                    case runeDir.UpLeft:
                        r.Add(new Vector2(-1,1));
                        break;
                }
            }

            return r;
        }
    }
}

public enum runeDir { Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft}
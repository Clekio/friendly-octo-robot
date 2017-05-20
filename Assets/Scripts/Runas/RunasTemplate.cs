using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunasTemplate
{

    public struct Runa
    {
        public List<Vector2> dirList;
        public string Name;
    }

    public static List<Runa> TemplateRunasList = new List<Runa>();
    public RunasTemplate()
    {
        //Cuadrado
        Runa Cuadrado1 = new Runa();
        Cuadrado1.Name = "wind";
        Cuadrado1.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(0, -1), new Vector2(-1, 0) });
        TemplateRunasList.Add(Cuadrado1);

        Runa Cuadrado2 = new Runa();
        Cuadrado2.Name = "wind";
        Cuadrado2.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, -1), new Vector2(-1, -1) });
        TemplateRunasList.Add(Cuadrado2);

        Runa Cuadrado3 = new Runa();
        Cuadrado3.Name = "wind";
        Cuadrado3.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, -1), new Vector2(0, -1), new Vector2(-1, 1) });
        TemplateRunasList.Add(Cuadrado3);


        //Triangulo
        Runa Triangulo1 = new Runa();
        Triangulo1.Name = "swirl";
        Triangulo1.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, -1), new Vector2(-1, 0) });
        TemplateRunasList.Add(Triangulo1);

        Runa Triangulo2 = new Runa();
        Triangulo2.Name = "swirl";
        Triangulo2.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, -1), new Vector2(-1, -1) });
        TemplateRunasList.Add(Triangulo2);

        Runa Triangulo3 = new Runa();
        Triangulo3.Name = "swirl";
        Triangulo3.dirList = new List<Vector2>(new Vector2[] { new Vector2(0, 1), new Vector2(1, 0), new Vector2(-1, -1) });
        TemplateRunasList.Add(Triangulo3);
    }
}

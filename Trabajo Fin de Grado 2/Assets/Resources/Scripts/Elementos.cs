using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Elementos
{
    public abstract class Elemento
    {
        public string nombre { get; protected set; }
        public int ID { get; protected set; }
        public Dictionary<string,string> propiedades { get; protected set; }
        public void addPropiedad(string clave, string valor)
        {
            propiedades[clave] = valor;
        }
        public string getPropiedad(string clave)
        {
            if (propiedades[clave] == null) return "NO EXISTE VALOR PARA ESA CLAVE";
            return propiedades[clave];
        }
    }
    public class Personaje : Elemento 
    {
        public Personaje(string nom, int id) {
            nombre = nom;
            ID = id;
            propiedades = new Dictionary<string, string>();
            propiedades["_Tipo"] = "Personaje";
        }
        public Personaje(string nom, int id, Dictionary<string, string> prop)
        {
            nombre = nom;
            ID = id;
            propiedades = prop;
        }
    }
    public class NPC : Elemento
    {
        public NPC(string nom, int id)
        {
            nombre = nom;
            ID = id;
            propiedades = new Dictionary<string, string>();
            propiedades["_Tipo"] = "NPC";
        }
        public NPC(string nom, int id, Dictionary<string, string> prop)
        {
            nombre = nom;
            ID = id;
            propiedades = prop;
        }
    }
    public class Objeto : Elemento
    {
        public Objeto(string nom, int id)
        {
            nombre = nom;
            ID = id;
            propiedades = new Dictionary<string, string>();
            propiedades["_Tipo"] = "Objeto";
        }
        public Objeto(string nom, int id, Dictionary<string, string> prop)
        {
            nombre = nom;
            ID = id;
            propiedades = prop;
        }
    }
    public class Accion : Elemento
    {
        public Accion(string nom, int id)
        {
            nombre = nom;
            ID = id;
            propiedades = new Dictionary<string, string>();
            propiedades["_Tipo"] = "Accion";
        }
        public Accion(string nom, int id, Dictionary<string, string> prop)
        {
            nombre = nom;
            ID = id;
            propiedades = prop;
        }
    }
    public class Regla
    {
        public List<List<Elemento>> antecedentes = new List<List<Elemento>>();
        public List<Elemento> consecuente;

        public Regla(List<List<Elemento>> ant, List<Elemento> cons)
        {
            foreach( List<Elemento> a in ant)
                antecedentes.Add(a);
            consecuente = cons;
        }

        public static string antecConsecToString(List<Elemento> antecedente){
            string s = " ";
            foreach (Elemento elem in antecedente)
            {
                s += elem.nombre;
                s += " ";
            }
            return s;
        }
    }

    // --------------------

    public abstract class ElementosJuego
    {
        public Vector2 posicion;
        public Elemento elemento;
        public GameObject GO;
        public string movimiento;
        public int modo = 0;
        public string reaparece = "";
        public abstract Vector3 posicion3D();
        public abstract void changeColor();
        public int crecer = 0;
        public Color color = Color.clear;
        public List<Elemento> inventario = new List<Elemento>();
    }

    public class Jugador : ElementosJuego
    {
        
        public string cola = "";
        public List<Cola> tail = new List<Cola>();

        public Jugador(Personaje pers, Vector2 pos)
        {
            elemento = pers;
            posicion = pos;
        }
        public override Vector3 posicion3D()
        {
            return new Vector3((posicion.x - 5f), 0.16f, (posicion.y - 5f));
        }
        public override void changeColor()
        {
            if (color != Color.clear)
            {
                GO.renderer.material.color = color;
            }
        }
    }
    public class Obstaculo : ElementosJuego
    {

        public Obstaculo(Objeto obj, Vector2 pos)
        {
            elemento = obj;
            posicion = pos;
        }
        public override Vector3 posicion3D()
        {
            return new Vector3((posicion.x - 5f), 0.2f, (posicion.y - 5f));
        }
        public override void changeColor()
        {
            if (color != Color.clear)
            {
                GO.renderer.material.color = color;
            }
        }
    }
    public class Comida : ElementosJuego
    {

        public Comida(Objeto obj, Vector2 pos)
        {
            elemento = obj;
            posicion = pos;
        }
        public override Vector3 posicion3D()
        {
            return new Vector3((posicion.x - 5f), 0.1f, (posicion.y - 5f));
        }
        public override void changeColor()
        {
            if (color != Color.clear)
            {
                GO.renderer.material.color = color;
            }
        }
    }
    public class NPCEnMapa : ElementosJuego
    {

        public NPCEnMapa(NPC np, Vector2 pos)
        {
            elemento = np;
            posicion = pos;
        }
        public override Vector3 posicion3D()
        {
            return new Vector3((posicion.x - 5f), 0.16f, (posicion.y - 5f));
        }
        public override void changeColor()
        {
            if (color != Color.clear)
            {
                GO.renderer.material.color = color;
            }
        }
    }
    public class Enemigo : ElementosJuego
    {
        public string cola = "";
        public List<Cola> tail = new List<Cola>();

        public Enemigo(NPC enem, Vector2 pos)
        {
            elemento = enem;
            posicion = pos;
        }
        public override Vector3 posicion3D()
        {
            return new Vector3((posicion.x - 5f), 0.16f, (posicion.y - 5f));
        }
        public override void changeColor()
        {
            if (color != Color.clear)
            {
                GO.renderer.material.color = color;
            }
        }
    }

    public class Cola
    {
        public Vector3 posicion;
        public GameObject GO;

        public Cola(GameObject pf, Vector3 pos)
        {
            GO = pf;
            GO.transform.parent = GameObject.Find("TailContainer").transform;
            posicion = pos;
            actualizarPos();
        }

        public void actualizarPos()
        {
            GO.transform.position = posicion;
            
        }
    }
    public class Dialogo
    {
        public float posicion = 0f;
        public bool pulsable = false;
        public Objeto elemento;
        public string texto;

        public Dialogo(Objeto obj)
        {
            elemento = obj;
            if(elemento.propiedades.ContainsKey("texto"))texto = elemento.propiedades["texto"];
        }
    }
}

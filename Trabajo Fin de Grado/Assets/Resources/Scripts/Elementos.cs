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
            if (!propiedades.ContainsKey(clave)) return "NO EXISTE VALOR PARA ESA CLAVE";
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
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elementos;
namespace ListaC
{
    public class ListaClass : MonoBehaviour
    {

        private List<Personaje> Personajes = new List<Personaje>();
        private List<NPC> NPCs = new List<NPC>();
        private List<Accion> Acciones = new List<Accion>();
        private List<Objeto> Objetos = new List<Objeto>();
        private List<Regla> Reglas = new List<Regla>();
        bool PersonajesChanged = false, NPCChanged = false, ObjetosChanged = false, AccionesChanged = false;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void addPersonaje(string s, int id)
        {
            Personajes.Add(new Personaje(s, id));
            PersonajesChanged = true;
        }

        public void addNPC(string s, int id)
        {
            NPCs.Add(new NPC(s, id));
            NPCChanged = true;
        }

        public void addObjeto(string s, int id)
        {
            Objetos.Add(new Objeto(s, id));
            ObjetosChanged = true;
        }

        public void addAccion(string s, int id)
        {
            Acciones.Add(new Accion(s, id));
            AccionesChanged = true;
        }

        public void addRegla(List<List<Elemento>> antec, List<Elemento> consec)
        {
            Reglas.Add(new Regla(antec, consec));
        }

        public void removePersonaje(int id)
        {
            Personaje aux = getPersonajeByID(id);
            if(aux != null)
            Personajes.Remove(aux);
        }

        public void removeNPC(int id)
        {
            NPC aux = getNPCByID(id);
            if (aux != null)
                NPCs.Remove(aux);
        }

        public void removeObjeto(int id)
        {
            Objeto aux = getObjetoByID(id);
            if (aux != null)
                Objetos.Remove(aux);
        }

        public void removeAccion(int id)
        {
            Accion aux = getAccionByID(id);
            if (aux != null)
                Acciones.Remove(aux);
        }

        public void removeRegla(int indice)
        {
            Reglas.RemoveAt(indice);
        }

        public string PersonajesToString() 
        {
            string s = "Personajes Jugadores: ";
            if (Personajes.Count < 1) s += "¡Ninguno!";
            else
            {
               for (int i = 0; i < Personajes.Count; i++)
               {
                    s += "\n Nombre: " + Personajes[i].nombre;
                    s += "   ID: " + Personajes[i].ID;
               }     
            }
            return s;
        }

        public string NPCToString()
        {
            string s = "Personajes no Jugadores: ";
            if (NPCs.Count < 1) s += "¡Ninguno!";
            else
            {
                for (int i = 0; i < NPCs.Count; i++)
                {
                    s += "\n Nombre: " + NPCs[i].nombre;
                    s += "   ID: " + NPCs[i].ID;
                }
            }
            return s;
        }

        public string ObjetosToString()
        {
            string s = "Objetos: ";
            if (Objetos.Count < 1) s += "¡Ninguno!";
            else
            {
                for (int i = 0; i < Objetos.Count; i++)
                {
                    s += "\n Nombre: " + Objetos[i].nombre;
                    s += "   ID: " + Objetos[i].ID;
                }
            }
            return s;
        }

        public string AccionesToString()
        {
            string s = "Acciones: ";
            if (Acciones.Count < 1) s += "¡Ninguno!";
            else
            {
                for (int i = 0; i < Acciones.Count; i++)
                {
                    s += "\n Nombre: " + Acciones[i].nombre;
                    s += "   ID: " + Acciones[i].ID;
                }
            }
            return s;
        }

        public string ReglasToString()
        {
            string s = "  Reglas: ";
            if (Reglas.Count < 1) s += "¡Ninguna!";
            else
            {
                int a = 0;
                for (int i = 0; i < Reglas.Count; i++)
                {
                    s += "\n  ";
                    s += i;
                    s += " -- ";
                    s += "Si ";
                    foreach (List<Elemento> antecedentes in Reglas[i].antecedentes)
                    {
                        a++;
                        foreach (Elemento elem in antecedentes)
                        {
                            s += elem.nombre;
                            s += " ";
                        }
                        if(a < Reglas[i].antecedentes.Count) s += "y ";
                    }
                    s += "entonces ";
                    foreach (Elemento elem in Reglas[i].consecuente)
                    {
                        s += elem.nombre;
                        s += " ";
                    }
                    s += "  ";
                }
            }
            return s;
        }

        public int getNumPersonajes()
        {
            return Personajes.Count;
        }

        public int getNumNPCs()
        {
            return NPCs.Count;
        }

        public int getNumObjetos()
        {
            return Objetos.Count;
        }

        public int getNumAcciones()
        {
            return Acciones.Count;
        }

        public Personaje getPersonaje(int i)
        {
            return Personajes[i];
        }

        public NPC getNPC(int i)
        {
            return NPCs[i];
        }

        public Objeto getObjeto(int i)
        {
            return Objetos[i];
        }

        public Accion getAccion(int i)
        {
            return Acciones[i];
        }

        public List<Personaje> getPersonajes()
        {
            return Personajes;
        }

        public List<NPC> getNPCs()
        {
            return NPCs;
        }

        public List<Objeto> getObjetos()
        {
            return Objetos;
        }

        public List<Accion> getAcciones()
        {
            return Acciones;
        }

        public List<Regla> getReglas()
        {
            return Reglas;
        }

        public Personaje getPersonajeByName(string n)
        {
            for (int i = 0; i < Personajes.Count; i++)
            {
                if (Personajes[i].nombre == n) return Personajes[i];
            }
            return null;
        }

        public NPC getNPCByName(string n)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                if (NPCs[i].nombre == n) return NPCs[i];
            }
            return null;
        }

        public Objeto getObjetoByName(string n)
        {
            for (int i = 0; i < Objetos.Count; i++)
            {
                if (Objetos[i].nombre == n) return Objetos[i];
            }
            return null;
        }

        public Accion getAccionByName(string n)
        {
            for (int i = 0; i < Acciones.Count; i++)
            {
                if (Acciones[i].nombre == n) return Acciones[i];
            }
            return null;
        }

        public Personaje getPersonajeByID(int id)
        {
            for (int i = 0; i < Personajes.Count; i++)
            {
                if (Personajes[i].ID == id) return Personajes[i];
            }
            return null;
        }

        public NPC getNPCByID(int id)
        {
            for (int i = 0; i < NPCs.Count; i++)
            {
                if (NPCs[i].ID == id) return NPCs[i];
            }
            return null;
        }

        public Objeto getObjetoByID(int id)
        {
            for (int i = 0; i < Objetos.Count; i++)
            {
                if (Objetos[i].ID == id) return Objetos[i];
            }
            return null;
        }

        public Accion getAccionByID(int id)
        {
            for (int i = 0; i < Acciones.Count; i++)
            {
                if (Acciones[i].ID == id) return Acciones[i];
            }
            return null;
        }

        public string PropiedadesToString(Elemento elem)
        {
            string s = "Propiedades de: " + elem.nombre;
            foreach (KeyValuePair<string, string> entry in elem.propiedades)
            {
                s += "\n" + entry.Key;
                s += ": " + entry.Value;
            }
            return s;
        }

    }
}


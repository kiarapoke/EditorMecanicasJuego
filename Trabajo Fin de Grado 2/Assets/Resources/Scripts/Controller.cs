using UnityEngine;
using System.Collections;
using Elementos;
using System.Collections.Generic;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    private static Controller _instancia = null;
    public static Controller Instancia
    {
        get
        {
            return _instancia;
        }
    }

    public static List<Personaje> Personajes = new List<Personaje>();
    public static List<NPC> NPCs = new List<NPC>();
    public static List<Objeto> Objetos = new List<Objeto>();
    public static List<Accion> Acciones = new List<Accion>();
    public static List<Regla> Reglas = new List<Regla>();

    public static Jugador Player;
    public static List<Jugador> Jugadores = new List<Jugador>();
    public static List<Enemigo> Enemigos = new List<Enemigo>();
    public static List<NPCEnMapa> NPCsEnMapa = new List<NPCEnMapa>();
    public static List<Obstaculo> Obstaculos = new List<Obstaculo>();
    public static List<Comida> Comidas = new List<Comida>();
    public static List<Dialogo> Dialogos = new List<Dialogo>();

    public static ReglasManager reglasMan = new ReglasManager();

    // Use this for initialization
    void Start()
    {
        _instancia = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Elemento buscarPorID(List<Personaje> lista, int ID)
    {
        foreach (Elemento elem in lista)
        {
            if (elem.ID == ID) return elem;
        }
        return new Personaje("", -1);
    }

    public static Elemento buscarPorID(List<NPC> lista, int ID)
    {
        foreach (Elemento elem in lista)
        {
            if (elem.ID == ID) return elem;
        }
        return new NPC("", -1);
    }

    public static Elemento buscarPorID(List<Objeto> lista, int ID)
    {
        foreach (Elemento elem in lista)
        {
            if (elem.ID == ID) return elem;
        }
        return new Objeto("", -1);
    }

    public static Elemento buscarPorID(List<Accion> lista, int ID)
    {
        foreach (Elemento elem in lista)
        {
            if (elem.ID == ID) return elem;
        }
        return new Accion("", -1);
    }

    public static void morir(Elemento elem)
    {
        ElementosJuego aux = buscarElemJuego(elem);
        if (aux.modo == 1) return;
        if (aux.reaparece == "random")
        {
            aux.posicion.x = UnityEngine.Random.Range(0.5f, 9.5f);
            aux.posicion.y = UnityEngine.Random.Range(0.5f, 9.5f);
            aux.GO.transform.localPosition = aux.posicion3D();
        }
        else if (aux.reaparece == "si")
        {
            aux.GO.transform.localPosition = aux.posicion3D();
        }
        else
        {
            Destroy(buscarElemJuego(elem).GO);
        }
    }

    public static void modoB(Elemento elem)
    {
        ElementosJuego aux = buscarElemJuego(elem);
        aux.modo = 1;
    }

    public static bool estaBerserker(Elemento elem)
    {
        if (buscarElemJuego(elem).modo == 1) return true;
        return false;
    }

    public static bool tiene(Elemento elem, Elemento objeto)
    {
        if (buscarElemJuego(elem).inventario.Contains(objeto)) return true;
        return false;
    }

    public static void coger(Elemento elem, Elemento objeto)
    {
        buscarElemJuego(elem).inventario.Add(objeto);
        Destroy(buscarElemJuego(objeto).GO);
    }

    public static void crecer(Elemento elem)
    {
        ElementosJuego aux = buscarElemJuego(elem);
        aux.crecer = 1;
    }

    public static void mostrarDialogo(Elemento elem)
    {
        if (elem.propiedades["pulsable"] == "no")
        {
            GameObject.Find("TextContainer").GetComponentInChildren<Text>().text = elem.propiedades["texto"];
            GameObject.Find("TextContainer").GetComponent<TextContainer>().dialogo = elem;
        }
        else if (elem.propiedades["pulsable"] == "si")
        {
            GameObject.Find("TextContainer").GetComponentInChildren<Text>().text = " ";
            GameObject.Find("TextContainer").GetComponentInChildren<TextContainer>().dialogo = null;
        }
    }

    public static ElementosJuego buscarElemJuego(Elemento elem)
    {
        if (Player.elemento == elem) return Player;
        foreach (Jugador aux in Jugadores)
        {
            if (aux.elemento == elem) return aux;
        }
        foreach (Enemigo aux in Enemigos)
        {
            if (aux.elemento == elem) return aux;
        }
        foreach (Obstaculo aux in Obstaculos)
        {
            if (aux.elemento == elem) return aux;
        }
        foreach (Comida aux in Comidas)
        {
            if (aux.elemento == elem) return aux;
        }
        foreach (NPCEnMapa aux in NPCsEnMapa)
        {
            if (aux.elemento == elem) return aux;
        }
        return new Jugador(new Personaje("", -1), new Vector2(0,0));
    }

}

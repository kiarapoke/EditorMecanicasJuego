using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Elementos;
using System;

public class Inicializador : MonoBehaviour {

    public TextAsset BaseDeDatosXML;
    public GameObject player;
    public GameObject npc;
    public GameObject enemy;
    public GameObject obstacle;
    public GameObject food;
    public GameObject escenario;

	// Use this for initialization
	void Start () {
        leerXML("prueba");
        buscarJugadores();
        buscarNPCs();
        buscarObjetos();
        instanciarPosiciones();
        inicializarReglas();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void leerXML(string xml)
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.LoadXml(BaseDeDatosXML.text); // load the file.
        string nombre = "";
        string ID = "";
        Dictionary<string, string> propiedades;

        XmlNodeList personajes = xmlDoc.GetElementsByTagName("Personaje"); // Personajes
        foreach (XmlNode personaje in personajes)
        {
            Personaje aux;
            nombre = "";
            ID = "";
            propiedades = new Dictionary<string, string>();
            XmlNodeList elementos = personaje.ChildNodes;
            foreach (XmlNode elemento in elementos)
            {
                if (elemento.Name == "Nombre")
                {
                    nombre = elemento.InnerText.ToLower();
                }
                if (elemento.Name == "ID")
                {
                    ID = elemento.InnerText;
                }
                if (elemento.Name == "Propiedades")
                {
                    foreach (XmlNode propiedad in elemento)
                    {
                        propiedades[propiedad.Name] = propiedad.InnerText.ToLower();
                    }
                }
            }
            aux = new Personaje(nombre, int.Parse(ID), propiedades);
            Controller.Personajes.Add(aux);
        } // /Personajes

        XmlNodeList npcs = xmlDoc.GetElementsByTagName("NPC"); // NPCs
        foreach (XmlNode npc in npcs)
        {
            NPC aux;
            nombre = "";
            ID = "";
            propiedades = new Dictionary<string, string>();
            XmlNodeList elementos = npc.ChildNodes;
            foreach (XmlNode elemento in elementos)
            {
                if (elemento.Name == "Nombre")
                {
                    nombre = elemento.InnerText.ToLower();
                }
                if (elemento.Name == "ID")
                {
                    ID = elemento.InnerText;
                }
                if (elemento.Name == "Propiedades")
                {
                    foreach (XmlNode propiedad in elemento)
                    {
                        propiedades[propiedad.Name] = propiedad.InnerText.ToLower();
                    }
                }
            }
            aux = new NPC(nombre, int.Parse(ID), propiedades);
            Controller.NPCs.Add(aux);
        } // /NPCs

        XmlNodeList objetos = xmlDoc.GetElementsByTagName("Objeto"); // Objetos
        foreach (XmlNode objeto in objetos)
        {
            Objeto aux;
            nombre = "";
            ID = "";
            propiedades = new Dictionary<string, string>();
            XmlNodeList elementos = objeto.ChildNodes;
            foreach (XmlNode elemento in elementos)
            {
                if (elemento.Name == "Nombre")
                {
                    nombre = elemento.InnerText.ToLower();
                }
                if (elemento.Name == "ID")
                {
                    ID = elemento.InnerText;
                }
                if (elemento.Name == "Propiedades")
                {
                    foreach (XmlNode propiedad in elemento)
                    {
                        if (propiedad.Name != "texto") propiedades[propiedad.Name] = propiedad.InnerText.ToLower();
                        else propiedades[propiedad.Name] = propiedad.InnerText;
                    }
                }
            }
            aux = new Objeto(nombre, int.Parse(ID), propiedades);
            Controller.Objetos.Add(aux);
        } // /Objetos

        XmlNodeList acciones = xmlDoc.GetElementsByTagName("Accion"); // Acciones
        foreach (XmlNode accion in acciones)
        {
            Accion aux;
            nombre = "";
            ID = "";
            propiedades = new Dictionary<string, string>();
            XmlNodeList elementos = accion.ChildNodes;
            foreach (XmlNode elemento in elementos)
            {
                if (elemento.Name == "Nombre")
                {
                    nombre = elemento.InnerText.ToLower();
                }
                if (elemento.Name == "ID")
                {
                    ID = elemento.InnerText;
                }
                if (elemento.Name == "Propiedades")
                {
                    foreach (XmlNode propiedad in elemento)
                    {
                        propiedades[propiedad.Name] = propiedad.InnerText.ToLower();
                    }
                }
            }
            aux = new Accion(nombre, int.Parse(ID), propiedades);
            Controller.Acciones.Add(aux);
        } // /Acciones

        List<List<Elemento>> antecedentes;
        List<Elemento> antecedente;
        List<Elemento> consecuente;
        string IDElem = "";
        string tipo = "";

        XmlNodeList reglas = xmlDoc.GetElementsByTagName("Regla"); // Reglas
        foreach (XmlNode regla in reglas)
        {
            Regla aux;
            antecedentes = new List<List<Elemento>>();
            consecuente = new List<Elemento>();

            XmlNodeList elementos = regla.ChildNodes;
            foreach (XmlNode elemento in elementos)
            {
                if (elemento.Name == "Antecedentes")
                {
                    foreach (XmlNode antec in elemento)
                    {
                        antecedente = new List<Elemento>();
                        foreach (XmlNode elem in antec)
                        {
                            foreach (XmlNode propElem in elem)
                            {
                                if (propElem.Name == "ID")
                                {
                                    IDElem = propElem.InnerText;
                                }
                                if (propElem.Name == "Tipo")
                                {
                                    tipo = propElem.InnerText;
                                }
                            }
                            antecedente.Add(buscarElemento(int.Parse(IDElem), tipo));
                        }
                        antecedentes.Add(antecedente);
                    }
                }
                if (elemento.Name == "Consecuente")
                {
                    foreach (XmlNode elem in elemento)
                    {
                        foreach (XmlNode propElem in elem)
                        {
                            if (propElem.Name == "ID")
                            {
                                IDElem = propElem.InnerText;
                            }
                            if (propElem.Name == "Tipo")
                            {
                                tipo = propElem.InnerText;
                            }
                        }
                        consecuente.Add(buscarElemento(int.Parse(IDElem), tipo));
                    }
                }
            }
            aux = new Regla(antecedentes, consecuente);
            Controller.Reglas.Add(aux);
        } // /Reglas
    }

    Elemento buscarElemento(int ID, string tipo)
    {
        Elemento elem = new Personaje("", -1);
        switch (tipo)
        {
            case "Personaje":
                elem = Controller.buscarPorID(Controller.Personajes, ID);
                break;
            case "NPC":
                elem = Controller.buscarPorID(Controller.NPCs, ID);
                break;
            case "Objeto":
                elem = Controller.buscarPorID(Controller.Objetos, ID);
                break;
            case "Accion":
                elem = Controller.buscarPorID(Controller.Acciones, ID);
                break;
        }
        return elem;
    }

    void buscarJugadores()
    {
        float x = 0f;
        float y = 0f;
        foreach (Personaje personaje in Controller.Personajes)
        {
            x = 0f; y = 0f;
            if (personaje.propiedades.ContainsKey("posicionx") && personaje.propiedades["posicionx"] == "aleatoria") x = randomPos();
            else if (personaje.propiedades.ContainsKey("posicionx")) x = float.Parse(personaje.propiedades["posicionx"]);
            if (personaje.propiedades.ContainsKey("posiciony") && personaje.propiedades["posiciony"] == "aleatoria") y = randomPos();
            else if (personaje.propiedades.ContainsKey("posiciony")) y = float.Parse(personaje.propiedades["posiciony"]);
            if (personaje.propiedades.ContainsKey("rol") && personaje.propiedades["rol"] == "protagonista" && personaje.propiedades.ContainsKey("posicionx"))
            {
                Controller.Player = new Jugador(personaje, new Vector2(x, y));
                if (personaje.propiedades.ContainsKey("movimiento")) Controller.Player.movimiento = personaje.propiedades["movimiento"];
                if (personaje.propiedades.ContainsKey("color")) Controller.Player.color = stringToColor(personaje.propiedades["color"]);
                if (personaje.propiedades.ContainsKey("cola")) Controller.Player.cola = personaje.propiedades["cola"];
            }
            else if (personaje.propiedades.ContainsKey("posicionx"))
            {
                Controller.Jugadores.Add(new Jugador(personaje, new Vector2(x, y)));
                if (personaje.propiedades.ContainsKey("movimiento")) Controller.Jugadores[Controller.Jugadores.Count-1].movimiento = personaje.propiedades["movimiento"];
                if (personaje.propiedades.ContainsKey("color")) Controller.Player.color = stringToColor(personaje.propiedades["color"]);
            }
        }
    }

    void buscarNPCs()
    {
        float x = 0f;
        float y = 0f;
        foreach (NPC npc in Controller.NPCs)
        {

            x = 0f; y = 0f;
            if (npc.propiedades.ContainsKey("posicionx") && npc.propiedades["posicionx"] == "aleatoria") x = randomPos();
            else if (npc.propiedades.ContainsKey("posicionx")) x = float.Parse(npc.propiedades["posicionx"]);
            if (npc.propiedades.ContainsKey("posiciony") && npc.propiedades["posiciony"] == "aleatoria") y = randomPos();
            else if (npc.propiedades.ContainsKey("posiciony")) y = float.Parse(npc.propiedades["posiciony"]);
            if (npc.propiedades.ContainsKey("rol") && npc.propiedades["rol"] == "enemigo" && npc.propiedades.ContainsKey("posicionx"))
            {
                Controller.Enemigos.Add(new Enemigo(npc, new Vector2(x, y)));
                if (npc.propiedades.ContainsKey("movimiento")) Controller.Enemigos[Controller.Enemigos.Count - 1].movimiento = npc.propiedades["movimiento"];
                if (npc.propiedades.ContainsKey("color")) Controller.Enemigos[Controller.Enemigos.Count - 1].color = stringToColor(npc.propiedades["color"]);
                if (npc.propiedades.ContainsKey("cola")) Controller.Enemigos[Controller.Enemigos.Count - 1].cola = npc.propiedades["cola"];
            }
            else if (npc.propiedades.ContainsKey("posicionx"))
            {
                Controller.NPCsEnMapa.Add(new NPCEnMapa(npc, new Vector2(x, y)));
                if (npc.propiedades.ContainsKey("movimiento")) Controller.NPCsEnMapa[Controller.NPCsEnMapa.Count - 1].movimiento = npc.propiedades["movimiento"];
                if (npc.propiedades.ContainsKey("color")) Controller.NPCsEnMapa[Controller.NPCsEnMapa.Count - 1].color = stringToColor(npc.propiedades["color"]);
            }
        }
    }

    void buscarObjetos()
    {
        float x = 0f;
        float y = 0f;
        foreach (Objeto obj in Controller.Objetos)
        {
            x = 0f; y = 0f;
            if (obj.propiedades.ContainsKey("posicionx") && obj.propiedades["posicionx"] == "aleatoria") x = randomPos();
            else if (obj.propiedades.ContainsKey("posicionx")) x = float.Parse(obj.propiedades["posicionx"]);
            if (obj.propiedades.ContainsKey("posiciony") && obj.propiedades["posiciony"] == "aleatoria") y = randomPos();
            else if (obj.propiedades.ContainsKey("posiciony")) y = float.Parse(obj.propiedades["posiciony"]);
            if (obj.propiedades.ContainsKey("posicionx") && obj.propiedades.ContainsKey("rol") && obj.propiedades["rol"] == "obstaculo")
            {
                Controller.Obstaculos.Add(new Obstaculo(obj, new Vector2(x, y)));
                if (obj.propiedades.ContainsKey("movimiento")) Controller.Obstaculos[Controller.Obstaculos.Count - 1].movimiento = obj.propiedades["movimiento"];
                if (obj.propiedades.ContainsKey("color")) Controller.Obstaculos[Controller.Obstaculos.Count - 1].color = stringToColor(obj.propiedades["color"]);
            }
            else if (obj.propiedades.ContainsKey("posicionx") && obj.propiedades.ContainsKey("rol") && obj.propiedades["rol"] == "comida")
            {
                Controller.Comidas.Add(new Comida(obj, new Vector2(x, y)));
                if (obj.propiedades.ContainsKey("movimiento")) Controller.Comidas[Controller.Comidas.Count - 1].movimiento = obj.propiedades["movimiento"];
                if (obj.propiedades.ContainsKey("reaparece")) Controller.Comidas[Controller.Comidas.Count - 1].reaparece = obj.propiedades["reaparece"];
                if (obj.propiedades.ContainsKey("color")) Controller.Comidas[Controller.Comidas.Count - 1].color = stringToColor(obj.propiedades["color"]);
            }
            else if (obj.propiedades.ContainsKey("rol") && obj.propiedades["rol"] == "dialogo")
            {
                Controller.Dialogos.Add(new Dialogo(obj));
                if (obj.propiedades.ContainsKey("posicionx")) Controller.Dialogos[Controller.Dialogos.Count - 1].posicion = float.Parse(obj.propiedades["posicionx"]);
                if (obj.propiedades.ContainsKey("pulsable") && obj.propiedades["pulsable"] == "si") Controller.Dialogos[Controller.Dialogos.Count - 1].pulsable = true;
            }
        }
    }

    void instanciarPosiciones()
    {
        //Player
        Controller.Player.GO = (GameObject)Instantiate(player, new Vector3(0,0,0), player.transform.rotation);
        Controller.Player.GO.name = "Player";
        Controller.Player.GO.transform.parent = escenario.transform;
        Controller.Player.GO.transform.localPosition = Controller.Player.posicion3D();
        Controller.Player.changeColor();

        //NPC
        foreach (NPCEnMapa np in Controller.NPCsEnMapa)
        {
            np.GO = (GameObject)Instantiate(npc, new Vector3(0, 0, 0), npc.transform.rotation);
            np.GO.name = "Ally";
            np.GO.transform.parent = escenario.transform;
            np.GO.transform.localPosition = np.posicion3D();
            np.GO.GetComponent<Ally>().npc = np;
            np.changeColor();
        }

        //Enemies
        foreach (Enemigo enem in Controller.Enemigos)
        {
            enem.GO = (GameObject)Instantiate(enemy, new Vector3(0, 0, 0), enemy.transform.rotation);
            enem.GO.name = "Enemy";
            enem.GO.transform.parent = escenario.transform;
            enem.GO.transform.localPosition = enem.posicion3D();
            enem.GO.GetComponent<Enemy>().enemigo = enem;
            enem.changeColor();
        }

        //Obstacles
        foreach (Obstaculo obj in Controller.Obstaculos)
        {
            obj.GO = (GameObject)Instantiate(obstacle, new Vector3(0, 0, 0), obstacle.transform.rotation);
            obj.GO.name = "Obstaculo";
            obj.GO.transform.parent = escenario.transform;
            obj.GO.transform.localPosition = obj.posicion3D();
            obj.changeColor();
        }

        //Food
        foreach (Comida obj in Controller.Comidas)
        {
            obj.GO = (GameObject)Instantiate(food, new Vector3(0, 0, 0), food.transform.rotation);
            obj.GO.name = "Comida";
            obj.GO.transform.parent = escenario.transform;
            obj.GO.transform.localPosition = obj.posicion3D();
            obj.GO.GetComponent<Food>().comida = obj;
            obj.changeColor();
        }
    }

    public float randomPos()
    {
        return UnityEngine.Random.Range(0.5F, 9.5F);
    }

    public Color stringToColor(string col)
    {
        switch (col)
        {
            case "black":
            case "negro":
                return Color.black;
            case "blue":
            case "azul":
                return Color.blue;
            case "gray":
            case "gris":
                return Color.gray;
            case "green":
            case "verde":
                return Color.green;
            case "red":
            case "rojo":
                return Color.red;
            case "white":
            case "blanco":
                return Color.white;
            case "yellow":
            case "amarillo":
                return Color.yellow;
            case "naranja":
            case "orange":
                return new Color(255f / 255f, 128f / 255f, 0f / 255f);
            case "rosa":
            case "pink":
                return new Color(255f / 255f, 153 / 255f, 153f / 255f);
        }
        return Color.clear;
    }

    public void inicializarReglas()
    {
        foreach (Regla regla in Controller.Reglas)
        {
            foreach (List<Elemento> antecedente in regla.antecedentes)
            {
                List<List<Elemento>> antecedentes2 = new List<List<Elemento>>();
                antecedentes2.Add(antecedente);
                foreach (Elemento elemento in antecedente)
                {
                    if (elemento.nombre == "chocar")
                    {
                        if (!Controller.reglasMan.chocar.ContainsKey(new KeyValuePair<Elemento, Elemento>(antecedente[antecedente.IndexOf(elemento) - 1], antecedente[antecedente.IndexOf(elemento) + 1])))
                        {
                            Controller.reglasMan.chocar[new KeyValuePair<Elemento, Elemento>(antecedente[antecedente.IndexOf(elemento) - 1], antecedente[antecedente.IndexOf(elemento) + 1])] = new Dictionary<List<List<Elemento>>, List<List<Elemento>>>();
                        }
                        foreach (List<Elemento> masAntecedentes in regla.antecedentes)
                        {
                            if (masAntecedentes != antecedente)
                            {
                                antecedentes2.Add(masAntecedentes);
                            }
                        }

                        if (!(Controller.reglasMan.chocar[new KeyValuePair<Elemento, Elemento>(antecedente[antecedente.IndexOf(elemento) - 1], antecedente[antecedente.IndexOf(elemento) + 1])]).ContainsKey(antecedentes2))
                        {
                            Controller.reglasMan.chocar[new KeyValuePair<Elemento, Elemento>(antecedente[antecedente.IndexOf(elemento) - 1], antecedente[antecedente.IndexOf(elemento) + 1])][antecedentes2] = new List<List<Elemento>>();
                        }
                        Controller.reglasMan.chocar[new KeyValuePair<Elemento, Elemento>(antecedente[antecedente.IndexOf(elemento) - 1], antecedente[antecedente.IndexOf(elemento) + 1])][antecedentes2].Add(regla.consecuente);
                    }
                    if (elemento.nombre == "terminar")
                    {
                        if (!Controller.reglasMan.terminar.ContainsKey(antecedente[antecedente.IndexOf(elemento) + 1])) Controller.reglasMan.terminar[antecedente[antecedente.IndexOf(elemento) + 1]] = new List<List<Elemento>>();
                        Controller.reglasMan.terminar[antecedente[antecedente.IndexOf(elemento) + 1]].Add(regla.consecuente);
                    }

                    if (elemento.nombre == "pulsar")
                    {
                        if (!Controller.reglasMan.pulsar.ContainsKey(antecedente[antecedente.IndexOf(elemento) + 1])) Controller.reglasMan.pulsar[antecedente[antecedente.IndexOf(elemento) + 1]] = new List<List<Elemento>>();
                        Controller.reglasMan.pulsar[antecedente[antecedente.IndexOf(elemento) + 1]].Add(regla.consecuente);
                    }
                }
            }
        }
    }
}

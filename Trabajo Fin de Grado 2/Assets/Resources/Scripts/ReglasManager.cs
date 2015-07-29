using UnityEngine;
using System.Collections;
using Elementos;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ReglasManager : MonoBehaviour {

    public Dictionary<KeyValuePair<Elemento, Elemento>, Dictionary<List<List<Elemento>>, List<List<Elemento>>>> chocar = new Dictionary<KeyValuePair<Elemento, Elemento>, Dictionary<List<List<Elemento>>, List<List<Elemento>>>>(); //Pareja de elementos - Pareja de mas antecedentes y cosas que provoca

    public Dictionary<Elemento, List<List<Elemento>>> terminar = new Dictionary<Elemento, List<List<Elemento>>>(); //Elemento - cosas que provoca

    public Dictionary<Elemento, List<List<Elemento>>> pulsar = new Dictionary<Elemento, List<List<Elemento>>>(); //Elemento - cosas que provoca

    public GameObject pfBoton;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onChoca(ElementosJuego o1, ElementosJuego o2)
    {
        foreach (KeyValuePair<KeyValuePair<Elemento, Elemento>, Dictionary<List<List<Elemento>>, List<List<Elemento>>>> par in chocar)
        {
            if (par.Key.Key == o1.elemento && par.Key.Value == o2.elemento)
            {
                foreach (List<List<Elemento>> antecedentes in par.Value.Keys)
                {
                    bool seCumple = true;
                    foreach (List<Elemento> antecedente in antecedentes)
                    {
                        foreach (Elemento elem in antecedente)
                        {
                            if (elem.nombre == "berserker")
                            {
                                seCumple = Controller.estaBerserker(antecedente[antecedente.IndexOf(elem) - 1]);
                            }
                            if (elem.nombre == "tener")
                            {
                                seCumple = Controller.tiene(antecedente[antecedente.IndexOf(elem) - 1], antecedente[antecedente.IndexOf(elem) + 1]);
                            }
                        }
                    }
                    if (!seCumple) continue;
                    foreach (List<Elemento> consecuente in par.Value[antecedentes])
                    {

                        foreach (Elemento elem in consecuente)
                        {
                            if (elem.nombre == "morir")
                            {
                                Controller.morir(consecuente[consecuente.IndexOf(elem) - 1]);
                            }
                            if (elem.nombre == "coger")
                            {
                                Controller.coger(consecuente[consecuente.IndexOf(elem) - 1], consecuente[consecuente.IndexOf(elem) + 1]);
                            }
                            if (elem.nombre == "berserker")
                            {
                                Controller.modoB(consecuente[consecuente.IndexOf(elem) - 1]);
                            }
                            if (elem.nombre == "crecer")
                            {
                                Controller.crecer(consecuente[consecuente.IndexOf(elem) - 1]);
                            }
                            if (elem.nombre == "mostrar")
                            {
                                Controller.mostrarDialogo(consecuente[consecuente.IndexOf(elem) + 1]);
                            }
                        }
                    }
                }

            }
        }
    }

    public void onTermina(Elemento e)
    {
        foreach (KeyValuePair<Elemento, List<List<Elemento>>> par in terminar)
        {
            if (par.Key == e)
            {
                foreach (List<Elemento> consecuente in par.Value)
                {
                    foreach (Elemento elem in consecuente)
                    {
                        if (elem.nombre == "mostrar")
                        {
                            Controller.mostrarDialogo(consecuente[consecuente.IndexOf(elem) + 1]);
                            if (consecuente[consecuente.IndexOf(elem) + 1].propiedades["pulsable"] == "si")
                            {
                                instanciarDialogoPulsable(consecuente[consecuente.IndexOf(elem) + 1]);
                            }
                        }
                    }
                }
            }
        }
    }

    public void instanciarDialogoPulsable(Elemento elem)
    {
        if (pfBoton == null)
        {
            pfBoton = Resources.Load("Prefabs/Boton") as GameObject;
            if (pfBoton == null)
            {
                throw new Exception("Cannot find resource");
            }
        }
        GameObject GO = (GameObject)Instantiate(pfBoton, new Vector3(0, 0, 0), Quaternion.identity);
        GO.transform.parent = GameObject.Find("TextContainer").transform;
        GO.name = elem.nombre;
        GO.GetComponentInChildren<Text>().text = elem.propiedades["texto"];
        GO.transform.localPosition = new Vector3(float.Parse(elem.propiedades["posicionx"]) -100, 0, 0);

        foreach (KeyValuePair<Elemento, List<List<Elemento>>> par in pulsar)
        {
            if (par.Key == elem)
            {
                foreach (List<Elemento> consecuente in par.Value)
                {
                    foreach (Elemento el in consecuente)
                    {
                        if(el.nombre == "mostrar")
                        {
                            addListener(GO, el, consecuente);
                        }
                    }
                }
            }
        }
    }

    private void addListener(GameObject GO, Elemento el, List<Elemento> consecuente)
    {
        GO.GetComponent<Button>().onClick.AddListener(delegate {
            foreach (Transform child in GO.transform.parent)
            {
                if (child.gameObject.GetComponent<Button>() != null) Destroy(child.gameObject);
            }
            Controller.mostrarDialogo(consecuente[consecuente.IndexOf(el) + 1]); 
        });
    }
}

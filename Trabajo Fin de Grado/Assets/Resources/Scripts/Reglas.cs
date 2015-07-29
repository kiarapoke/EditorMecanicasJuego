using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Elementos;
using UnityEngine.UI;

public class Reglas : MonoBehaviour {

    private int cantidadA = 0;
    private int cantidadC = 0;
    private List<List<Elemento>> antecedentes = new List<List<Elemento>>();
    private List<Elemento> antecedenteActual = new List<Elemento>();
    private List<Elemento> consecuenteActual = new List<Elemento>();
    private List<List<Elemento>> consecuente = new List<List<Elemento>>();

    public GameObject txtAntec = null;
    public GameObject txtConsec = null;
    public GameObject panelAntec = null;
    public GameObject panelConsec = null;
    public GameObject reglaABorrar = null;
	// Use this for initialization
	void Start () {
        
	}

	
	// Update is called once per frame
	void Update () {
	
	}

    public void agregarAntec(Elemento elem)
    {
        if (cantidadA < 4)
        {
            cantidadA = cantidadA + 1;
            antecedenteActual.Add(elem);
            actualizarTxtAntec();
        }
        else
        {
            Debug.Log("Error, no se pueden mas de 4 elementos!");
        }
    }

    public void agregarConsec(Elemento elem)
    {
        if (cantidadC < 4)
        {
            cantidadC = cantidadC + 1;
            consecuenteActual.Add(elem);
            actualizarTxtConsec();
        }
        else
        {
            Debug.Log("Error, no se pueden mas de 4 elementos!");
        }
    }

    public void actualizarTxtAntec(){
        txtAntec.GetComponent<Text>().text = listaAString(antecedenteActual);
    }

    public void actualizarTxtConsec()
    {
        txtConsec.GetComponent<Text>().text = listaAString(consecuenteActual);
    }

    public void actualizarPanelAntec()
    {
        string s = "";
        foreach (List<Elemento> lelem in antecedentes)
        {
            s += listaAString(lelem);
            s += "\n";
        }
        panelAntec.GetComponent<Text>().text = s;
    }

    public void actualizarPanelConsec()
    {
        string s = "";
        foreach (List<Elemento> lelem in consecuente)
        {
            s += listaAString(lelem);
            s += "\n";
        }
        panelConsec.GetComponent<Text>().text = s;
    }

    public void addAntecedente()
    {
        if (antecedenteActual.Count >= 1)
        {
            antecedentes.Add(antecedenteActual);
            antecedenteActual = new List<Elemento>();
            actualizarTxtAntec();
            actualizarPanelAntec();
            cantidadA = 0;
        }
        
    }
    
    public void addConsecuente()
    {
        if (consecuente.Count < 1 && consecuenteActual.Count >= 1)
        {
            consecuente.Add(consecuenteActual);
            consecuenteActual = new List<Elemento>();
            actualizarTxtConsec();
            actualizarPanelConsec();
            cantidadC = 0;
        }
        
    }

    public void agregarRegla()
    {
        if (antecedentes.Count > 0 && consecuente.Count > 0)
        {
            UIController.Lista.addRegla(antecedentes, consecuente[0]);
            consecuente.Clear();
            antecedentes.Clear();
            consecuenteActual.Clear();
            antecedenteActual.Clear();
            actualizarTxtAntec();
            actualizarTxtConsec();
            actualizarPanelAntec();
            actualizarPanelConsec();
        }      
    }

    public void borrarRegla()
    {
        string s = reglaABorrar.GetComponent<Text>().text;
        if (s != "")
        {
            UIController.Lista.removeRegla(int.Parse(s));
        }
    }

    public void cerrar()
    {
        this.transform.position = new Vector3(2000, this.transform.position.y, 0);
    }

    public void abrir()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, 0);
    }

    private string listaAString(List<Elemento> lista)
    {
        string s = "";
        foreach (Elemento elem in lista)
        {
            s += elem.nombre + " ";
        }
        return s;
    }
}

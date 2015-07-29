using UnityEngine;
using System.Collections;
using ListaC;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private static UIController _instancia = null;
    public static UIController Instancia
    {
        get
        {
            return _instancia;
        }
    }

    public static ListaClass Lista = new ListaClass();
    private static int id = 0;
    private string nombre;

	// Use this for initialization
	void Start () {
        _instancia = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPersonaje() 
    {
        nombre = GameObject.Find("InputCharacter").GetComponentInChildren<Text>().text;
        if (nombre != null && nombre != "")
        {
            Lista.addPersonaje(nombre, id);
            id++;
            GameObject.Find("InputCharacter").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void addNPC()
    {
        nombre = GameObject.Find("InputNPC").GetComponentInChildren<Text>().text;
        if (nombre != null && nombre != "")
        {
            Lista.addNPC(nombre, id);
            id++;
            GameObject.Find("InputNPC").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void addObjeto()
    {
        nombre = GameObject.Find("InputItem").GetComponentInChildren<Text>().text;
        if (nombre != null && nombre != "")
        {
            Lista.addObjeto(nombre, id);
            id++;
            GameObject.Find("InputItem").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void addAccion()
    {
        nombre = GameObject.Find("InputAction").GetComponentInChildren<Text>().text;
        if (nombre != null && nombre != "")
        {
            Lista.addAccion(nombre, id);
            id++;
            GameObject.Find("InputAction").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void borrarPersonaje()
    {
        string idS = GameObject.Find("InputID").GetComponentInChildren<Text>().text;
        if (idS != null && idS != "")
        {
            Lista.removePersonaje(int.Parse(idS));
            GameObject.Find("InputID").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void borrarNPC()
    {
        string idS = GameObject.Find("InputID").GetComponentInChildren<Text>().text;
        if (idS != null && idS != "")
        {
            Lista.removeNPC(int.Parse(idS));
            GameObject.Find("InputID").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void borrarAccion()
    {
        string idS = GameObject.Find("InputID").GetComponentInChildren<Text>().text;
        if (idS != null && idS != "")
        {
            Lista.removeAccion(int.Parse(idS));
            GameObject.Find("InputID").GetComponentInChildren<InputField>().text = "";
        }
    }

    public void borrarObjeto()
    {
        string idS = GameObject.Find("InputID").GetComponentInChildren<Text>().text;
        if (idS != null && idS != "")
        {
            Lista.removeObjeto(int.Parse(idS));
            GameObject.Find("InputID").GetComponentInChildren<InputField>().text = "";
        }
    }

}

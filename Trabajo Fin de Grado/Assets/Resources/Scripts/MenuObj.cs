using UnityEngine;
using System.Collections;
using ListaC;
using UnityEngine.UI;
using Elementos;

public class MenuObj : MonoBehaviour {

    public GameObject reglasManager = null;
    public Transform menuPanel;
    public GameObject buttonPrefab;
    public Objeto objActual = null;
    public GameObject clave = null;
    public GameObject valor = null;
    public GameObject panel = null;
    public GameObject panelEscribir = null;
    public bool esRegla = false;
    public bool esAntecedente = false;

	// Use this for initialization
	void Start () {

        actualizar();

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MostrarOcultar(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
            panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -20, panel.transform.localPosition.z);
        }
        else panel.SetActive(true);
    }

    public void add()
    {
        if (objActual != null && clave.GetComponent<InputField>().text != "" && valor.GetComponent<InputField>().text != "")
        {
            objActual.addPropiedad(clave.GetComponent<InputField>().text, valor.GetComponent<InputField>().text);
            panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(objActual);
        }
    }

    public void setObjAct(Objeto oa)
    {
        MostrarOcultar(panel);
        objActual = oa;
        if (!esRegla) panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(objActual);
        else if (esRegla)
        {
            if (esAntecedente)
            {
                reglasManager.GetComponent<Reglas>().agregarAntec(oa);
            }
            else
            {
                reglasManager.GetComponent<Reglas>().agregarConsec(oa);
            }
        }
    }

    public void cerrar()
    {
        this.transform.position = new Vector3(1000, this.transform.position.y, 0);
    }

    public void abrir()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, 0);
        actualizar();
    }

    public void actualizar()
    {
        foreach (Transform child in panel.transform) Destroy(child.gameObject);
        for (int i = 0; i < UIController.Lista.getNumObjetos(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getObjeto(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { setObjAct(UIController.Lista.getObjetoByName(button.GetComponentInChildren<Text>().text)); }
            );
            button.transform.parent = menuPanel;
        }
    }
}

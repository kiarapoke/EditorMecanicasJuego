using UnityEngine;
using System.Collections;
using ListaC;
using UnityEngine.UI;
using Elementos;

public class MenuAcc : MonoBehaviour {

    public GameObject reglasManager = null;
    public Transform menuPanel;
    public GameObject buttonPrefab;
    public Accion AccionActual = null;
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
        if (AccionActual != null && clave.GetComponent<InputField>().text != "" && valor.GetComponent<InputField>().text != "")
        {
            AccionActual.addPropiedad(clave.GetComponent<InputField>().text, valor.GetComponent<InputField>().text);
            panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(AccionActual);
        }
    }

    public void setAccAct(Accion aa)
    {
        MostrarOcultar(panel);
        AccionActual = aa;
        if (!esRegla) panelEscribir.GetComponentInChildren<Text>().text = UIController.Lista.PropiedadesToString(AccionActual);
        else if (esRegla)
        {
            if (esAntecedente)
            {
                reglasManager.GetComponent<Reglas>().agregarAntec(aa);
            }
            else
            {
                reglasManager.GetComponent<Reglas>().agregarConsec(aa);
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
        for (int i = 0; i < UIController.Lista.getNumAcciones(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getAccion(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { setAccAct(UIController.Lista.getAccionByName(button.GetComponentInChildren<Text>().text)); }
            );
            button.transform.parent = menuPanel;
        }
    }
}

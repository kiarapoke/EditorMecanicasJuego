using UnityEngine;
using System.Collections;
using ListaC;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public Transform menuPanel;
    public Transform menuPanel2;
    public Transform menuPanel3;
    public GameObject buttonPrefab;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < UIController.Lista.getNumPersonajes(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getPersonaje(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { Debug.Log(button.GetComponentInChildren<Text>().text); }
            );
            button.transform.parent = menuPanel;
        }
        for (int i = 0; i < UIController.Lista.getNumAcciones(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getAccion(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { Debug.Log(button.GetComponentInChildren<Text>().text); }
            );
            button.transform.parent = menuPanel2;
        }
        for (int i = 0; i < UIController.Lista.getNumObjetos(); i++)
        {
            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.GetComponentInChildren<Text>().text = UIController.Lista.getObjeto(i).nombre;
            int index = i;
            button.GetComponent<Button>().onClick.AddListener(
                () => { Debug.Log(button.GetComponentInChildren<Text>().text); }
            );
            button.transform.parent = menuPanel3;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MostrarOcultar(GameObject panel)
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
            panel.transform.localPosition = new Vector3(panel.transform.localPosition.x, -30, panel.transform.localPosition.z);
        }
        else panel.SetActive(true);
    }
}

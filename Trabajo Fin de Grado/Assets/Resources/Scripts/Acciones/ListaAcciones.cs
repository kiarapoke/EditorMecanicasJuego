using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListaAcciones : MonoBehaviour{

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponentInChildren<Text>().text = UIController.Lista.AccionesToString();
	}
}

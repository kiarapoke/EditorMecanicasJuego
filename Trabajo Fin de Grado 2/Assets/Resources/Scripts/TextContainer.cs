using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Elementos;

public class TextContainer : MonoBehaviour {

    public Elemento dialogo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.gameObject.GetComponentInChildren<Text>().text = "";
            Controller.reglasMan.onTermina(dialogo);
        }
	}
}

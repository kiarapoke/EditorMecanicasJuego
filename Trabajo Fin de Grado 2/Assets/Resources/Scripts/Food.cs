using UnityEngine;
using System.Collections;
using Elementos;

public class Food : MonoBehaviour {

    public Comida comida;
    private Transform padre;
	// Use this for initialization
	void Start () {
        padre = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        
    }
}

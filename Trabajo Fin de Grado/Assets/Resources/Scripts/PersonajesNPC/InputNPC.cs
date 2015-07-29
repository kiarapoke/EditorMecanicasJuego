using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputNPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            UIController.Instancia.addNPC();
        }
    }
}

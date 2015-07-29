using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public void cargarMain()
    {
        Application.LoadLevel("Main");
    }

    public void cargarPersonajes()
    {
        Application.LoadLevel("Personajes");
    }

    public void cargarNPC()
    {
        Application.LoadLevel("PersonajesNPC");
    }

    public void cargarObjetos()
    {
        Application.LoadLevel("Objetos");
    }

    public void cargarAcciones()
    {
        Application.LoadLevel("Acciones");
    }

    public void cargarReglas()
    {
        Application.LoadLevel("Reglas");
    }
}

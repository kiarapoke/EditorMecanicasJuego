using UnityEngine;
using System.Collections;
using Elementos;

public class Player : MonoBehaviour {

    private char direccion = ' ';
    private float speed = 0.2f;
    private int actualAngle = 0;
    private Vector3 pos;
    private bool haComido = false;

    private float nextActionTime = 0.0f;
    private float period = 0.1f;

    private float endOfB = 0f;

    public GameObject pfCola;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion = 'u';
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = 'd';
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = 'l';
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direccion = 'r';
        }

        switch (Controller.Player.movimiento)
        {
            case "normal":
                movimientoNormal();
                break;
            case "giro":
                movimientoGiro();
                break;
            case "no continuo":
                movimientoNoContinuo();
                break;
        }

        if (Controller.Player.modo == 1 && endOfB == 0)
        {
            modoBerserkerOn();
        }

        if (Controller.Player.modo == 1)
        {
            if (Time.time > endOfB)
            {
                modoBerserkerOff();
            }
        }

        if (Controller.Player.crecer == 1)
        {
            crecerCola();
            Controller.Player.crecer = 0;
        }

	}

    public void movimientoNormal()
    {
        pos = this.gameObject.transform.position;
        switch (direccion)
        {
            case 'u':
                this.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                break;
            case 'd':
                this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                break;
            case 'l':
                this.gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                break;
            case 'r':
                this.gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                break;
        }
        if (direccion != ' ')
        {
            transform.Translate(Vector3.forward * speed);
        }
        else
        {
            nextActionTime = Time.time;
        }
        if (!Mathf.Approximately(this.gameObject.transform.position.x, pos.x) || !Mathf.Approximately(this.gameObject.transform.position.z, pos.z))
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                if (Controller.Player.cola == "siempre") Controller.Player.tail.Add(new Cola((GameObject)Instantiate(pfCola, new Vector3(0, 0, 0), pfCola.transform.rotation), new Vector3(0, 0, 0)));
                if (Controller.Player.tail.Count > 0)
                {
                    // Move last Tail Element to where the Head was
                    Controller.Player.tail[Controller.Player.tail.Count - 1].GO.collider.enabled = false;
                    Controller.Player.tail[Controller.Player.tail.Count - 1].posicion = pos;
                    Controller.Player.tail[Controller.Player.tail.Count - 1].actualizarPos();
                    // Add to front of list, remove from the back
                    Controller.Player.tail.Insert(0, Controller.Player.tail[Controller.Player.tail.Count - 1]);
                    Controller.Player.tail.RemoveAt(Controller.Player.tail.Count - 1);
                    if (Controller.Player.tail.Count > 2) Controller.Player.tail[2].GO.collider.enabled = true;
                }
            }
            
        }
    }

    public void movimientoGiro()
    {
        pos = this.gameObject.transform.position;
        switch (direccion)
        {
            case 'l':
                actualAngle -= 90;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.up);
                direccion = '-';
                break;
            case 'r':
                actualAngle += 90;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.up);
                direccion = '-';
                break;
        }
        if (direccion == 'l' || direccion == 'r' || direccion == '-')
        {
            transform.Translate(Vector3.forward * speed);
        }
        else
        {
            nextActionTime = Time.time;
        }
        if (!Mathf.Approximately(this.gameObject.transform.position.x, pos.x) || !Mathf.Approximately(this.gameObject.transform.position.z, pos.z))
        {
            if ((Time.time > nextActionTime))
            {
                nextActionTime += period;
                if (Controller.Player.cola == "siempre")Controller.Player.tail.Add(new Cola((GameObject)Instantiate(pfCola, new Vector3(0, 0, 0), pfCola.transform.rotation), new Vector3(0, 0, 0)));
                if (Controller.Player.tail.Count > 0)
                {
                    // Move last Tail Element to where the Head was
                    Controller.Player.tail[Controller.Player.tail.Count - 1].GO.collider.enabled = false;
                    Controller.Player.tail[Controller.Player.tail.Count - 1].posicion = pos;
                    Controller.Player.tail[Controller.Player.tail.Count - 1].actualizarPos();
                    // Add to front of list, remove from the back
                    Controller.Player.tail.Insert(0, Controller.Player.tail[Controller.Player.tail.Count - 1]);
                    Controller.Player.tail.RemoveAt(Controller.Player.tail.Count - 1);
                    if (Controller.Player.tail.Count > 2) Controller.Player.tail[2].GO.collider.enabled = true;
                }
            }

        }
    }

    public void movimientoNoContinuo()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime * 30;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Escenario")
        {
            if(collision.gameObject.name == "Enemy") Controller.reglasMan.onChoca(Controller.Player, collision.gameObject.GetComponent<Enemy>().enemigo);
            if (collision.gameObject.name == "EnemyTail(Clone)") Controller.reglasMan.onChoca(Controller.Player, Controller.Enemigos[0]);
            if (collision.gameObject.name == "Ally") Controller.reglasMan.onChoca(Controller.Player, collision.gameObject.GetComponent<Ally>().npc);

            direccion = ' ';
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Comida") Controller.reglasMan.onChoca(Controller.Player, other.GetComponent<Food>().comida);
        
    }

    void modoBerserkerOn()
    {
        Controller.Player.GO.transform.localScale = new Vector3(Controller.Player.GO.transform.localScale.x + 0.25f, Controller.Player.GO.transform.localScale.y + 0.25f, Controller.Player.GO.transform.localScale.z + 0.25f);
        Controller.Player.GO.transform.position = new Vector3(Controller.Player.GO.transform.position.x, Controller.Player.GO.transform.position.y + 0.25f, Controller.Player.GO.transform.position.z);
        endOfB = Time.time + 5f;
    }

    void modoBerserkerOff()
    {
        Controller.Player.GO.transform.localScale = new Vector3(Controller.Player.GO.transform.localScale.x - 0.25f, Controller.Player.GO.transform.localScale.y, Controller.Player.GO.transform.localScale.z - 0.25f);
        endOfB = 0f;
        Controller.Player.modo = 0;
    }

    void crecerCola()
    {
        Controller.Player.tail.Add(new Cola((GameObject)Instantiate(pfCola, new Vector3(0, 0, 0), pfCola.transform.rotation), new Vector3(0, 0, 0)));
    }

}

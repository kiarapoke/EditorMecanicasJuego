using UnityEngine;
using System.Collections;
using Elementos;

public class Enemy : MonoBehaviour {

    private int direccion = 0;
    private float speed = 0.15f;
    private int actualAngle = 0;
    private float nextActionTime2 = 0.0f;
    private float period2 = 0.1f;
    public Enemigo enemigo;
    private float nextActionTime = 0.0f;
    private float period = 0.1f;
    private Vector3 pos;
    private bool haComido = false;

    public GameObject pfCola;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        pos = this.gameObject.transform.position;
        transform.Translate(Vector3.forward * speed);
        if (enemigo.crecer == 1)
        {
            crecerCola();
            enemigo.crecer = 0;
        }
        if (!Mathf.Approximately(this.gameObject.transform.position.x, pos.x) || !Mathf.Approximately(this.gameObject.transform.position.z, pos.z))
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                if (enemigo.cola == "siempre") enemigo.tail.Add(new Cola((GameObject)Instantiate(pfCola, new Vector3(0, 0, 0), pfCola.transform.rotation), new Vector3(0, 0, 0)));
                if (enemigo.tail.Count > 0)
                {
                    // Move last Tail Element to where the Head was
                    enemigo.tail[enemigo.tail.Count - 1].GO.collider.enabled = false;
                    enemigo.tail[enemigo.tail.Count - 1].posicion = pos;
                    enemigo.tail[enemigo.tail.Count - 1].actualizarPos();
                    // Add to front of list, remove from the back
                    enemigo.tail.Insert(0, enemigo.tail[enemigo.tail.Count - 1]);
                    enemigo.tail.RemoveAt(enemigo.tail.Count - 1);
                    if (enemigo.tail.Count > 2) enemigo.tail[2].GO.collider.enabled = true;
                }
            }
            
        }
        if (Time.time > nextActionTime2 ) { 
            period2 = Random.Range(0.3f, 1.5f);
            nextActionTime2 += period2;
            switch (enemigo.movimiento)
            {
                case "normal":
                    movimientoNormal();
                    break;
                case "giro":
                    movimientoGiro();
                    break;
            }
        }
        
	}

    public void movimientoNormal()
    {
        direccion = Random.Range(0, 4);
        switch (direccion)
        {
            case 0:
                this.gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                break;
            case 1:
                this.gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                break;
            case 2:
                this.gameObject.transform.rotation = Quaternion.AngleAxis(-90, Vector3.up);
                break;
            case 3:
                this.gameObject.transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                break;
        }
        
    }

    public void movimientoGiro()
    {
        direccion = Random.Range(0, 2);
        switch (direccion)
        {
            case 0:
                actualAngle -= 90;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.up);
                break;
            case 1:
                actualAngle += 90;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.up);
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") Controller.reglasMan.onChoca(enemigo, Controller.Player);
        if (collision.gameObject.name == "Tail(Clone)") Controller.reglasMan.onChoca(enemigo, Controller.Player);

        switch (enemigo.movimiento)
        {
            case "normal":
                movimientoNormal();
                break;
            case "giro":
                movimientoGiro();
                break;
        }
    }

    void crecerCola()
    {
        enemigo.tail.Add(new Cola((GameObject)Instantiate(pfCola, new Vector3(0, 0, 0), pfCola.transform.rotation), new Vector3(0, 0, 0)));
    }
       
}

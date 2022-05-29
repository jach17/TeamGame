using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Start is called before the first frame update

    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public int hp = 10;
    public int dañoArma = 8;
    public int dañoPuño = 4;
    void Start()
    {       
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    public void Comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position)>7)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;

            if (cronometro >= 4)
            {

                rutina = Random.Range(0,2);
                cronometro = 0;

            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                  
                    break;
                case 1:
                  
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);

                    break;




            }
        }
        else
        {

            if (Vector3.Distance(transform.position, target.transform.position)>1 && !atacando)
            {
                ani.SetBool("run", true);
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 5);
                ani.SetBool("walk", false);
                transform.Translate(Vector3.forward * 5 * Time.deltaTime);
                ani.SetBool("attack", false);
            }
            else
            {
                //ani.SetBool("run", false); 
                //ani.SetBool("walk", false);
                ani.SetBool("attack", true);
                atacando = true;
            }



        }
    }
    public void final_ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
    }
    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arma")
        {
            ani.Play("Damage");
            hp -= dañoArma;
        }
        if (other.gameObject.tag =="Puño")
        {
            ani.Play("Damage");
            hp -= dañoPuño;
        }
        if (hp<1)
        {
            ani.SetBool("Death", true);
           
            Destroy(gameObject, 4.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlliesMovLogic : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private List<Transform> nearWallTransform = new List<Transform>();
    private Transform MasCercano;
    private bool EnemigoCerca = false;
    private BoxCollider boxCollider;
    
    private GameObject enemy;
    private AllieDamage damage;

    private void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        damage = GetComponentInChildren<AllieDamage>();

    }


    void Update()
    {



        

        if(EnemigoCerca == true && damage.Attacking == false)
        {
            if (enemy != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
            }
            else
            {
                
                EnemigoCerca = false;
                enemy = null;
            }



            // transform.position = Vector3.MoveTowards(transform.position,enemy.transform.position,speed * Time.deltaTime); 
        }
   
        if (EnemigoCerca == false)
        {
            float distanciaMinima = Mathf.Infinity;
            MasCercano = null;

            foreach (Transform t in nearWallTransform)
            {
                float dist = Vector3.Distance(t.position, transform.position);

                if (dist < distanciaMinima)
                {
                    distanciaMinima = dist;
                    MasCercano = t;
                }
            }


            if (MasCercano != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, MasCercano.position, speed * Time.deltaTime);
            }
        }
        //Debug.Log(EnemigoCerca);
        //Debug.Log(damage.Attacking);
        
    }

    public void EnemyDetected(GameObject detectedEnemy)
    {
        if (enemy == null || enemy != detectedEnemy)
        {
            enemy = detectedEnemy;
            EnemigoCerca = true;
        }
    }

    public void EnemyLost()
    {
        EnemigoCerca = false;
        enemy = null;
    }



    /*private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {



            if (enemy == null || enemy != other.gameObject)
            {
                enemy = other.gameObject;
                EnemigoCerca = true;
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            EnemigoCerca = false;
            enemy = null;


        }
      }*/
}










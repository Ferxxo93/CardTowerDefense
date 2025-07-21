using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AllieDamage : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private GameObject enemy;
    private EnemyLife enemylife;
    


    
   [SerializeField] public AllieType type;
    private float damage;
    private float damageMelee = 50f;
    private float damageRange = 80f;
    private float firstTimer = 0f;
    private float timer;
    private float timerMelee = 2f;
    private float timerRange = 5f;
 
  


    private bool attacking = false;
    
    public bool Attacking => attacking;
   
 



    



    void Start()
    {
     sphereCollider = GetComponent<SphereCollider>();





        
        if (type == AllieType.MeleeAllie)
        {
            damage = damageMelee;
            timer = timerMelee;
        }
        if(type == AllieType.RangeAllie)

        {
            damage = damageRange;   
            timer = timerRange;
        }


    }

    


    // Update is called once per frame
    void Update()
    {
       




       if(attacking == true)
        {
            if (enemy == null)
            {
                attacking = false;
                enemylife = null;
                return;
            }
            firstTimer -= Time.deltaTime;

            if(firstTimer < 0f) {

             enemylife.DamageEnemy(damage);
             firstTimer = timer;
            }
        }
        

    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (enemy == null || enemy != other.gameObject) 
            {
                enemy = other.gameObject;
                enemylife = enemy.GetComponent<EnemyLife>();
                attacking = true;
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (enemy != null && enemy == other.gameObject)
            {
                attacking = false;
                enemy = null;
                enemylife = null;
            }
        }
    }
}

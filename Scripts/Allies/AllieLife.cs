using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class AllieLife : MonoBehaviour
{
    [SerializeField] private float life;

   
    private void Update()
    {
        
        
        
        
        if(life < 1)
        {
            Destroy(gameObject);
        }
               
    }


    public void DamageAllie(float damage)
    {
        life -= damage;
        

    }



}

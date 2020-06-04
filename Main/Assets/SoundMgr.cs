using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{

  
    private AudioSource Enemydie;

    // Start is called before the first frame update
    void Start()
    {
       
        Enemydie = GetComponent<AudioSource>();
    }

    // Update is called once per frame
  

   public void EnemyDieSound()
    {
        
        
            Enemydie.Play();
        

    }
}

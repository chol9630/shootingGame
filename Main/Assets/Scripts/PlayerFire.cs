using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//2.6  2.25    고정 <-> 0.75



public enum EnemyFire
{
    wait, attack , Sattack
}  //대기 , 공격 , s자 공격


public class PlayerFire : MonoBehaviour
{
    [HideInInspector]
    public EnemyFire EnemyState = EnemyFire.wait;

    public GameObject Bullet;
   

    BulletMove bulletMove;

    AudioSource FireSound;
    
    public GameObject PreFabBullet;
    public GameObject FirePosL;
    public GameObject FirePosR;

    public GameObject Player;

    public EnemyMove EnemyMove;
    public EnemyParams enemyParams;

    public PlayerParams playerParams;

    
    float PlayerTime = 0f;//플레이어 자동발사

    float ModenTime = 0f;//현재시간
    float WaitTime = 2f;//기다릴시간

    public bool Boombutton;
    private float boomTime;

    // Start is called before the first frame update
    void Start()
    {
        playerParams = GetComponent<PlayerParams>();

        Boombutton = false;
        FireSound = GetComponent<AudioSource>();
       

    }
  

    // Update is called once per frame
    void Update()
    {

        Fire();
       
        if (Boombutton == true)
        {
            boomTime += Time.deltaTime;
            if (boomTime > 0.1f)
            {
                boomTime = 0f;
                Boombutton = false;
            }
        }

    }

    public void Boom()
    {
         if (playerParams.BoomNum > 0)
         {
            
            playerParams.BoomNum -= 1;
            Boombutton = true;
            boomTime += Time.deltaTime;
            playerParams.PlayerBooms[playerParams.BoomNum].gameObject.SetActive(false);
            if (playerParams.BoomNum - 1 >= 0)
            {
                playerParams.PlayerBooms[playerParams.BoomNum - 1].gameObject.SetActive(true);
               
            }

        }
        if(playerParams.BoomNum ==0)
        {
          for(int i=0; i<playerParams.MaxBoombNum; i++)
            {
                playerParams.PlayerBooms[i].gameObject.SetActive(false);
            }
                

        }
        


        
    }
        
    
    

    void Fire()
    {
        Vector3 FirePosLPosition = FirePosL.transform.position;
        Vector3 FirePosRPosition = FirePosR.transform.position;

        Quaternion FirePosLRotation = FirePosL.transform.rotation;
        Quaternion FirePosRRotation = FirePosR.transform.rotation;

        PlayerTime +=1 ;
        

        if (gameObject.CompareTag("Player"))
        {

            if (PlayerTime % 6 ==1)
            {
                
                ObjectPool.Instance.SpawnFromPool("PlayerBullet", FirePosLPosition, FirePosLRotation);
                ObjectPool.Instance.SpawnFromPool("PlayerBullet", FirePosRPosition, FirePosRRotation);

                FireSound.Play();
              

             
            }



        }

        else if (gameObject.CompareTag("Enemy"))
        {

            
           if(EnemyState == EnemyFire.wait)
            {

                ModenTime += Time.deltaTime;

                //Random.Range(0, 2);

                if (ModenTime > WaitTime)
                {
                    //Debug.Log(Random.Range(0, 2));
                    if (Random.Range(0, 2) == 0)
                    { 
                        ModenTime = 0f;
                        EnemyState = EnemyFire.attack;
                    }
                    if (Random.Range(0, 2) == 1)
                    {
                       
                        ModenTime = 0f;
                        EnemyState = EnemyFire.Sattack;
                    }
                }

            }
            
            
            if (EnemyState == EnemyFire.attack)
            {

                

                
                ObjectPool.Instance.SpawnFromPool("EnemyBullet", FirePosLPosition, FirePosLRotation);
                ObjectPool.Instance.SpawnFromPool("EnemyBullet", FirePosRPosition, FirePosRRotation);
                //Bullet.tag = "Enemy";
                EnemyState = EnemyFire.wait;
               
                                 
            }
            if (EnemyState == EnemyFire.Sattack)
            {


                                
                ObjectPool.Instance.SpawnFromPool("EnemyBullet", FirePosLPosition, FirePosLRotation).name = "SatackL";
                
                ObjectPool.Instance.SpawnFromPool("EnemyBullet", FirePosRPosition, FirePosRRotation).name = "SatackR";
                


               
                EnemyState = EnemyFire.wait;
                    ModenTime = 0f;
               


            }
        }
    }

    
    
}

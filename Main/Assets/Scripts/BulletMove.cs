using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Transform BulletTr;

    private float MoveSpeed = 10f;
    //GameObject Enemy;
    
    private PlayerParams Player;
    public GameObject Enemy;
    

    private PlayerFire Fire;

    
    private EnemySpawn enemySpawn;
    private EnemyMove enemyMove;
   
    public float MoveTime =0f;
  
    private float Rotate = 1f;


    //float RotateTime = 0f;

    public bool EnemyIsDamege;

    private Stage stage;

   

    public bool IsPlayerDamege = false;

    private float Sin; //사인함수 값저장

   
    // Start is called before the first frame update
    void Start()
    {

        stage = GameObject.Find("reSpawn").GetComponent<Stage>();
        BulletTr = GetComponent<Transform>();
        enemyMove = GetComponent<EnemyMove>();
       
        Sin = Mathf.Sin(-0.5f);

      Player = GameObject.Find("Player").GetComponent<PlayerParams>();
      
        

      enemySpawn = GameObject.Find("reSpawn").GetComponent<EnemySpawn>();

      

    }



    // Update is called once per frame
    void Update()
    {
        Damage();
        EnemyDie();
        BulletRotate();
        BulletDestory();
       
        
     
    }

    void Damage()
    {

       
        //적이 부딪히는 조건
        if (!BulletTr.CompareTag("Enemy"))
        {

            for (int i=0; i<enemySpawn.EnemyList.Count; i++)
            {



                if (BulletTr.position.x > enemySpawn.EnemyList[i].transform.position.x - 0.4f && BulletTr.position.x < enemySpawn.EnemyList[i].transform.position.x + 0.4f &&
                   //&&  a - tr.position.x  * (1- tr.localScale.x )<1* b -0.6 * b &&

                   BulletTr.position.z > enemySpawn.EnemyList[i].transform.position.z - 0.5f && BulletTr.position.z < enemySpawn.EnemyList[i].transform.position.z + 0.5f)
                {
                    //enemySpawn.EnemyList[i].GetComponent<bu>

                    //a = i;
                    //Debug.Log(enemySpawn.EnemyList[i]);
                    EnemyIsDamege = true;

                    enemySpawn.EnemyList[i].GetComponent<EnemyParams>().CurHp -= 5f;

                    //enemySpawn.enemyParams.CurHp -= 5f;

                }
               
            }
            
        }
        
        //플레이어가 부딪히는 조건
        if (BulletTr.CompareTag("Enemy"))
        {
            if (
                BulletTr.position.x > Player.gameObject.transform.position.x  - 0.4f && BulletTr.position.x < Player.gameObject.transform.position.x + 0.4f &&
                BulletTr.position.z >Player.gameObject.transform.position.z -0.5f && BulletTr.position.z < Player.gameObject.transform.position.z + 0.5f)
            {
                IsPlayerDamege = true;
                Player.PlayerCurHp -= 20f;
                Player.PlayerDamege();
            }
        }
    }


    void EnemyDie()
    {

        for (int i = 0; i < enemySpawn.EnemyList.Count; i++)
        {

            if (enemySpawn.EnemyList[i].enemyParams.CurHp <= 0f)
            {
                enemySpawn.EnemyDieList[i].transform.position = enemySpawn.EnemyList[i].transform.position;
                if (enemySpawn.EnemyDieList[i].isPlaying == false)
                {
                    enemySpawn.EnemyDieList[i].Play();
                    
                   enemySpawn.soundMgr.EnemyDieSound();
                }






               
                enemySpawn.EnemyList[i].EnemyMove.ModenTime = 0f;
                enemySpawn.EnemyList[i].EnemyMove.enemyPatten = EnemyPatten.Wait;
                enemySpawn.EnemyList[i].gameObject.SetActive(false);







                //게임종료




            }
        }
        
        




    }

 


    void BulletRotate()
    {

        if (Rotate < 4)
        {
            Rotate += 1 * Time.deltaTime;
        }
        else if (Rotate == 4 || Rotate > 4)
        {
            Rotate = 0;
        }



        if (BulletTr.name == "SatackL")
        {
            if (Rotate < 2f)
            {
                BulletTr.Rotate(0f, Sin, 0f);
            }
            else if (Rotate > 2f)
            {
                BulletTr.Rotate(0f, -Sin, 0f);
            }


        }
        


        else if (BulletTr.name == "SatackR")
        {

            if (Rotate < 2f)
            {


                BulletTr.Rotate(0f, -Sin, 0f);
            }
            else if (Rotate > 2f)
            {
                BulletTr.Rotate(0f, Sin, 0f);
            }




        }



        MoveTime += Time.deltaTime;



        BulletTr.position += BulletTr.forward * MoveSpeed * Time.deltaTime;

    }



    void BulletDestory()
    {
        if (EnemyIsDamege == true)
        {
            this.gameObject.SetActive(false);


           
            MoveTime = 0f;
            EnemyIsDamege = false;
        }
        else if (EnemyIsDamege == false && MoveTime > 3.5f)
        {
            this.gameObject.SetActive(false);


            
            MoveTime = 0f;
        }


        if (Player.BoomNum > 0 && Player.PlayBoom.Boombutton == true)
        {

            if (this.gameObject.CompareTag("Enemy"))
            {
                this.gameObject.SetActive(false);
                
            }


            MoveTime = 0f;
           
        }


        if (IsPlayerDamege == true)
        {
            this.gameObject.SetActive(false);

            if (this.gameObject.CompareTag("Enemy"))
            {
                this.gameObject.name = "EnemyBullet";
            }
            MoveTime = 0f;

           
            IsPlayerDamege = false;

        }
        else if (MoveTime > 10 && IsPlayerDamege == false)
        {
            IsPlayerDamege = false;
            MoveTime = 0f;
            
            this.gameObject.SetActive(false);


            if (this.gameObject.CompareTag("Enemy"))
            {
                this.gameObject.name = "EnemyBullet";
            }
        }

    }
    
  


}

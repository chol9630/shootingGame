using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyPatten
{
    Wait , Move , Rotate  ,End    
  //기다림 , 몸박 ,몸박후돌아가기 ,  회전    , 패턴끝  
}

public class EnemyMove : MonoBehaviour
{
    //[HideInInspector]
    public EnemyPatten enemyPatten = EnemyPatten.Wait;
    public float ModenTime;//현재 시간




    private Vector3 MovePosition;
    private Vector3 EndPosition;
    private float enemyMoveSpeed;
    private float TimeDelay = 3f;//딜레이 시간
    private EnemySpawn enemySpawn;


    public float EndTime;

   

    //rotate 는 현재 기준에서 회전할 값만 넣어서 사용.
    //rotation 은 현재 회전할 값을 사용  현재 기준이 없음


    void Start()
    {


        enemySpawn = GameObject.Find("reSpawn").GetComponent<EnemySpawn>();
        enemyMoveSpeed = 5f;
        ModenTime = 0f;
        
    }

    void Update()
    {
       
        Patten();
    }

    void Patten()
    {

            ModenTime += Time.deltaTime;
        
            if (enemyPatten == EnemyPatten.Wait)
            {
                MovePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 20f);
                EndPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                if (ModenTime > TimeDelay)
                {


                    switch (Random.Range(0, 3)) //0~2)
                    {

                        case 0:
                            enemyPatten = EnemyPatten.Move;
                            ModenTime = 0f;
                            break;
                        case 1:
                            enemyPatten = EnemyPatten.Rotate;
                            ModenTime = 0f;
                            break;
                        case 2:
                            enemyPatten = EnemyPatten.End;
                            ModenTime = 0f;
                            break;




                    }
                }
            }
            else if (enemyPatten == EnemyPatten.Move)
            {

               
                if (transform.position != MovePosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, MovePosition, enemyMoveSpeed * Time.deltaTime);
                    
                }
                else
                {
                    enemyPatten = EnemyPatten.End;
                    ModenTime = 0f;
                }
               
            }
            else if (enemyPatten == EnemyPatten.End)
            {
            
                if (transform.position != EndPosition)
                {
            
                   transform.position = Vector3.MoveTowards(transform.position, EndPosition, enemyMoveSpeed * Time.deltaTime);


                }
                else 
                {
                    enemyPatten = EnemyPatten.Wait;
                    ModenTime = 0f;
                }
            
            }
            else if (enemyPatten == EnemyPatten.Rotate)
            {
                if (ModenTime < 10f)
                {
                    transform.Rotate(0f, Mathf.Sin(1f), 0f);
                    transform.position += transform.forward * enemyMoveSpeed * Time.deltaTime;
                }
                else
                {
                    transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, 0f);
                    enemyPatten = EnemyPatten.End;
                    ModenTime = 0f;
                }
            }
        }
    
}
    


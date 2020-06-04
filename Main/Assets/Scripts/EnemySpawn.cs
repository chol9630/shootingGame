using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    //[System.Serializable]

    public GameObject EnemyPre; //에너미 프리팹 저장


    //public Vector3[] SpawnStart;
    [SerializeField]
    private List<Vector3> SpawnStart = new List<Vector3>();
    [SerializeField]
    private List<Vector3> SpawnEnd = new List<Vector3>();
  
    public int ReSpawnSize;           //적몇마리 생성할껀지

    public float SpawnSpeed;
    
    public bool IsStarted;

    //enemy저장해둘 리스트
    public List<PlayerFire> EnemyList = new List<PlayerFire>();

    //시작위치 저장해줄 리스트
    public List<Vector3> ETransform = new List<Vector3>();

    private float StartTime =0f;

    //public Text text;

    private bool ReStart;
    public int Stage; //스테이지 몇인지

    private Stage stage;

    private PlayerFire Enemy;

    private AudioSource StageSonud;

    private ParticleSystem EnemyEffect;
   
    public List<ParticleSystem> EnemyDieList = new List<ParticleSystem>();
    public int Score = 0;

    public EnemyParams enemyParams;
    public  float PaticleTime = 0f;

    public SoundMgr soundMgr;
   
    // Start is called before the first frame update
    void Start()
    {
        StartTime = 0f;


        StageSonud = GetComponent<AudioSource>();
        StageSonud.Play();
        stage = GetComponent<Stage>();
         Stage = 1;
        
        stage.StageText.text = "Stage : " + Stage.ToString();
        stage.StageNum = Stage;
        stage.ScoreText.text = "Score : " + Score.ToString();
        stage.ScoreNum = Score;





        Enemy = EnemyPre.GetComponent<PlayerFire>();
        
        EnemyEffect = EnemyPre.GetComponent<ParticleSystem>();
        
       


        ReSpawnSize = Stage;//스테이지숫자만큼 적생성
        
         enemyParams = EnemyPre.GetComponent<EnemyParams>();


       

        //생성되는 위치값 리스트에 저장해서 프리팹 만들떄 사용하자
        for (int i = 0; i < ReSpawnSize; i++)
        {
            //SpawnStart[i] = new Vector3(Random.Range(-9+i, i-8), 0.5f, Random.Range(13, 15));
            SpawnStart.Add(new Vector3(Random.Range(-9 + i, i - 8), 0.5f, Random.Range(13, 15)));
            ETransform.Add(SpawnStart[i]);

        }
        for (int i = 0; i < SpawnStart.Count; i++)
        {
            SpawnEnd.Add(new Vector3(SpawnStart[i].x, SpawnStart[i].y, Random.Range(7, 11)));
        }
        for (int i = 0; i < Stage; i++)
        {
            //GameObject instance = Instantiate(EnemyPre, ETransform[i], Quaternion.identity);

            
            Enemy = ObjectPool.Instance.SpawnFromPool("Enemy", ETransform[i], Quaternion.identity).GetComponent<PlayerFire>();
            
            Enemy.enabled = false;
            
            EnemyEffect = ObjectPool.Instance.SpawnFromPool("Effect", ETransform[i], Quaternion.identity).GetComponent<ParticleSystem>();
            
            EnemyEffect.Stop();
            EnemyDieList.Add(EnemyEffect);
            EnemyList.Add(Enemy);
        }

        //게임이 시작하면 이동하게
        IsStarted = true;
        ReStart = false;
        //시작하면 프리팹을 만들고 프리팹만든 위치를 적list에 저장하기

    }

    // Update is called once per frame
    void Update()
    {
        Enemysummons();

    }

    //로드 할떄 재소환
    public void LoadEnemy()
    {
        ReStart = true;
        //로드햇을떄 숫자가 다르면  다 비워주고
        if (ReStart == true)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].gameObject.SetActive(false);
            }
           
            EnemyList.Clear();    
            EnemyDieList.Clear(); 
            ETransform.Clear();   
            SpawnStart.Clear();   
            SpawnEnd.Clear();     
            

            
            ReSpawnSize = Stage;
            
             for (int i = 0; i < ReSpawnSize; i++)
             {
                    //SpawnStart[i] = new Vector3(Random.Range(-9+i, i-8), 0.5f, Random.Range(13, 15));
                    SpawnStart.Add(new Vector3(Random.Range(i + 1, i - 1), 0.5f, Random.Range(13, 15)));
                    ETransform.Add(SpawnStart[i]);
            
             }
            
            
             for (int i = 0; i <ReSpawnSize; i++)
             {  
                 SpawnEnd.Add(new Vector3(Random.Range(i + 1, i - 1), 0.5f, Random.Range(7, 12)));
             }
            
            
            
            for (int i = 0; i < Stage; i++)
            {
                //EnemyPre.GetComponent<EnemyParams>().CurHp = 20;
                Enemy = ObjectPool.Instance.SpawnFromPool("Enemy", ETransform[i], Quaternion.identity).GetComponent<PlayerFire>();
            
                
                EnemyList.Add(Enemy);
            
                EnemyEffect = ObjectPool.Instance.SpawnFromPool("Effect", ETransform[i], Quaternion.identity).GetComponent<ParticleSystem>();
                EnemyEffect.Stop();
                EnemyDieList.Add(EnemyEffect);
            }
                ReStart = false;
            
        }

    }

    


    //적소환
    void Enemysummons()
    {


        StartTime += Time.deltaTime;
        

        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyDieList[i].isPlaying == true)
            {
                PaticleTime += Time.deltaTime;
                
                if (PaticleTime >= 0.5f && EnemyList[i].enemyParams.CurHp <=0)
                {
                    EnemyList[i].enemyParams.CurHp = EnemyList[i].enemyParams.HP;
                    EnemyDieList[i].gameObject.SetActive(false);
                    EnemyDieList.Remove(EnemyDieList[i]);
                    
                    EnemyList.Remove(EnemyList[i]);
                    IsStarted = true;
                    Score += 50;
                    stage.TextScore();
                    PaticleTime = 0f;
                  
                }
            }

        }


        //적이 이동도중 다죽었을때 다음스테이지
        if (EnemyList.Count == 0 && IsStarted == false && ReStart == false)
        {
           
                Stage += 1;
            SpawnSpeed += 0.1f;
                stage.TextStage();
            
            ReSpawnSize = Stage;//스테이지숫자만큼 적생성




            for (int i = 0; i < Stage; i++)
            {
                //EnemyPre.GetComponent<EnemyParams>().CurHp = 20;
                Enemy = ObjectPool.Instance.SpawnFromPool("Enemy", ETransform[i], Quaternion.identity).GetComponent<PlayerFire>();

                Enemy.enabled = false;
                EnemyList.Add(Enemy);
                EnemyEffect = ObjectPool.Instance.SpawnFromPool("Effect", ETransform[i], Quaternion.identity).GetComponent<ParticleSystem>();
                //EnemyEffect.Stop();
                EnemyDieList.Add(EnemyEffect);
               
            }
        }

        //시작을 햇는데 적이 없으면 만들어주자 다음스테이지
        if (EnemyList.Count == 0 && IsStarted == true && ReStart == false)
        {
        
        
            
                Stage += 1;
            SpawnSpeed += 0.1f;
            stage.TextStage();
               ReSpawnSize = Stage;
            
            if (SpawnStart.Count != ReSpawnSize)
            {
                for (int i = 0; i < ReSpawnSize-SpawnStart.Count; i++)
                {
                    //SpawnStart[i] = new Vector3(Random.Range(-9+i, i-8), 0.5f, Random.Range(13, 15));
                    SpawnStart.Add(new Vector3(Random.Range(i+ 3, i -4), 0.5f, Random.Range(13, 15)));
                    ETransform.Add(SpawnStart[i]);
        
                }  
            }
            if (SpawnEnd.Count != SpawnStart.Count)
            {
                for (int i = 0; i < SpawnStart.Count-SpawnEnd.Count; i++)
                {
                    SpawnEnd.Add(new Vector3(Random.Range(i + 3, i - 4), 0.5f, Random.Range(7, 12)));
                }
            }
        
        
            for (int i = 0; i < Stage; i++)
            {
                //EnemyPre.GetComponent<EnemyParams>().CurHp = 20;
                Enemy = ObjectPool.Instance.SpawnFromPool("Enemy", ETransform[i], Quaternion.identity).GetComponent<PlayerFire>();
        
                Enemy.enabled = false;
                EnemyList.Add(Enemy);
        
                EnemyEffect = ObjectPool.Instance.SpawnFromPool("Effect", ETransform[i], Quaternion.identity).GetComponent<ParticleSystem>();
                EnemyEffect.Stop();
                EnemyDieList.Add(EnemyEffect);
                
            }
        
        }



        if (IsStarted == true && ReStart == false)
        {

            for (int i = 0; i < EnemyList.Count; i++)
            {

                if (EnemyList[i].transform.position.z > SpawnEnd[i].z)
                {

                    EnemyList[i].transform.position = Vector3.MoveTowards(EnemyList[i].transform.position, SpawnEnd[i], SpawnSpeed * Time.deltaTime);
                    //Debug.Log(EnemyList[i].transform.position.z);
                }

                //시작햇고 다이동햇으면  IsStarted = false;중단
                else if (EnemyList[EnemyList.Count - 1].transform.position.z == SpawnEnd[EnemyList.Count - 1].z)
                {
                    IsStarted = false;

                    EnemyList[i].enabled = true;
                    stage.TextStage();
                }
            }

            for (int i = 0; i < EnemyDieList.Count; i++)
            {
                if (EnemyDieList[i].transform.position.z > SpawnEnd[i].z)
                {

                    EnemyDieList[i].transform.position = Vector3.MoveTowards(EnemyDieList[i].transform.position, SpawnEnd[i], SpawnSpeed * Time.deltaTime);
                    //Debug.Log(EnemyList[i].transform.position.z);
                }
                
            }
        }
       
       
        
        

    }


   
}

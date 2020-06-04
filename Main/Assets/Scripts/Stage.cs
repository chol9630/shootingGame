using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{

    public Text StageText;
    public Text ScoreText;
    public EnemySpawn enemySpawn;
    public int StageNum;
    public int ScoreNum;
    private PlayerParams PlayerHp;

    public GameObject StopButton;
    public GameObject PlayButton;
    public GameObject SaveButton;
  

    private bool IsOnceLoad;

    private int StopCount;
    //// Start is called before the first frame update
    void Start()
    {
        //enemySpawn = enemySpawn.GetComponent<EnemySpawn>();

        SaveButton.SetActive(false);
       
        PlayerHp = GameObject.Find("Player").GetComponent<PlayerParams>();

        StageText.text = "Stage : " + StageNum.ToString();
        
        //메인 씬에서 로드를 햇으면 로드를 하고 시작하자
        if(SceneChange.Load==true)
        {
            load();
        }

        StageNum = enemySpawn.Stage;
        IsOnceLoad = false;

    }
    
    public void Stop()
    {
        StopCount += 1;
        if (StopCount == 1) //정지 상태
        {
            PlayButton.SetActive(true);
            SaveButton.SetActive(true);
            

            StopButton.SetActive(false);
            StageText.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(false);

            PlayerHp.PlayBoom.enabled =false;
            Time.timeScale = 0;
        }
        if(StopCount ==2)  //정지상태 해제
        {
            PlayButton.SetActive(false);
            SaveButton.SetActive(false);
            

            StopButton.SetActive(true);
            StageText.gameObject.SetActive(true);
            ScoreText.gameObject.SetActive(true);
            Time.timeScale = 1;
            StopCount = 0;

            PlayerHp.PlayBoom.enabled = true;//플레이어 총알 다시 발사

        }
    }

    
    public void Save()
    {
        if (StopCount == 1)
        {
            PlayerPrefs.SetInt("Stage", StageNum);
            PlayerPrefs.SetInt("Score", ScoreNum);
            PlayerPrefs.SetFloat("PlayHp", PlayerHp.PlayerCurHp);
            PlayerPrefs.SetInt("Boom", PlayerHp.BoomNum);
            
        }
    }

    public void Main()
    {
        SceneManager.LoadScene("Start");
    }

   


     public void load()
    {
        if (IsOnceLoad == false)
        {
            if (PlayerPrefs.HasKey("Stage"))
            {
                StageNum = PlayerPrefs.GetInt("Stage", StageNum);
                StageText.text = "Stage : " + StageNum.ToString();
                enemySpawn.IsStarted = true;
                enemySpawn.Stage = StageNum;
                enemySpawn.ReSpawnSize = StageNum;
                enemySpawn.LoadEnemy();
                ScoreNum = PlayerPrefs.GetInt("Score", ScoreNum);
                enemySpawn.Score = ScoreNum;
                ScoreText.text = "Score : " + ScoreNum.ToString();

                PlayerHp.PlayerCurHp = PlayerPrefs.GetFloat("PlayHp", PlayerHp.PlayerCurHp);
                PlayerHp.PlayerDamege();

                PlayerHp.BoomNum = PlayerPrefs.GetInt("Boom", PlayerHp.BoomNum);

                // 3 2 1
                // x x 1   -> //o x 1
                if (PlayerHp.BoomNum - 1 >= 0)
                {
                    
                    PlayerHp.PlayerBooms[PlayerHp.BoomNum - 1].gameObject.SetActive(true);
                }
               

            }
            IsOnceLoad = true;
        }
    }
    
    public void TextScore()
    {
        ScoreNum = enemySpawn.Score;
        ScoreText.text = "Score : " + ScoreNum.ToString();


        if (ScoreNum % 1000 ==1)
        {
            if (PlayerHp.PlayerCurHp + 50 < PlayerHp.PlayerHp)
            {
                PlayerHp.PlayerCurHp += 50;
            }
            else if (PlayerHp.PlayerCurHp + 50 > PlayerHp.PlayerHp)
            {
                PlayerHp.PlayerCurHp = PlayerHp.PlayerHp;
            }
        }
    }

    public  void TextStage()
    {
        if (enemySpawn.IsStarted == false)
        {
            StageText.enabled = false;
        }
        else if(enemySpawn.IsStarted == true)   
        {
            StageText.enabled = true;
        }



        StageNum = enemySpawn.Stage;
        StageText.text = "Stage : " + StageNum.ToString();
       
    }

}

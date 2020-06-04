using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerParams : MonoBehaviour
{

    public int BoomNum;
    public int MaxBoombNum;
    public GameObject Player;
    //public GameObject Enemey;

    public float PlayerHp = 100;
    public float PlayerCurHp;
    sbyte PlayLifeNum;
    

    public Image PlayerHpBar;
    public Text PlayerLife;

    public PlayerFire PlayBoom;

    public Text GameOver;

    public Image[] PlayerBooms;

    public GameObject MainButton;
    

   

    // Start is called before the first frame update
    void Start()
    {
        PlayBoom = GetComponent<PlayerFire>();
        BoomNum = 3;
        MaxBoombNum = BoomNum;
        PlayerCurHp = PlayerHp;

        PlayLifeNum = 0;
        PlayerLife.text = "X :  " + PlayLifeNum;

      

    }
    
    public void PlayerDamege()
    {

        PlayerHpBar.fillAmount =(PlayerCurHp / PlayerHp);
       
        if(PlayerCurHp <=0 && PlayLifeNum >0)
        {
            PlayLifeNum -= 1;
            PlayerCurHp += 100f;
            PlayerLife.text = "X :  " + PlayLifeNum;
            PlayerHpBar.fillAmount =(PlayerCurHp / PlayerHp);
        }

        
    

       else if(PlayerCurHp <=0 && PlayLifeNum <=0)
        {
            GameOver.gameObject.SetActive(true);
            PlayBoom.enabled = false;
            MainButton.gameObject.SetActive(true);
           
            


        }


        //else
        //{
        //    EnemyCurHp -= 5f;
        //}

    }

    public void GameOverButton()
    {
        SceneManager.LoadScene("Start");
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private int count =0;
    static public bool Load;

    private void Update()
    {
        MobieQuit();
    }

    public void MainSceneChage()
    {
        SceneManager.LoadScene("Main");

    }
    public void LoadGameChage()
    {
        SceneManager.LoadScene("Main");
        if (PlayerPrefs.HasKey("Stage"))
        {
            Load = true;
        }
    }
    public void Quit()
    {
        

        UnityEditor.EditorApplication.isPlaying =false;
        Application.Quit();
    }
    public void MobieQuit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            count += 1;

        }
        else if (count >= 2)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            count = 0;
        }

    }

    public void StartSceneChage()
    {
        SceneManager.LoadScene("Start");
    }
   
}

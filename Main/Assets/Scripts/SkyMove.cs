using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMove : MonoBehaviour
{

    PlayerMove playerMove;
    float MoveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation",   playerMove.PlayerWSKey+3*Time.time);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rb;

    public joystickMove joystick;
    public float MoveSpeed = 2.0f;

    public float PlayerWSKey =0f;

    
    

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

       
    }

    // Update is called once per frame
    void Update()
    {

        float rectX = joystick.Stick.GetComponent<RectTransform>().localPosition.x;
        float rectY = joystick.Stick.GetComponent<RectTransform>().localPosition.y;
        #region 키 받아서 값이용
        //float LRKey = Input.GetAxis("Horizontal");
        float LRKey = (rectX -50)*0.05f;
        float UDKey = (rectY - 50) * 0.05f;
        //float UDKey = Input.GetAxis("Vertical");

        #endregion

        float LR1Key  = LRKey * MoveSpeed *Time.deltaTime;
        float WSKey = UDKey * MoveSpeed * Time.deltaTime;

        PlayerWSKey = WSKey;


       tr.transform.position = new Vector3(tr.transform.position.x + LR1Key, tr.transform.position.y, (tr.transform.position.z + WSKey));

        //pushObjectBackInFrustum(tr.transform);

        

    }


    
   
}

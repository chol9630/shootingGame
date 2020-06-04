using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class joystickMove : MonoBehaviour
{

    public GameObject Stick;
    public GameObject center;
    private float rideus;
    private Vector3 centerPos;
   
    Vector3 BoomRect;

    // Start is called before the first frame update
    void Start()
    {
        //중심좌표 저장
        centerPos = new Vector3(center.transform.position.x, center.transform.position.y, center.transform.position.z);
        rideus = GetComponent<RectTransform>().rect.height / 2;//반지름
        //int layer =  LayerMask.NameToLayer("UI");
        //int layer = LayerMask.NameToLayer("BoomButton");
        //Debug.Log(layer);
    }

    // Update is called once per frame
    void Update()
    {
        StickMove();
        
    }

    void StickMove()
    {


        if (Input.GetMouseButton(0))
        {
            
            
            Vector3 mousePos = Input.mousePosition; //Camera.main.WorldToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z)); // .transform.position.z));

            // float Distance =  Vector3.Distance(mousePos, Stick.transform.position);

            //sqrMagnitude 쓰는게 distance쓰는거보다 루트를 안쓴 값을 반환하므로 더 최적화 시킬수있음


            float distance = (mousePos - centerPos).sqrMagnitude;


            Vector3 direction = (mousePos - centerPos).normalized; //방향 구하는 공식 (목표-현재위치)  
                                                                   //normalized 크기를 1로 맞춘다.



                    if (distance <= 2500)
                    {
                        Stick.transform.position = mousePos;

                    }
                    else if( distance >2500  && distance <150000)
                    {
                        Stick.transform.position = centerPos + direction * rideus;
                        //최대 크기 처음위치 + 방향 * 반지름 만큼
                    }
                

            
        }
        else
        {
            Stick.transform.position = centerPos;
        }
        
    }
}

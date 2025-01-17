using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //스크롤 제한 멤버 변수
    public float leftLimit = 0.0f;  
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;


    public GameObject subScreen; //서브 스크린. GameObject형식은 게임오브젝트자료형임.


    public bool isForceScrollX = false;
    public float forceScrollSpeedX = 0.0f;
    public bool isForceScrollY = false;
    public float forceScrollSpeedY = 0.0f;







    void Start()
    {
        
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //카메라의 좌표 갱신
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;          //z축은 왜 필요하지?


            //가로 방향 동기화         가로세로 따로 if문 분리해서 만드는 이유는 두가지 조건을 동시에 체크해야하는 경우를 위한 겁니다.
            //양 끝에 이동 제한 적용
            if (isForceScrollX)
            {
                //가로 강제 스크롤
                x = transform.position.x + (forceScrollSpeedX * Time.deltaTime);
            }
            if (x < leftLimit)
            {
                x = leftLimit;
            }
            else if (x > rightLimit)
            {
                x = rightLimit;
            }

            //세로 방향 동기화
            if (isForceScrollY)
            {
                //세로 강제 스크롤
                y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }
            //위아래에 이동 제한 적용
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            //카메라 위치의 Vector3 만들기. 유니티는 2D라도 3D기준으로 transform을 받아오기때문에 Vector3를 사용함.
            Vector3 v3 = new Vector3 (x, y, z);
            transform.position = v3;



            //서브 스크린 스크롤
            if (subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                //subScreen용 Vector3 좌표입니다. 플레이어 위치 x축의 2분의 1만큼 움직입니다.
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            }
        }

    }


}

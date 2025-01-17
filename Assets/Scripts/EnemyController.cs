using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

/*Description
 * 현재 해당 스크립트에서 개선되어야 할 부분은 아래와 같습니다.
 * 초기위치 설정때문에 좌측, 우측의 움직일 수 있는 구간이 암묵적으로 지정되어 있습니다.
 * 초기위치 설정과 관련된 코드를 개선함이 필요해보입니다.
 */ 


public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;              // 이동속도
    public string direction = "left";       // 이동방향
    public float range = 0.0f;              // 움직이는 범위
    Vector3 defPos;                         // 시작위치
    

    void Start()
    {
        if(direction == "right")
        {
            transform.localScale = new Vector2(-1,1);       //나중에 flip X로 전환테스트 합시다
        }
        //시작 위치
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /* 현재 상태는 계속해서 프레임단위로 빠르게 적용되어야하기 때문에, 상태는 Update에서
         * 체크하되, 직접적인 이동 연산은 FixedUpdate 메서드 내에서 합니다.
        */


        if(range > 0.0f)
        {
            /*
             * 10의 범위 사이를 움직일 테니 움직일 거리는 지정한 거리에 2를 나눠주어야 합니다.
             * 즉 range가 10일떄 현재 위치가 거리에서 10-5를 한것보다 크면 우측 상태라는 겁니다.
            */
            if (transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";   
                /* 기본적으로 스프라이트가 좌측을 바라보고 있으므로 우측으로 이동중일땐
                 * 스프라이트 반전을 해줍니다.
                */
                transform.localScale = new Vector2(-1,1); 
            }
            if (transform.position.x > defPos.x + (range / 2))
            {
                direction = "left"; 
                transform.localScale = new Vector2(1,1);   
            }
        }
    }


    void FixedUpdate()
    {
        //속도 갱신
        //Rigidbody2D 가져오기
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();


        if (direction == "right")
        {
            rbody.velocity = new Vector2 (speed, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y);
        }
    }


    /* 무언가 오브젝트에 부딪혔다면 방향을 전환합니다.
      폭탄과의 충돌은 무시해야하니 Enemy레이어와 Shell과의 레이어 옵션을 조정하도록 합시다.
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(direction == "right")
        {
            direction = "left";
            transform.localScale *= new Vector2(1,1);   
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1);
        }
    }


}

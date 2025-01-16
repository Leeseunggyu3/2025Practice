using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    //static 변수에 저장된 값은 게임이 완전히 종료될 때까지 사라지지 않습니다. 하나만 존재해야하는 요소가 static 키워드를 사용하여 주로 관리됩니다.
    public static string gameState = ""; //게임 상태


    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f; //이동속도

    public float jump = 9.0f; //점프력
    public LayerMask groundLayer; //착지할 수 있는 레이어                            더 알아봐야 됨. < 뭘 더 알아봐야 한다는거지? LayerMask가 뭔지 뭔 기능하는애인지.
    bool goJump = false; //점프 개시 플래그
    bool onGround = false; //지면에 서 있는 플래그

    Animator animator;
    public string idleAnime = "Player_Idle";
    public string walkAnime = "Player_Walk";
    public string jumpAnime = "Player_Jump";
    public string goalAnime = "Player_Goal";
    public string deadAnime = "Player_Dead";
    string nowAnime = "";
    string oldAnime = "";


    public int score = 0; 



    void Start()
    {
        // Rigidbody2D
        rbody = gameObject.GetComponent<Rigidbody2D>();
        //Animator 가져오기
        animator = GetComponent<Animator>();
        //시작할 때 기본 애니메이션으로 설정.
        nowAnime = idleAnime; 
        oldAnime = idleAnime;
        gameState = "Playing"; //게임 중
    }

    
    void Update()
    {
        if(gameState != "Playing")
        {
            return;
        }

        // 수평방향입력
        axisH = Input.GetAxisRaw("Horizontal");
        // 방향 조절
        if (axisH > 0.0f)
        {
            //오른쪽 이동 (localScale은 그림을 뒤집어버리는놈임
            Debug.Log("오른쪽 이동");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH<0.0f)
        {
            //왼쪽 이동
            Debug.Log("왼쪽 이동");
            transform.localScale = new Vector2(-1, 1); //좌우 반전시키기
        }

        //캐릭터 점프하기
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //점프
        }

    }

    void FixedUpdate()
    {
        if (gameState != "Playing")
        {
            return;
        }

        //착지판정
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.5f), groundLayer);
        
        
        if (axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(0, rbody.velocity.y);
        }


        if (onGround && goJump)
        {
            // 지면 위에서 점프 키 눌림
            // 점프하기
            Debug.Log("점프");
            Vector2 jumpPw = new Vector2(0, jump); // 점프를 위한 벡터 생성
            rbody.AddForce(jumpPw,ForceMode2D.Impulse); // 순간적인 힘 가하기
            goJump = false; // 점프 플래그 끄기
        }

        //지면 위 일때
        if(onGround)
        {
            // 움직이고 있지 않다면 => 기본 애니메이션 재생
            if (axisH == 0)
            {
                nowAnime = idleAnime;
            }
            // 움직이고 있는 중이라면 => 걷는 애니메이션 재생
            else 
            {
                nowAnime = walkAnime;
            }
        }
        //공중 일떄
        else
        {
            nowAnime = jumpAnime;
        }


        // 앞서 지정했던 지면 위일때, 공중일때의 애니메이션 상태를 파악하고, 애니메이션을 재생함.
        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //애니메이션 재생
        }
    }

    //점프
    public void Jump()
    {
        goJump = true; //점프 플래그 켜기
        Debug.Log(" 점프 버튼 눌림! ");
    }

    // 접촉 시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal(); //골
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            //점수 아이템
            //ItemData 가져오기
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //점수 얻기
            score = item.value;
            //아이템제거
            Destroy(collision.gameObject);
        }
    }

    //골
    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "GameClear";
        GameStop(); //게임 중지
    }
    //게임오버
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "GameOver";
        GameStop();


        //게임오버 연출
        //플레이어의 충돌 판정 비활성 기능을 아래에 구현하였으나, 내가 원하는 연출이 아니라 주석처리 했습니다.
        //GetComponent<CapsuleCollider2D>().enabled = false;
    }
    void GameStop()
    {
        //Rigidbody2D가져오기 
       // Rigidbody2D rbody = GetComponent<Rigidbody2D>(); //위에서 rbody 선언했는데 왜 하나 더 만듬?
        //속도를 0으로 하여 강제 정지
        rbody.velocity = new Vector2(0, 0);
    }

}

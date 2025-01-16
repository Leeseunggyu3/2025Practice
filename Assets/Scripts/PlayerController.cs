using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    //static ������ ����� ���� ������ ������ ����� ������ ������� �ʽ��ϴ�. �ϳ��� �����ؾ��ϴ� ��Ұ� static Ű���带 ����Ͽ� �ַ� �����˴ϴ�.
    public static string gameState = ""; //���� ����


    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f; //�̵��ӵ�

    public float jump = 9.0f; //������
    public LayerMask groundLayer; //������ �� �ִ� ���̾�                            �� �˾ƺ��� ��. < �� �� �˾ƺ��� �Ѵٴ°���? LayerMask�� ���� �� ����ϴ¾�����.
    bool goJump = false; //���� ���� �÷���
    bool onGround = false; //���鿡 �� �ִ� �÷���

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
        //Animator ��������
        animator = GetComponent<Animator>();
        //������ �� �⺻ �ִϸ��̼����� ����.
        nowAnime = idleAnime; 
        oldAnime = idleAnime;
        gameState = "Playing"; //���� ��
    }

    
    void Update()
    {
        if(gameState != "Playing")
        {
            return;
        }

        // ��������Է�
        axisH = Input.GetAxisRaw("Horizontal");
        // ���� ����
        if (axisH > 0.0f)
        {
            //������ �̵� (localScale�� �׸��� ����������³���
            Debug.Log("������ �̵�");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH<0.0f)
        {
            //���� �̵�
            Debug.Log("���� �̵�");
            transform.localScale = new Vector2(-1, 1); //�¿� ������Ű��
        }

        //ĳ���� �����ϱ�
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //����
        }

    }

    void FixedUpdate()
    {
        if (gameState != "Playing")
        {
            return;
        }

        //��������
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
            // ���� ������ ���� Ű ����
            // �����ϱ�
            Debug.Log("����");
            Vector2 jumpPw = new Vector2(0, jump); // ������ ���� ���� ����
            rbody.AddForce(jumpPw,ForceMode2D.Impulse); // �������� �� ���ϱ�
            goJump = false; // ���� �÷��� ����
        }

        //���� �� �϶�
        if(onGround)
        {
            // �����̰� ���� �ʴٸ� => �⺻ �ִϸ��̼� ���
            if (axisH == 0)
            {
                nowAnime = idleAnime;
            }
            // �����̰� �ִ� ���̶�� => �ȴ� �ִϸ��̼� ���
            else 
            {
                nowAnime = walkAnime;
            }
        }
        //���� �ϋ�
        else
        {
            nowAnime = jumpAnime;
        }


        // �ռ� �����ߴ� ���� ���϶�, �����϶��� �ִϸ��̼� ���¸� �ľ��ϰ�, �ִϸ��̼��� �����.
        if(nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime); //�ִϸ��̼� ���
        }
    }

    //����
    public void Jump()
    {
        goJump = true; //���� �÷��� �ѱ�
        Debug.Log(" ���� ��ư ����! ");
    }

    // ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            Goal(); //��
        }
        else if (collision.gameObject.tag == "Dead")
        {
            GameOver();
        }
        else if (collision.gameObject.tag == "ScoreItem")
        {
            //���� ������
            //ItemData ��������
            ItemData item = collision.gameObject.GetComponent<ItemData>();
            //���� ���
            score = item.value;
            //����������
            Destroy(collision.gameObject);
        }
    }

    //��
    public void Goal()
    {
        animator.Play(goalAnime);
        gameState = "GameClear";
        GameStop(); //���� ����
    }
    //���ӿ���
    public void GameOver()
    {
        animator.Play(deadAnime);

        gameState = "GameOver";
        GameStop();


        //���ӿ��� ����
        //�÷��̾��� �浹 ���� ��Ȱ�� ����� �Ʒ��� �����Ͽ�����, ���� ���ϴ� ������ �ƴ϶� �ּ�ó�� �߽��ϴ�.
        //GetComponent<CapsuleCollider2D>().enabled = false;
    }
    void GameStop()
    {
        //Rigidbody2D�������� 
       // Rigidbody2D rbody = GetComponent<Rigidbody2D>(); //������ rbody �����ߴµ� �� �ϳ� �� ����?
        //�ӵ��� 0���� �Ͽ� ���� ����
        rbody.velocity = new Vector2(0, 0);
    }

}

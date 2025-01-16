using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{
    public float length = 0.0f;         // �ڵ� ���� Ž�� �Ÿ�
    public bool isDelete = false;       // ���� �� �������� ����
            
    bool isFell = false;                // ���� �÷���
    float fadeTime = 0.5f;              // ���̵� �ƿ� �ð�


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody ���� �ùķ��̼� ����
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ã��
        if (player != null)
        {
            //�÷��̾���� �Ÿ� ���
            /* Vector2.Distance�޼���� ���� 3�Ƚᵵ�ȴ��ϳ׿�
             * Vector2.Distance : �μ��� ���޵� 2�� ������ �Ÿ��� ���ϴ� �޼����Դϴ�.
             */
            
            float d = Vector2.Distance(transform.position, player.transform.position);

            if (length >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    // Rigidbody2D ���� �ùķ��̼� ����
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        if (isFell)
        {
            //����
            //������ ������ ���̵�ƿ� ȿ��
            fadeTime -= Time.deltaTime;                         //���� �����Ӱ��� ���̸�ŭ �ð� ����
            Color col = GetComponent<SpriteRenderer>().color;   //�÷� �� ��������
            col.a = fadeTime;                                   //���� ����
            GetComponent<SpriteRenderer>().color = col; ;       //�÷� ���� �缳��
            if (fadeTime <= 0.0f)
            {
                //0���� ������(����)����
                Destroy(gameObject);
            }
        }
    }


    //���� ����
    void OnCollisionEnter2D(Collision2D collision)
    {
     if(isDelete)
        {
            isFell = true; //���� �÷��� true
        }
    }



}

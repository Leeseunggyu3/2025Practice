using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

/*Description
 * ���� �ش� ��ũ��Ʈ���� �����Ǿ�� �� �κ��� �Ʒ��� �����ϴ�.
 * �ʱ���ġ ���������� ����, ������ ������ �� �ִ� ������ �Ϲ������� �����Ǿ� �ֽ��ϴ�.
 * �ʱ���ġ ������ ���õ� �ڵ带 �������� �ʿ��غ��Դϴ�.
 */ 


public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;              // �̵��ӵ�
    public string direction = "left";       // �̵�����
    public float range = 0.0f;              // �����̴� ����
    Vector3 defPos;                         // ������ġ
    

    void Start()
    {
        if(direction == "right")
        {
            transform.localScale = new Vector2(-1,1);       //���߿� flip X�� ��ȯ�׽�Ʈ �սô�
        }
        //���� ��ġ
        defPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /* ���� ���´� ����ؼ� �����Ӵ����� ������ ����Ǿ���ϱ� ������, ���´� Update����
         * üũ�ϵ�, �������� �̵� ������ FixedUpdate �޼��� ������ �մϴ�.
        */


        if(range > 0.0f)
        {
            /*
             * 10�� ���� ���̸� ������ �״� ������ �Ÿ��� ������ �Ÿ��� 2�� �����־�� �մϴ�.
             * �� range�� 10�ϋ� ���� ��ġ�� �Ÿ����� 10-5�� �Ѱͺ��� ũ�� ���� ���¶�� �̴ϴ�.
            */
            if (transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";   
                /* �⺻������ ��������Ʈ�� ������ �ٶ󺸰� �����Ƿ� �������� �̵����϶�
                 * ��������Ʈ ������ ���ݴϴ�.
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
        //�ӵ� ����
        //Rigidbody2D ��������
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


    /* ���� ������Ʈ�� �ε����ٸ� ������ ��ȯ�մϴ�.
      ��ź���� �浹�� �����ؾ��ϴ� Enemy���̾�� Shell���� ���̾� �ɼ��� �����ϵ��� �սô�.
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

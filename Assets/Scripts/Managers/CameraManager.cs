using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //��ũ�� ���� ��� ����
    public float leftLimit = 0.0f;  
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;


    public GameObject subScreen; //���� ��ũ��. GameObject������ ���ӿ�����Ʈ�ڷ�����.


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
            //ī�޶��� ��ǥ ����
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float z = transform.position.z;          //z���� �� �ʿ�����?


            //���� ���� ����ȭ         ���μ��� ���� if�� �и��ؼ� ����� ������ �ΰ��� ������ ���ÿ� üũ�ؾ��ϴ� ��츦 ���� �̴ϴ�.
            //�� ���� �̵� ���� ����
            if (isForceScrollX)
            {
                //���� ���� ��ũ��
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

            //���� ���� ����ȭ
            if (isForceScrollY)
            {
                //���� ���� ��ũ��
                y = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }
            //���Ʒ��� �̵� ���� ����
            if (y < bottomLimit)
            {
                y = bottomLimit;
            }
            else if (y > topLimit)
            {
                y = topLimit;
            }
            //ī�޶� ��ġ�� Vector3 �����. ����Ƽ�� 2D�� 3D�������� transform�� �޾ƿ��⶧���� Vector3�� �����.
            Vector3 v3 = new Vector3 (x, y, z);
            transform.position = v3;



            //���� ��ũ�� ��ũ��
            if (subScreen != null)
            {
                y = subScreen.transform.position.y;
                z = subScreen.transform.position.z;
                //subScreen�� Vector3 ��ǥ�Դϴ�. �÷��̾� ��ġ x���� 2���� 1��ŭ �����Դϴ�.
                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScreen.transform.position = v;
            }
        }

    }


}

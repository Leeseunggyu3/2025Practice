using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage; //�̹����� ��Ƶδ� GameObject
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;


    Image titleImage;

    void Start()
    {
        //�̹��� �����
        Invoke("InactiveImage", 1.0f);                                      //InactiveImage �� ""�� ǥ���Ѱź��� ��������Ʈ�� ������Ʈ�� �ҷ��´ٴ°ǰ�? �ƴϸ� ������������
        //��ư(�г�)�� �����
        panel.SetActive(false);
    }

    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            //���� Ŭ����
            mainImage.SetActive(true); //�̹��� ǥ��
            panel.SetActive(true);
            
            //Restart ��ư ��ȿȭ
            Button restartBtn = restartButton.GetComponent<Button>();
            restartBtn.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "gameover")
        {
            //���ӿ���
            mainImage.SetActive(false); //�̹��� ǥ��
            panel.SetActive(true); 
            //Next��ư�� ��Ȱ��
            Button nextBtn = nextButton.GetComponent<Button>(); 
            nextBtn.interactable = false;
            mainImage.GetComponent <Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "playing")
        {
            //������

        }
    }
    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}

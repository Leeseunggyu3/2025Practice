using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage; //�̹����� ��Ƶδ� GameObject
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;


    Image titleImage;


    public GameObject timeSlot;      // �ð�ǥ���̹���
    public GameObject timeTxt;       // �ð� �ؽ�Ʈ      
    TimeController timeCnt;          // TimeController ����


    public GameObject scoreText;    // ���� �ؽ�Ʈ
    public static int totalScore;   // ���� ����
    public int stageScore = 0;      // �������� ����


    // ���� ���
    public AudioClip meGameOver;    // ���� ����
    public AudioClip meGameClear;   // ���� Ŭ����
    void Start()
    {
        //�̹��� �����
        Invoke("InactiveImage", 1.0f);                                      //InactiveImage �� ""�� ǥ���Ѱź��� ��������Ʈ�� ������Ʈ�� �ҷ��´ٴ°ǰ�? �ƴϸ� ������������
        //��ư(�г�)�� �����
        panel.SetActive(false);


        // +++ �ð����� �߰� +++
        //TimeController ������
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeSlot.SetActive(false);
            }
        }


        // ���� �߰�
        UpdateScore();
    }

    void Update()
    {


        if(PlayerController.gameState == "GameClear")
        {
            //���� Ŭ����
            mainImage.SetActive(true); //�̹��� ǥ��
            panel.SetActive(true);
            
            //Restart ��ư ��ȿȭ
            Button restartBtn = restartButton.GetComponent<Button>();
            restartBtn.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "GameEnd";


            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //�ð� ī��Ʈ ����
                //���� �߰�
                //������ �Ҵ��Ͽ� �Ҽ����� ������.
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10; //���� �ð��� ������ ���Ѵ�
            }


            // +++++++++++++++++++++++++ ���� �߰�
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore(); //���� ����


            // +++++++++++++++++++++++++ ���� ��� �߰�
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM ����
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameClear);

            }
        }


        else if (PlayerController.gameState == "GameOver")
        {
            //���ӿ���
            mainImage.SetActive(false); //�̹��� ǥ��
            panel.SetActive(true); 


            //Next��ư�� ��Ȱ��
            Button nextBtn = nextButton.GetComponent<Button>(); 
            nextBtn.interactable = false;
            mainImage.GetComponent <Image>().sprite = gameOverSpr;
            PlayerController.gameState = "GameEnd";


            //�ð����� �߰�
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; // �ð� ī��Ʈ ����
            }
        }
        else if (PlayerController.gameState == "Playing")
        {
            //������
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // PlayerController ��������
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            
            
            //�ð����� �߰�
            //�ð� ����
            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //������ �Ҵ��Ͽ� �Ҽ��� ���ϸ� ����
                    int time = (int)timeCnt.displayTime;
                    //�ð� ����
                    timeTxt.GetComponent<TMP_Text>().text = time.ToString();
                    //Ÿ�ӿ���
                    if(time == 0) 
                    {
                        playerCnt.GameOver(); // ���� ����
                    }
                    //���� �߰�
                    if (playerCnt.score != 0)
                    {
                        stageScore += playerCnt.score;
                        playerCnt.score = 0;
                        UpdateScore();
                    }
                }
            }
        }
    }
    //�̹��� �����
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }


    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<TMP_Text>().text = score.ToString();
    }

}

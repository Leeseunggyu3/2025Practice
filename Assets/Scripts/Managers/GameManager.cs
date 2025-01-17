using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage; //이미지를 담아두는 GameObject
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;


    Image titleImage;


    public GameObject timeSlot;      // 시간표시이미지
    public GameObject timeTxt;       // 시간 텍스트      
    TimeController timeCnt;          // TimeController 변수


    public GameObject scoreText;    // 점수 텍스트
    public static int totalScore;   // 점수 총합
    public int stageScore = 0;      // 스테이지 점수


    // 사운드 재생
    public AudioClip meGameOver;    // 게임 오버
    public AudioClip meGameClear;   // 게임 클리어
    void Start()
    {
        //이미지 숨기기
        Invoke("InactiveImage", 1.0f);                                      //InactiveImage 에 ""로 표시한거보면 스프라이트나 오브젝트를 불러온다는건가? 아니면 형식이저러나
        //버튼(패널)을 숨기기
        panel.SetActive(false);


        // +++ 시간제한 추가 +++
        //TimeController 가져옴
        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeSlot.SetActive(false);
            }
        }


        // 점수 추가
        UpdateScore();
    }

    void Update()
    {


        if(PlayerController.gameState == "GameClear")
        {
            //게임 클리어
            mainImage.SetActive(true); //이미지 표시
            panel.SetActive(true);
            
            //Restart 버튼 무효화
            Button restartBtn = restartButton.GetComponent<Button>();
            restartBtn.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "GameEnd";


            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; //시간 카운트 중지
                //점수 추가
                //정수에 할당하여 소수점을 버린다.
                int time = (int)timeCnt.displayTime;
                totalScore += time * 10; //남은 시간을 점수에 더한다
            }


            // +++++++++++++++++++++++++ 점수 추가
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore(); //점수 갱신


            // +++++++++++++++++++++++++ 사운드 재생 추가
            AudioSource soundPlayer = GetComponent<AudioSource>();
            if (soundPlayer != null)
            {
                //BGM 정지
                soundPlayer.Stop();
                soundPlayer.PlayOneShot(meGameClear);

            }
        }


        else if (PlayerController.gameState == "GameOver")
        {
            //게임오버
            mainImage.SetActive(false); //이미지 표시
            panel.SetActive(true); 


            //Next버튼을 비활성
            Button nextBtn = nextButton.GetComponent<Button>(); 
            nextBtn.interactable = false;
            mainImage.GetComponent <Image>().sprite = gameOverSpr;
            PlayerController.gameState = "GameEnd";


            //시간제한 추가
            if (timeCnt != null)
            {
                timeCnt.isTimeOver = true; // 시간 카운트 중지
            }
        }
        else if (PlayerController.gameState == "Playing")
        {
            //게임중
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            // PlayerController 가져오기
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            
            
            //시간제한 추가
            //시간 갱신
            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {
                    //정수에 할당하여 소수점 이하를 버림
                    int time = (int)timeCnt.displayTime;
                    //시간 갱신
                    timeTxt.GetComponent<TMP_Text>().text = time.ToString();
                    //타임오버
                    if(time == 0) 
                    {
                        playerCnt.GameOver(); // 게임 오버
                    }
                    //점수 추가
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
    //이미지 숨기기
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

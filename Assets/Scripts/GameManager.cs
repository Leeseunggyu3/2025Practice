using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage; //이미지를 담아두는 GameObject
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;


    Image titleImage;

    void Start()
    {
        //이미지 숨기기
        Invoke("InactiveImage", 1.0f);                                      //InactiveImage 에 ""로 표시한거보면 스프라이트나 오브젝트를 불러온다는건가? 아니면 형식이저러나
        //버튼(패널)을 숨기기
        panel.SetActive(false);
    }

    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            //게임 클리어
            mainImage.SetActive(true); //이미지 표시
            panel.SetActive(true);
            
            //Restart 버튼 무효화
            Button restartBtn = restartButton.GetComponent<Button>();
            restartBtn.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "gameover")
        {
            //게임오버
            mainImage.SetActive(false); //이미지 표시
            panel.SetActive(true); 
            //Next버튼을 비활성
            Button nextBtn = nextButton.GetComponent<Button>(); 
            nextBtn.interactable = false;
            mainImage.GetComponent <Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
        }
        else if (PlayerController.gameState == "playing")
        {
            //게임중

        }
    }
    //이미지 숨기기
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

}

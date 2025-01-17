using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public string sceneName; //ºÒ·¯¿Ã ¾À

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}

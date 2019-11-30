using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public void ChangeScene()
    {
        switch (name)
        {
            case "Level1Button":
                SceneManager.LoadScene("Level1");
                break;
            case "Level2Button":
                SceneManager.LoadScene("Level2");
                break;
            case "Level3Button":
                SceneManager.LoadScene("Level3");
                break;
            case "HomeButton":
                SceneManager.LoadScene("SelectLevel");
                break;
            default:
                break;
        }
    }
}

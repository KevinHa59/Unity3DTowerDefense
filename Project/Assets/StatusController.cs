using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Text StatusText;
    public GameObject WinFX;
    public void Set_Status(bool isWinner) {
        
        if (isWinner)
        {
            StatusText.text = "You Win";
            StatusText.color = Color.yellow;
        }
        else {
            WinFX.SetActive(false);
            StatusText.text = "You Lose";
        }
    }

    public void btn_Retry() {
        FindObjectOfType<FadeControl>().ActiveFadeOut(SceneManager.GetActiveScene().name);
        
        Time.timeScale = 1;
    }
    public void btn_Done() {
        
        FindObjectOfType<FadeControl>().ActiveFadeOut("Dashboard");
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject SettingPanel;

    public void btn_ActiveSettingPanel() {
        SettingPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void btn_DeactiveSettingPanel()
    {
        SettingPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void btn_Yes() {
        Time.timeScale = 1;
        SettingPanel.SetActive(false);
        FindObjectOfType<FadeControl>().ActiveFadeOut("Dashboard");
    }
}

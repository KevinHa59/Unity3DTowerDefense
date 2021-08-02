using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DashboardController : MonoBehaviour
{
    public GameObject ButtonPref;
    public GameObject ButtonContainer;
    int levelCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialButton();
        //UpdateLevelDetail();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void initialButton() {
        levelCount = GetComponent<MapManager>().MapPref.Length;
        for (int i = 0; i < levelCount; i++) {
            GameObject _mapButton = Instantiate(ButtonPref, ButtonContainer.transform);
            _mapButton.transform.name = "Level" + (i + 1);
            _mapButton.GetComponent<LevelButtonControl>().InitialSetup("Level " + (i + 1), GetComponent<MapManager>().MapPref[i].GetComponent<EnemiesManager>().Enemies.Length, GetComponent<MapManager>().MapPref[i].GetComponent<MapDetails>().Thumb);
            _mapButton.GetComponent<LevelButtonControl>().ButtonID = i;
            //_mapButton.GetComponent<Image>().color = GetComponent<MapManager>().MapPref[i].GetComponent<MapDetails>().RepresentColor;
            if(i == 0)
            {
                _mapButton.GetComponent<LevelButtonControl>().btn_LevelClick();

            }
        }
    }


    [Header("Level Detail")]
    public Text Level;
    public int LevelSelectedID = 0;
    public void UpdateLevelDetail() {
        Level.text = "Level "+ (LevelSelectedID + 1);
        int EnemyCount = GetComponent<MapManager>().MapPref[LevelSelectedID].GetComponent<EnemiesManager>().Enemies.Length;
    }

    public void btn_Play() {
        LevelID.LevelIDSelected = LevelSelectedID;
        FindObjectOfType<FadeControl>().ActiveFadeOut("SampleScene");
        //SceneManager.LoadScene("SampleScene");
        
    }
    public void btn_Back() {
        FindObjectOfType<FadeControl>().ActiveFadeOut("Menu");
        //SceneManager.LoadScene("Menu");
    }

    [Header("How to Play")]
    public GameObject InstructionPanel;
    public void btn_ActiveInstructionpanel() {
        InstructionPanel.SetActive(true);
    }
}

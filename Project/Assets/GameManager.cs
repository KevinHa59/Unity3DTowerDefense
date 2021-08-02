using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject Selected;
    public GameObject MainBuildingPanel;
    public GameObject UpgradePanel;
    public GameObject TowerBuildPanel;
    public GameObject BaseStationUnlockPanel;
    void Start()
    {
        InformationLabelUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    bool TowerUpgradePanel = false;
    bool TowerBuildPane = false;
    bool BaseStationPane = false;
    bool MainBuildingPane = false;
    public void setSelectedObject(GameObject newSelectedObject) {
        Selected = newSelectedObject;
        // Tower Select

        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Tower"))
        {
            child.transform.GetComponent<Weapon1Controller>().attackRangeObject.SetActive(false);
        }
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("RocketTower"))
        {
            child.transform.GetComponent<RocketController>().attackRangeObject.SetActive(false);
        }
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Cannon"))
        {
            child.transform.GetComponent<CannonController>().attackRangeObject.SetActive(false);
        }
        if (Selected.transform.tag == "Tower")
        {
            TowerUpgradePanel = true;
            Selected.transform.GetComponent<Weapon1Controller>().attackRangeObject.SetActive(true);
            Selected.transform.GetComponent<Weapon1Controller>().setUpdateInformation();
            UpgradePanel.GetComponent<Animator>().Play("UpgradePanel");
            UpgradePanel.GetComponent<UpgradeController>().SelectedTower = Selected;
        }
        else if (Selected.transform.tag == "RocketTower")
        {
            TowerUpgradePanel = true;
            Selected.transform.GetComponent<RocketController>().attackRangeObject.SetActive(true);
            Selected.transform.GetComponent<RocketController>().setUpdateInformation();
            UpgradePanel.GetComponent<Animator>().Play("UpgradePanel");
            UpgradePanel.GetComponent<UpgradeController>().SelectedTower = Selected;
        }
        else if (Selected.transform.tag == "Cannon")
        {
            TowerUpgradePanel = true;
            Selected.transform.GetComponent<CannonController>().attackRangeObject.SetActive(true);
            Selected.transform.GetComponent<CannonController>().setUpdateInformation();
            UpgradePanel.GetComponent<Animator>().Play("UpgradePanel");
            UpgradePanel.GetComponent<UpgradeController>().SelectedTower = Selected;
        }
        else {
            if (TowerUpgradePanel == true) {
                UpgradePanel.GetComponent<Animator>().Play("UpgradePanelHide");
                TowerUpgradePanel = false;
            }
        }
        // Main Building Select
        if (Selected.transform.tag == "MainBuilding")
        {

            MainBuildingPanel.GetComponent<Animator>().Play("MainBuilding");
            MainBuildingPane = true;
        
         }
        else {
            if (MainBuildingPane == true) {
                MainBuildingPanel.GetComponent<Animator>().Play("MainBuildingHide");

            }
        }

            // Base Station Select
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("BaseStation"))
        {
            child.transform.GetComponent<BaseStationControl>().ActiveHighlight(false);

        }
        if (Selected.transform.tag == "BaseStation")
        {
            Selected.transform.GetComponent<BaseStationControl>().ActiveHighlight(true);
            if (Selected.GetComponent<BaseStationControl>().Lock == false)
            {
                TowerBuildPane = true;
                MainCamera.GetComponent<Animator>().Play("CamMove");

                TowerBuildPanel.SetActive(true);
                TowerBuildPanel.GetComponent<Animator>().Play("TowersBuild");
                if (BaseStationPane == true)
                {
                    BaseStationUnlockPanel.GetComponent<Animator>().Play("BaseStationUnlockHide");
                    //BaseStationPane = false;
                }
            }
            else {
                //if (BaseStationPane == false) { 
                    BaseStationUnlockPanel.GetComponent<Animator>().Play("BaseStationUnlock");
                    BaseStationUnlockPanel.GetComponent<UnlockStationControl>().SelectedBaseStation = Selected;
                    BaseStationUnlockPanel.GetComponent<UnlockStationControl>().UnlockCost = Selected.GetComponent<BaseStationControl>().UnlockCost;
                    BaseStationUnlockPanel.GetComponent<UnlockStationControl>().CostText.text = Selected.GetComponent<BaseStationControl>().UnlockCost.ToString();
                    BaseStationPane = true;
                //}
                if (TowerBuildPane == true)
                {
                    
                    MainCamera.GetComponent<Animator>().Play("CamMoveBack");
                    TowerBuildPanel.GetComponent<Animator>().Play("TowersBuildHide");
                }
            }
        }
        else {
            if (TowerBuildPane == true) {
                TowerBuildPane = false;
                MainCamera.GetComponent<Animator>().Play("CamMoveBack");
                TowerBuildPanel.GetComponent<Animator>().Play("TowersBuildHide");
                
            }
            if (BaseStationPane == true) { 
                BaseStationUnlockPanel.GetComponent<Animator>().Play("BaseStationUnlockHide");
                //BaseStationPane = false;
            }
        }      


    }

    public void HideAllPanels() {
        TowerBuildPane = false;
        MainCamera.GetComponent<Animator>().Play("CamMoveBack");
        TowerBuildPanel.GetComponent<Animator>().Play("TowersBuildHide");
    }

    public GameObject getSelectedObject() {
        return Selected;
    }

    [Header("Player Information")]
    public int Cash;

    public Text Cash_lbl;
    public Text Cash_Update_lbl;
    void InformationLabelUpdate() {
        Cash_lbl.text = Cash.ToString();
        
    }
    public int getCurrentCash() {
        return Cash;
    }
    public void Cash_Use(int cash) {
        Cash -= cash;
        InformationLabelUpdate();
        Cash_Update_lbl.text = "-" + cash;
        Cash_lbl.GetComponent<Animator>().Play("CashDeduct", 0, 0);
    }
    public void Cash_Add(int cash)
    {
        Cash += cash;
        InformationLabelUpdate();
        Cash_Update_lbl.text = "+" + cash;
        Cash_lbl.GetComponent<Animator>().Play("CashAdd", 0, 0);
    }

    // Status Controller
    [Header("Status Controller")]
    public GameObject StatusPanel;
    public bool isWinner = false;
    void StatusPanelActive() {
        Invoke("PauseGame", 0.2f);
        StatusPanel.SetActive(true);
        StatusPanel.GetComponent<StatusController>().Set_Status(isWinner);
    }
    void PauseGame() {
        Time.timeScale = 0.05f;
    }
    public void Active_StatusPanel() {
        Invoke("StatusPanelActive",2);
    }


    // Media Controller
    public void btn_pause()
    {
        Time.timeScale = 0;
    }
    public void btn_play()
    {
        Time.timeScale = 1;
    }
    public void btn_speed(int speed)
    {
        Time.timeScale = speed;
    }

    public GameObject DestructionFX;
    public void Destruction() {
        DestructionFX.SetActive(true);
    }

    [Header("MessageManager")]
    public GameObject MessagePanel;

    public void setMessage(string message, float hideIn) {
        MessagePanel.GetComponent<Animator>().Play("Message",0,0);
        MessagePanel.transform.Find("Message").GetComponent<Text>().text = message;
        Invoke("HideMessage", hideIn);
    }

    void HideMessage() {
        MessagePanel.GetComponent<Animator>().Play("MessageHide", 0, 0);
    }



    [Header("Enemy Text UI")]
    public GameObject CountDownPanel;
    public Text CurrentWaveText;
    public Text CountDownNextWaveText;
    public Text EnemyNameText;
    public Text EnemyCountText;
    public Text EnemyHPText;
    public Image Circle;

    public void btn_SkipCountDown() {
        FindObjectOfType<EnemiesManager>().btn_skipCountDown();
    }

}

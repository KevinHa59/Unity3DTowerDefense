using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuildingControl : MonoBehaviour
{
    

    
    // Start is called before the first frame update
    void Start()
    {
        InitialInfor();
    }

    // Update is called once per frame
    void Update()
    {
        CostReloadControl();
        NuclearReload();
    }

    void InitialInfor() {
        ReloadCountDown = Reload[CashReloadID];
        HPText.text = HP.ToString();
        HPTopBar.text = HP.ToString();
        GetCashReloadInfor();
        HPInitial();
        NuclearInitial();
    }
    

    


    [Header("CastReload")]
    public Image CostReloadImage;
    public Text CurrentCashDisplayText;
    public Text CurrentCashText;
    public Text NextCashText;
    public Text CurrentReloadText;
    public Text NextReloadText;
    public Text UpgradeCostText;
    int CashReloadID = 0;
    int[] CashGet = new int[] { 50, 120, 300, 480, 610, 750, 900, 1200 };
    float[] Reload = new float[] { 10, 9.5f, 8.5f, 8f, 7.5f, 7f, 6.5f, 6f };
    int[] UpgradeCost = new int[] { 0, 1900, 2600, 3700, 4900, 6100, 7800, 10000 };
    float ReloadCountDown;
    void CostReloadControl() {
        if (ReloadCountDown > 0)
        {
            ReloadCountDown -= Time.deltaTime;
            CostReloadImage.fillAmount = 1 - (ReloadCountDown / Reload[CashReloadID]);
        }
        else
        {
            ReloadCountDown = Reload[CashReloadID];
            FindObjectOfType<GameManager>().Cash_Add(CashGet[CashReloadID]);
        }
    }

    void GetCashReloadInfor() {
        if (CashReloadID < CashGet.Length - 1)
        {
            CurrentCashDisplayText.text = "+ $"+CashGet[CashReloadID].ToString();
            CurrentCashText.text = CashGet[CashReloadID].ToString();
            NextCashText.text = CashGet[CashReloadID + 1].ToString();
            CurrentReloadText.text = Reload[CashReloadID].ToString() + "s";
            NextReloadText.text = Reload[CashReloadID + 1].ToString()+"s";
            UpgradeCostText.text = UpgradeCost[CashReloadID + 1].ToString();
        }
        else {
            CurrentCashDisplayText.text = "+ $"+ CashGet[CashReloadID].ToString();
            CurrentCashText.text = CashGet[CashReloadID].ToString();
            NextCashText.text = "Max";
            CurrentReloadText.text = Reload[CashReloadID].ToString();
            NextReloadText.text = "Max";
            UpgradeCostText.text = "Max";
        }
    }

    public void UpdateReloadCash() {
        
        if (CashReloadID < CashGet.Length - 1)
        {
            if (GetComponent<GameManager>().Cash >= UpgradeCost[CashReloadID + 1]) { 
                FindObjectOfType<GameManager>().Cash_Use(UpgradeCost[CashReloadID+1]);
                CashReloadID += 1;
                GetCashReloadInfor();
            }
        }
    }

    [Header("HP Control")]
    int HP;
    int HPID = 0;
    int[] HPs = new int[] {300, 500, 800, 1200, 1700, 2300, 2900, 3700, 5000 };
    int[] HPUpgradeCost = new int[] { 0, 1400, 2300, 3800, 5100, 6300, 7200, 8400, 10000 };

    public Text HPTopBar;
    public Text HPText;
    public Text currentHPText;
    public Text NextHPText;
    public Text HPUpgradeCostText;

    void HPInitial() {
        HP = HPs[HPID];
        HPText.text = HP.ToString();
        HPTopBar.text = HP.ToString();
        currentHPText.text = HPs[HPID].ToString();
        NextHPText.text = HPs[HPID+1].ToString();
        HPUpgradeCostText.text = HPUpgradeCost[HPID + 1].ToString();
    }

    void UpdateHPInformation() {
        if (HPID < HPs.Length - 1)
        {
            HPText.text = HP.ToString();
            HPTopBar.text = HP.ToString();
            currentHPText.text = HPs[HPID].ToString();
            NextHPText.text = HPs[HPID + 1].ToString();
            HPUpgradeCostText.text = HPUpgradeCost[HPID + 1].ToString();
        }
        else {
            HPText.text = HP.ToString();
            HPTopBar.text = HP.ToString();
            currentHPText.text = HPs[HPID].ToString();
            NextHPText.text = "Max";
            HPUpgradeCostText.text = "Max";
        }
    }

    public void HP_Deduct(int _amount)
    {
        if (_amount < HP)
        {
            HP -= _amount;
        }
        else
        {
            HP = 0;
            FindObjectOfType<GameManager>().isWinner = false;
            FindObjectOfType<GameManager>().Active_StatusPanel();
        }
        HPText.text = HP.ToString();
        HPTopBar.text = HP.ToString();
    }

    public void UpgradeHP() {
        if (HPID < HPs.Length - 1) {
            if (GetComponent<GameManager>().Cash >= HPUpgradeCost[HPID + 1])
            {
                GetComponent<GameManager>().Cash_Use(HPUpgradeCost[HPID + 1]);
                int HPLost = HPs[HPID] - HP;
                HPID += 1;
                HP = HPs[HPID] - HPLost;
                UpdateHPInformation();

            }
        }
    }

    [Header("Nuclear Weapon")]
    public GameObject NuclearPrefab;
    public Image ReloadNuclear;
    public Text NuClearCostText;
    int NuclearCost = 30000;
    float NuclearReloadTime = 60;
    float NuclearReloadCountdown = 0;
    bool NuclearReady = false;
    void NuclearInitial() {
        NuclearReloadCountdown = NuclearReloadTime;
        NuClearCostText.text = NuclearCost.ToString();
    }

    void NuclearReload() {
        if (NuclearReloadCountdown > 0)
        {
            NuclearReloadCountdown -= Time.deltaTime;
            ReloadNuclear.fillAmount = 1- (NuclearReloadCountdown / NuclearReloadTime);
        }
        else {
            NuclearReady = true;
        }
    }
    public void btn_ActiveNuclearWeapon() {
        if (NuclearReady)
        {
            if (FindObjectOfType<GameManager>().Cash >= NuclearCost)
            {
                GameObject _nuclear = Instantiate(NuclearPrefab);
                _nuclear.transform.position = GameObject.Find("NuclearWeaponPos").transform.position;
                NuclearReady = false;
                NuclearReloadCountdown = NuclearReloadTime;
                FindObjectOfType<GameManager>().Cash_Use(NuclearCost);
            }
            else
            {
                FindObjectOfType<GameManager>().setMessage("Not Enough Cash", 3f);
            }
        }
        else {
            FindObjectOfType<GameManager>().setMessage("Nuclear weapon is not ready", 4f);
        }
        

    }
}

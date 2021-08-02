using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [Header("Current Information")]
    public Image C_Icon;
    public Text C_Strength;
    public Text C_Range;
    public Text C_Reload;

    [Header("Next Information")]
    public Image N_Icon;
    public Text N_Strength;
    public Text N_Range;
    public Text N_Reload;
    public Text N_Cost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setCurrentInfor(Sprite icon, int Strength, int Range, float Reload) {
        C_Icon.sprite = icon;
        C_Strength.text = Strength.ToString();
        C_Range.text = Range.ToString();
        C_Reload.text = Reload.ToString() + "s";
    }

    public void setNextInfor(Sprite icon, int Strength, int Range, float Reload, int Cost)
    {
        N_Icon.sprite = icon;
        N_Strength.text = Strength.ToString();
        N_Range.text = Range.ToString();
        N_Reload.text = Reload.ToString() + " s";
        N_Cost.text = Cost.ToString();
    }

    public void setNextInforMax(Sprite icon)
    {
        N_Icon.sprite = icon;
        N_Strength.text = "MAX";
        N_Range.text = "MAX";
        N_Reload.text = "MAX";
        N_Cost.text = "MAX";
    }

    public GameObject SelectedTower;
    public void Upgrade() {
        if (SelectedTower.gameObject.tag == "Tower")
        {
            SelectedTower.GetComponent<Weapon1Controller>().setUpdateLevelInformation();
        }
        else if (SelectedTower.gameObject.tag == "RocketTower")
        {
            SelectedTower.GetComponent<RocketController>().setUpdateLevelInformation();
        }
        else if (SelectedTower.gameObject.tag == "Cannon")
        {
            SelectedTower.GetComponent<CannonController>().setUpdateLevelInformation();
        }
    }
}

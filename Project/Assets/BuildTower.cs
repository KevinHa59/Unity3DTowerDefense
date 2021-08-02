using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTower : MonoBehaviour
{
    public GameObject TowerPrefab;
    public Text Strength;
    public Text AttackRange;
    public Text Reload;
    public Text Cost;

    Component tower;

    private void Start()
    {
        
        InitialValue();
    }

    void InitialValue() {
        if (transform.name == "LazerTower") {
            //Strength.text = TowerPrefab.GetComponent<Weapon1Controller>().getWeaponInformation("Str");
            //AttackRange.text = TowerPrefab.GetComponent<Weapon1Controller>().getWeaponInformation("R");
            //Reload.text = TowerPrefab.GetComponent<Weapon1Controller>().getWeaponInformation("Rl");
            Cost.text = TowerPrefab.GetComponent<Weapon1Controller>().getWeaponInformation("C");
        }
        else if (transform.name == "RocketTower")
        {
            //Strength.text = TowerPrefab.GetComponent<RocketController>().getWeaponInformation("Str");
            //AttackRange.text = TowerPrefab.GetComponent<RocketController>().getWeaponInformation("R");
            //Reload.text = TowerPrefab.GetComponent<RocketController>().getWeaponInformation("Rl");
            Cost.text = TowerPrefab.GetComponent<RocketController>().getWeaponInformation("C");
        }
    }

    public Transform location;
    public void BuildTown() {
        if (FindObjectOfType<GameManager>().getCurrentCash() >= int.Parse(Cost.text.ToString())) { 
            GameObject _Tower = (GameObject)Instantiate(TowerPrefab);
            _Tower.transform.name = TowerPrefab.name;
            _Tower.transform.position = FindObjectOfType<GameManager>().getSelectedObject().transform.position;
            FindObjectOfType<GameManager>().HideAllPanels();
            FindObjectOfType<GameManager>().Cash_Use(int.Parse(Cost.text.ToString()));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStationControl : MonoBehaviour
{
    public GameObject SelectedBaseStation;
    public int UnlockCost;
    public Text CostText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock() {
        if (UnlockCost <= FindObjectOfType<GameManager>().Cash) { 
            SelectedBaseStation.GetComponent<BaseStationControl>().Lock = false;
            SelectedBaseStation.GetComponent<BaseStationControl>().InitialStation();
            GetComponent<Animator>().Play("BaseStationUnlockHide");
            FindObjectOfType<GameManager>().Cash_Use(UnlockCost);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStationControl : MonoBehaviour
{
    public bool Lock;
    public int UnlockCost;
    public GameObject[] Stations;
    public MeshRenderer Object;
    public Material Yellow;
    public Color[] SelectedFX;

    

    // Start is called before the first frame update
    void Start()
    {
        InitialStation();
        
        ActiveHighlight(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialStation() {
        Stations[0].SetActive(Lock);
        Stations[1].SetActive(!Lock);
        if (Lock == true)
        {
            Object = Stations[0].GetComponent<MeshRenderer>();
        }
        else {
            Object = Stations[1].GetComponent<MeshRenderer>();
        }
        Yellow = Object.materials[2];

    }

    public void ActiveHighlight(bool active) {
        if (active == true)
        {
            Yellow.color = SelectedFX[0];
        }
        else {
            Yellow.color = SelectedFX[1];
        }
    }

    [Header("Tower List")]
    public GameObject[] Towers;
}

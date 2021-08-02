using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public int MapID;
    public GameObject[] MapPref;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene") { 
            SpawnMap();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMap() {
        MapID = LevelID.LevelIDSelected;
        GameObject _Map = Instantiate(MapPref[MapID]);
    }
}

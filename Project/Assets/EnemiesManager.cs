using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public int[] EnemyQuantityEachWave;
    public int[] HPEnemyEachWave;
    public int currentWaveID = 0;
    int WaveCount = 0;
    public float WaitBetweenWave = 30;
    float WaitCountDown = 0;
    bool NextWaveStart = false;
    bool WaitActive = true;
    bool Finish = false;
    public bool LastWaveGenFinish = false;

    private void Start()
    {
        EnemyUIInitial();
        WaitCountDown = WaitBetweenWave;
        WaveInforUpdate();
    }
    private void Update()
    {
        if (Finish == false) { 
            if (NextWaveStart == true)
            {
                GenWave();
                FindObjectOfType<GameManager>().setMessage("New wave is coming", 3);
                NextWaveStart = false;
            }
            else {
                if (WaitActive == true) {
                    WaitCountDown -= Time.deltaTime;
                    CountDownNextWaveText.text = ((int)WaitCountDown+1).ToString();
                    FXControl();
                    CountDownPanel.SetActive(true);
                    EnemyInformation(); ;
                    if (WaitCountDown <= 0) {
                        countEnemyGen = 0;
                        //EnemyInformation();
                        NextWaveStart = true;
                        WaitCountDown = WaitBetweenWave;
                        WaitActive = false;
                        CountDownPanel.SetActive(false) ;
                        WaveInforUpdate();
                    }
                }
            }
        }
    }

    public int maxQuantity = 0;
    void GenWave() {
        for (int i = 0; i < EnemyQuantityEachWave[currentWaveID]; i++) {
            Invoke("GenEnemy", i*2);
        }
    }

    void GenEnemy() {
        countEnemyGen++;
        EnemyInformation();
        GameObject _enemy = Instantiate(Enemies[currentWaveID], transform);
        _enemy.transform.Find("Container").GetComponent<EnemyProperties>().HP = HPEnemyEachWave[currentWaveID];
        maxQuantity++;
        if (maxQuantity == EnemyQuantityEachWave[currentWaveID]) {
            WaitActive = true;
            maxQuantity = 0;
            if (currentWaveID < Enemies.Length-1)
            {
                currentWaveID++;
            }
            else {
                Finish = true;
            }
            if (WaveCount < Enemies.Length) {
                WaveCount++;
            }
        }
        if (WaveCount == Enemies.Length && countEnemyGen == EnemyQuantityEachWave[currentWaveID]) {
            LastWaveGenFinish = true;
        }

    }

    public bool CheckComplete() {
        if (LastWaveGenFinish == true)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
            {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    [Header("Information Properties")]
    
    public GameObject CountDownPanel;
    public Text CurrentWaveText;
    public Text CountDownNextWaveText;
    public Text EnemyNameText;
    public Text EnemyCountText;
    public Text EnemyHPText;
    int countEnemyGen = 0;

    void EnemyUIInitial() {
        CountDownPanel = FindObjectOfType<GameManager>().CountDownPanel;
        CurrentWaveText = FindObjectOfType<GameManager>().CurrentWaveText;
        CountDownNextWaveText = FindObjectOfType<GameManager>().CountDownNextWaveText;
        EnemyNameText = FindObjectOfType<GameManager>().EnemyNameText;
        EnemyCountText = FindObjectOfType<GameManager>().EnemyCountText;
        EnemyHPText = FindObjectOfType<GameManager>().EnemyHPText;
        Circle = FindObjectOfType<GameManager>().Circle;
}

    private void WaveInforUpdate()
    {
        CurrentWaveText.text = (currentWaveID+1) + "/" + Enemies.Length;
        
    }

    void EnemyInformation() {
        EnemyNameText.text = Enemies[currentWaveID].transform.name;
        EnemyHPText.text = HPEnemyEachWave[currentWaveID].ToString();
        EnemyCountText.text = EnemyQuantityEachWave[currentWaveID].ToString();
    }

    public Image Circle;
    void FXControl() {
        Circle.fillAmount = 1 - (WaitCountDown / WaitBetweenWave);
    }

    public void btn_skipCountDown() {
        WaitCountDown = 0;
    }

    public bool isLastWave() {
        
        if (currentWaveID == Enemies.Length - 1) {
            return true;
        }
        else {
            return false;
        }
    }

}

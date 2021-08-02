using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    public int HP;
    public int HPCurrent;
    public GameObject Explosion;
    public GameObject HealthBar;
    public bool isGroundEnemy;


    // Start is called before the first frame update
    void Start()
    {
        HPCurrent = HP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeductHP(int _HP) {
        HPCurrent -= _HP;
        HealthBar.GetComponent<HealthBarControl>().UpdateHealthBar(HPCurrent, HP);
        if (HPCurrent <= 0) {
            Destruction();
        }
    }

    public void Destruction() {
        Reward();
        Explosion.SetActive(true);
        Explosion.transform.parent = null;
        Destroy(transform.parent.gameObject);
        Destroy(Explosion, 2);
        CheckComplete();
    }

    void Reward() {
        float randPercentReward = Random.Range(200, 300);
        int cashReward = (int)(HP * (float)(randPercentReward / 100));
        FindObjectOfType<GameManager>().Cash_Add(cashReward);
    }

    void CheckComplete() {
        
        if (FindObjectOfType<EnemiesManager>().CheckComplete() == true) {
            FindObjectOfType<GameManager>().isWinner = true;
            FindObjectOfType<GameManager>().Active_StatusPanel();
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Controller : MonoBehaviour
{
    public GameObject[] LevelObjects;
    public Sprite[] LevelIcon;
    public GameObject RotatePart;
    public List<GameObject> Targets;
    public GameObject Target;
    public GameObject UpgradeFX;

    [Header("Properties")]
    public bool shootActivate;
    public int strength;
    public float shootRate;

    [Header("Attack Range")]
    public int attackRange;
    public GameObject attackRangeObject;


    void Start()
    {
        InitialLevelInformation();
        //WeaponInitial(Level[currentLvl]);
    }

    // Update is called once per frame
    void Update()
    {
        
        GetAllTargetsInRange();
        CheckWeaponActivating();
        GetClosetTarget();
    }

    void WeaponInitial(int currentLevel) {
        foreach (GameObject child in LevelObjects) {
            child.gameObject.SetActive(false);
        }
        LevelObjects[currentLevel - 1].gameObject.SetActive(true);
    }

    void GetAllTargetsInRange() {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (child.GetComponent<EnemyProperties>().isGroundEnemy == true) { 
                    if (Vector3.Distance(child.transform.position, transform.position) < attackRange)
                    {
                        if (!Targets.Contains(child))
                        {
                            Targets.Add(child);
                        }
                    }
                    else
                    {
                        if (Targets.Contains(child))
                        {
                            Targets.Remove(child);
                        }
                    }
                }
            }
        }
        else {
            Targets.Clear();
            Target = null;
        }
    }

    void CheckWeaponActivating() {
        if (Targets.Count == 0)
        {
            shootActivate = false;
        }
        else {
            RotateToTarget();
            shootActivate = true;
        }
    }

    void GetClosetTarget() {
        if (Targets.Count > 0) {
            if (Targets[0] == null)
            {
                Targets.Remove(Targets[0]);
            }
            else { 
                Target = Targets[0];
            }
        }
    }

    void RotateToTarget() {
        if (Target != null) {
            RotatePart.transform.rotation = Quaternion.Slerp(RotatePart.transform.rotation, Quaternion.LookRotation((Target.transform.position - RotatePart.transform.position).normalized) , Time.deltaTime * 8f);
        }
        
    }

    void setAttackRange() {
        attackRangeObject.transform.localScale = new Vector3(attackRange, 1, attackRange);
    }

    public void setUpdateInformation() {
        FindObjectOfType<UpgradeController>().setCurrentInfor(LevelIcon[Icon[currentLvl]], Strength[currentLvl], Range[currentLvl], Reload[currentLvl]);
        if (currentLvl < Strength.Length - 1)
        {
            FindObjectOfType<UpgradeController>().setNextInfor(LevelIcon[Icon[currentLvl + 1]], Strength[currentLvl + 1], Range[currentLvl + 1], Reload[currentLvl + 1], Cost[currentLvl + 1]);
            
        }
        else {
            FindObjectOfType<UpgradeController>().setNextInforMax(LevelIcon[Icon[currentLvl]]);
        }
    }

    public int getCurrentWeaponLevel() {
        return Level[currentLvl];
    }

    public int getCurrentWeaponStrength() {
        return Strength[currentLvl];
    }

    public void InitialLevelInformation()
    {
        strength = Strength[currentLvl];
        attackRange = Range[currentLvl];
        shootRate = Reload[currentLvl];
        WeaponInitial(Level[currentLvl]);
        setAttackRange();
        setUpdateInformation();
    }

    [Header("Level Properties")]
    
    public int currentLvl = 0;
    int[] Icon = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
    int[] Level = new int[] {1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
    int[] Strength = new int[] {2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
    int[] Range = new int[] {7, 7, 8, 8, 9, 9, 10, 11, 12, 13 };
    float[] Reload = new float[] {0.5f, 0.45f, 0.4f, 0.35f, 0.3f, 0.25f, 0.2f, 0.15f, 0.1f, 0.05f};
    int[] Cost = new int[] { 100, 250, 500, 800, 1100, 1500, 2100, 2800, 3500, 4100 };

    public void setUpdateLevelInformation() {
        if (currentLvl < Strength.Length - 1)
        {
            if (FindObjectOfType<GameManager>().getCurrentCash() >= Cost[currentLvl + 1])
            {
                if (currentLvl < Strength.Length - 1)
                {
                    currentLvl += 1;

                    strength = Strength[currentLvl];
                    attackRange = Range[currentLvl];
                    shootRate = Reload[currentLvl];
                    WeaponInitial(Level[currentLvl]);
                    setAttackRange();
                    setUpdateInformation();
                    UpgradeFX.SetActive(false);
                    UpgradeFX.SetActive(true);
                    FindObjectOfType<GameManager>().Cash_Use(Cost[currentLvl]);
                }
                else
                {
                    setUpdateInformation();
                }
            }
        }
    }

    public string getWeaponInformation(string field) {
        string value = "";
        switch (field) {
            case "Str":
                value = Strength[0].ToString();
                break;
            case "R":
                value = Range[0].ToString();
                break;
            case "Rl":
                value = Reload[0].ToString();
                break;
            case "C":
                value = Cost[0].ToString();
                break;
        }

        return value;
    }
}

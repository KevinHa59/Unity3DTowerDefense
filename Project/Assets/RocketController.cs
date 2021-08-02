using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{

    public GameObject[] LevelObjects;
    public Sprite[] LevelIcon;
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

    [Header("Level Properties")]

    public int currentLvl = 0;
    int[] Icon = new int[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2 };
    int[] Level = new int[] { 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
    int[] Strength = new int[] { 20, 25, 30, 45, 55, 65, 75, 90, 100, 120 };
    int[] Range = new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
    float[] Reload = new float[] { 2.5f, 2.4f, 2.3f, 2f, 1.9f, 1.8f, 1.7f, 1.5f, 1.4f, 1.3f };
    int[] Cost = new int[] { 600, 400, 700, 1800, 2400, 2900, 3700, 4600, 5700, 6600 };


    // Start is called before the first frame update
    void Start()
    {
        InitialLevelInformation();
    }

    // Update is called once per frame
    void Update()
    {
        GetAllTargetsInRange();
        CheckWeaponActivating();
        GetClosetTarget();
    }

    void WeaponInitial(int currentLevel)
    {
        foreach (GameObject child in LevelObjects)
        {
            child.gameObject.SetActive(false);
        }
        LevelObjects[currentLevel - 1].gameObject.SetActive(true);
    }

    void setAttackRange()
    {
        attackRangeObject.transform.localScale = new Vector3(attackRange, 1, attackRange);
    }

    public void setUpdateInformation()
    {
        FindObjectOfType<UpgradeController>().setCurrentInfor(LevelIcon[Icon[currentLvl]], Strength[currentLvl], Range[currentLvl], Reload[currentLvl]);
        if (currentLvl < Strength.Length - 1)
        {
            FindObjectOfType<UpgradeController>().setNextInfor(LevelIcon[Icon[currentLvl + 1]], Strength[currentLvl + 1], Range[currentLvl + 1], Reload[currentLvl + 1], Cost[currentLvl + 1]);

        }
        else
        {
            FindObjectOfType<UpgradeController>().setNextInforMax(LevelIcon[Icon[currentLvl]]);
        }
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


    // Get all enemy in range;
    void GetAllTargetsInRange()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                
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
        else
        {
            Targets.Clear();
            Target = null;
        }
    }
    // Get Closet Target
    void GetClosetTarget()
    {
        if (Targets.Count > 0)
        {
            if (Targets[0] == null)
            {
                Targets.Remove(Targets[0]);
            }
            else
            {
                Target = Targets[0];
            }
        }
    }

    // Check Active Weapon
    void CheckWeaponActivating()
    {
        if (Targets.Count == 0)
        {
            shootActivate = false;
        }
        else
        {
            shootActivate = true;
        }
    }


    // get Weapon information
    public string getWeaponInformation(string field)
    {
        string value = "";
        switch (field)
        {
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
    // Set Weapon Update Information
    public void setUpdateLevelInformation()
    {
        if (currentLvl < Strength.Length - 1) { 
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



    public int getCurrentWeaponLevel()
    {
        return Level[currentLvl];
    }

    public int getCurrentWeaponStrength()
    {
        return Strength[currentLvl];
    }

}

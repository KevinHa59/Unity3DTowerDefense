using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
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

    [Header("Level Properties")]

    public int currentLvl = 0;
    int[] Icon = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] Level = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    int[] Strength = new int[] { 25, 40, 55, 70, 90, 105, 130, 155, 180, 250 };
    int[] Range = new int[] { 7, 7, 7, 8, 8, 8, 9, 9, 9, 12 };
    float[] Reload = new float[] { 4f, 3.9f, 3.8f, 3.7f, 3.6f, 3.5f, 3.4f, 3.3f, 3.2f, 2f };
    int[] Cost = new int[] { 1200, 1500, 1900, 2500, 3100, 3700, 4600, 5300, 6500, 9900 };
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
        GetTarget();
        Shoot();
    }
    public void InitialLevelInformation()
    {
        strength = Strength[currentLvl];
        attackRange = Range[currentLvl];
        shootRate = Reload[currentLvl];
        countDownShoot = shootRate;
        WeaponInitial(Level[currentLvl]);
        setAttackRange();
        setUpdateInformation();
    }
    void WeaponInitial(int currentLevel)
    {
        foreach (GameObject child in LevelObjects)
        {
            child.gameObject.SetActive(false);
        }
        LevelObjects[currentLevel - 1].gameObject.SetActive(true);
    }
    void GetAllTargetsInRange()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (GameObject child in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (child.GetComponent<EnemyProperties>().isGroundEnemy == true)
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
        }
        else
        {
            Targets.Clear();
            Target = null;
        }
    }

    void GetTarget()
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

    void CheckWeaponActivating()
    {
        if (Targets.Count == 0)
        {
            shootActivate = false;
        }
        else
        {
            RotateToTarget();
            shootActivate = true;
        }
    }

    void RotateToTarget()
    {
        if (Target != null)
        {
            RotatePart.transform.rotation = Quaternion.Slerp(RotatePart.transform.rotation, Quaternion.LookRotation((Target.transform.position - RotatePart.transform.position).normalized), Time.deltaTime * 8f);
        }

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

    public int getCurrentWeaponLevel()
    {
        return Level[currentLvl];
    }

    public int getCurrentWeaponStrength()
    {
        return Strength[currentLvl];
    }

    public void setUpdateLevelInformation()
    {
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

    // Shoot
    [Header("Shoot Properties")]
    public GameObject Bullet;
    public Transform ShootPos;
    float countDownShoot = 0;
    public void Shoot() {
        if (shootActivate == true) {
            if (countDownShoot <= 0)
            {
                GameObject _bullet = Instantiate(Bullet);
                _bullet.transform.position = ShootPos.position;
                Rigidbody rig = _bullet.GetComponent<Rigidbody>();
                rig.velocity = ShootPos.transform.forward * 20f;

                _bullet.GetComponent<CannonBulletControl>().Strength = Strength[currentLvl];

                countDownShoot = shootRate;
            }
            else {
                countDownShoot -= Time.deltaTime;
            }
        }
    }
}

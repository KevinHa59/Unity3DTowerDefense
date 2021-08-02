using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShoot : MonoBehaviour
{
    public GameObject MainWeapon;
    public GameObject ShootingPos;
    public GameObject BulletPreb;
    public float shootRate;
    public float shootSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ShootRateInitial();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (MainWeapon.GetComponent<RocketController>().shootActivate == true)
        {
            Shooting();
        }
    }

    float countDownshoot;
    void Shooting()
    {
        if (countDownshoot <= 0)
        {
            ShootRateInitial();
            int weaponLevel = MainWeapon.GetComponent<RocketController>().getCurrentWeaponLevel();
            for (int i = 0; i < weaponLevel; i++) {
                
                Invoke("genRocket", ((float)i/ (float)10) *3 );
            }



            countDownshoot = shootRate;

        }
        else
        {
            countDownshoot -= Time.deltaTime;
        }
    }

    void genRocket() {
        GameObject bullet = (GameObject)Instantiate(BulletPreb);
        bullet.transform.name = posID.ToString();
        bullet.transform.position = ShootingPos.transform.Find(posID.ToString()).position;
        bullet.transform.rotation = ShootingPos.transform.Find(posID.ToString()).rotation;
        bullet.GetComponent<RocketBulletControl>().Target = MainWeapon.GetComponent<RocketController>().Target;
        bullet.GetComponent<RocketBulletControl>().Strength = MainWeapon.GetComponent<RocketController>().getCurrentWeaponStrength();
        bullet.GetComponent<RocketBulletControl>().Station = this.gameObject;
        int weaponLevel = MainWeapon.GetComponent<RocketController>().getCurrentWeaponLevel();
        if (weaponLevel == 1)
        {
            posID = 0;
        }
        else if (weaponLevel == 2)
        {
            if (posID == 0)
            {
                posID = 1;
            }
            else
            {
                posID = 0;
            }
        }
        else if (weaponLevel == 3)
        {
            if (posID == 0)
            {
                posID = 1;
            }
            else if (posID == 1)
            {
                posID = 2;
            }
            else
            {
                posID = 0;
            }
        }
    }


    int posID = 0;

    public GameObject getNewTarget() {
        return MainWeapon.GetComponent<RocketController>().Target;
    }

    void ShootRateInitial()
    {
        shootRate = MainWeapon.GetComponent<RocketController>().shootRate;
    }
}

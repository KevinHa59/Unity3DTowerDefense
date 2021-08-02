using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon1Shooting : MonoBehaviour
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
        if (MainWeapon.GetComponent<Weapon1Controller>().shootActivate == true) { 
            Shooting();
        }
    }

    float countDownshoot;
    void Shooting() {
        if (countDownshoot <= 0)
        {
            GameObject bullet = (GameObject)Instantiate(BulletPreb);
            int weaponLevel = MainWeapon.GetComponent<Weapon1Controller>().getCurrentWeaponLevel();
            float shootRateByLevel = 0;
            if (weaponLevel == 1)
            { // Weapon Level 1
                bullet.transform.position = ShootingPos.transform.position;
                shootRateByLevel = 1;


            }
            else
            { // Weapon Level 2
                bullet.transform.position = getShootPost().position;
                shootRateByLevel = 1.5f;
            }
            bullet.transform.rotation = ShootingPos.transform.rotation;
            Rigidbody bullRig = bullet.GetComponent<Rigidbody>();
            bullRig.velocity = transform.forward * shootSpeed;

            bullet.GetComponent<BulletProperties>().Strength = MainWeapon.GetComponent<Weapon1Controller>().getCurrentWeaponStrength();

            countDownshoot = shootRate/ shootRateByLevel;
            Destroy(bullet, 2f);
        }
        else {
            countDownshoot -= Time.deltaTime;
        }
    }

    int posID = 0;
    Transform getShootPost() {
        if (posID == 1)
        {
            posID = 0;
        }
        else {
            posID = 1;
        }
        Transform pos = ShootingPos.transform.Find(posID.ToString());
        
        return pos;
    }

    void ShootRateInitial() {
        shootRate = MainWeapon.GetComponent<Weapon1Controller>().shootRate;
        if (MainWeapon.GetComponent<Weapon1Controller>().getCurrentWeaponLevel() == 2) {
            shootRate = shootRate/ 2;
        }
    }

}

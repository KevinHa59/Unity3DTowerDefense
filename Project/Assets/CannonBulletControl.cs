using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletControl : MonoBehaviour
{
    public int Strength;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ExploreItSelf", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            AttackEnemiesInRange();
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 2);
        }

        if (collision.transform.tag == "Level")
        {
            AttackEnemiesInRange();
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 2);
        }
    }

    void AttackEnemiesInRange() {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Enemy")) {
            float distance = Vector3.Distance(transform.position, child.gameObject.transform.position);
            int strength = 0;
            if (distance < 3f)
            {
                strength = Strength;
            }
            else if (distance < 4f)
            {
                strength = (int)(Strength * 0.8);
            }
            else if (distance < 6f)
            {
                strength = (int)(Strength * 0.6);
            }
            else if (distance < 7f)
            {
                strength = (int)(Strength * 0.4);
            }
            else if (distance < 8f)
            {
                strength = (int)(Strength * 0.2);
            }
            else {
                strength = 0;
            }
            child.GetComponent<EnemyProperties>().DeductHP(strength);

        }
    }

    void ExploreItSelf() {
        Explosion.SetActive(true);
        Explosion.transform.parent = null;
        Destroy(this.gameObject);
        Destroy(Explosion, 2);
    }
}

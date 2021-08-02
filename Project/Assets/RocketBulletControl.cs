using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBulletControl : MonoBehaviour
{
    public int Strength;
    public GameObject Target;
    public GameObject Explosion;
    public GameObject Station;
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InitalShoot", 1f);
        Invoke("ActiveChasing", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();
    }
    void InitalShoot() {
        rig = GetComponent<Rigidbody>();
        rig.velocity = transform.forward * 10f;

    }
    bool activeTargetChasing = false;
    void RotateToTarget()
    {
        if (activeTargetChasing == true) {

            if (Target != null)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((Target.transform.position - transform.position).normalized), Time.deltaTime * 10f);
            }
            else {
                Target = Station.GetComponent<RocketShoot>().getNewTarget();
                Invoke("NoTargetFound", 0.3f);
            }
            rig.velocity = transform.forward * 15f;
        }

    }

    void NoTargetFound() {
        if (Target == null)
        {
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 2);
        }
    }
    void ActiveChasing() {
        activeTargetChasing = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy") {
            collision.gameObject.GetComponent<EnemyProperties>().DeductHP(Strength);
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 2);
        }

        if (collision.transform.tag == "Level")
        {
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 2);
        }
    }
}

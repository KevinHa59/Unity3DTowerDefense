using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    public int Strength;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyProperties>().DeductHP(Strength);
            Destroy(this.gameObject);
        }
    }
}

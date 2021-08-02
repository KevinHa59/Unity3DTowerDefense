using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveBoomExplosionControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active == false) {
            HideItself();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyProperties>().Destruction();
        }
    }
    public bool active;
    public void HideItself() {
        this.gameObject.SetActive(false);
    }

}

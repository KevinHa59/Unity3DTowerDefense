using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBombControl : MonoBehaviour
{

    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Level") {
            Explosion.SetActive(true);
            Explosion.transform.parent = null;
            Destroy(this.gameObject);
            Destroy(Explosion, 5f) ;
        }
    }
}

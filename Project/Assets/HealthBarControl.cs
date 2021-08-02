using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    public Image HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform.position);
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }

    public void UpdateHealthBar(int current, int max) {
        HealthBar.fillAmount = ((float)current / (float)max);
    }
}

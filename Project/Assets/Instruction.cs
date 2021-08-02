using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    public Image Thumb;
    public Sprite[] Photos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_ActiveInstruction(int id) {
        Thumb.sprite = Photos[id];
    }

    public void btn_Exit() {
        this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
        if (!EventSystem.current.IsPointerOverGameObject()) { 
            FindObjectOfType<GameManager>().setSelectedObject(this.gameObject);
        }
    }
}

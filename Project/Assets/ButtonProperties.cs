using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonProperties : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip Hover;
    public AudioClip Click;
    int fontSize;
    // Start is called before the first frame update
    void Start()
    {
        fontSize = transform.Find("Text").GetComponent<Text>().fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        transform.Find("Text").GetComponent<Text>().fontSize = fontSize;
        transform.Find("Text").GetComponent<Text>().fontStyle = FontStyle.Normal;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.Find("Text").GetComponent<Text>().fontSize = fontSize + 3;
        transform.Find("Text").GetComponent<Text>().fontStyle = FontStyle.Bold;
        GetComponent<AudioSource>().clip = Hover;
        GetComponent<AudioSource>().Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<AudioSource>().clip = Click;
        GetComponent<AudioSource>().Play();
    }
    //void OnPointer

}

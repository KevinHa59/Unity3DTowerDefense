using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int ButtonID;
    public Text Level;
    public Text Wave;
    public Image Thumb;
    public Sprite Normal;
    public Sprite HighLight;
    public string enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitialSetup(string level, int wave, Sprite thumb) {
        Level.text = level;
        Wave.text = "Waves: " + wave;
        Thumb.sprite = thumb;
    }

    public void btn_LevelClick() {
        GetComponent<AudioSource>().Play();
        FindObjectOfType<DashboardController>().LevelSelectedID = ButtonID;
        FindObjectOfType<DashboardController>().UpdateLevelDetail();
        //Debug.Log(transform.parent.transform.childCount);
        foreach (Transform child in transform.parent.transform)
        {
            //child.GetComponent<LevelButtonControl>().HighLight.SetActive(false);
            child.GetComponent<Image>().sprite = child.GetComponent<LevelButtonControl>().Normal;
        }
        GetComponent<Image>().sprite = HighLight;
        //HighLight.SetActive(true);
        //GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<AudioSource>().Play();
        GetComponent<Animator>().Play("LevelButtonHover",0,0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().Play("LevelButtonExit", 0, 0);
    }
}

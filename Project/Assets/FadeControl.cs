using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour
{
    public GameObject Fade;
    // Start is called before the first frame update
    void Start()
    {
        ActiveFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveFadeIn() {
        GetComponent<Animator>().Play("FadeIn", 0, 0);
        Invoke("HideFade", 1f);
    }
    void HideFade() {
        Fade.SetActive(false);
    }
    string sceneName;
    public void ActiveFadeOut(string _sceneName) {
        Fade.SetActive(true);
        GetComponent<Animator>().Play("FadeOut", 0, 0);
        sceneName = _sceneName;
        Invoke("SwitchScene", 1);
    }

    void SwitchScene() {
        SceneManager.LoadScene(sceneName);
    }
}

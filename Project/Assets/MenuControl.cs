using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void btn_Play() {
        FindObjectOfType<FadeControl>().ActiveFadeOut("Dashboard");
        //SceneManager.LoadScene("Dashboard");
    }
}

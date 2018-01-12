using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnPause : MonoBehaviour {
    public static bool isClickBtnResume = false;

    public Button BtnResume;
    public Text BuyText;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnButtonClicked()
    {
        isClickBtnResume = !isClickBtnResume;
        if (isClickBtnResume)
        {
            GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Resume";
     
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(i - 1);             

        }
        else
        {
            GetComponentsInChildren<UnityEngine.UI.Text>()[0].text = "Pause";
        }
        

    }
}

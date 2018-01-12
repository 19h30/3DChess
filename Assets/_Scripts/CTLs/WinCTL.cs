using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCTL : MonoBehaviour {

    public Text winner;
    public Button okBtn;
    GameObject music;

    [SerializeField]
    AudioClip winSound;

    AudioSource src;

    void Awake()
    {
        src = GameObject.Find("WinMusic").GetComponent<AudioSource>();
        src.volume = PlayerPrefs.GetFloat("volume");
        src.PlayOneShot(winSound);

        music = GameObject.Find("Music");
        Destroy(music);
        winner.text = PlayerPrefs.GetString("winner") + " won!!!";
    }

    void OnEnable()
    {
        okBtn.onClick.AddListener(delegate { OnClick(); });
    }

    public void OnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}

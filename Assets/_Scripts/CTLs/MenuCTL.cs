using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCTL : MonoBehaviour {

    GameObject audioSource;
    public static int indexMenu = -1;

    void Awake()
    {
        audioSource = GameObject.Find("Music");
        DontDestroyOnLoad(audioSource);
    }

    public void PlayGame()
    {
        indexMenu = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void LoadGame()
    {
        indexMenu = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCTL : MonoBehaviour {
    public static int _MenuState = -1;
    // 0: Start
    // 1: Save
    // 2: Load
    // 3: Options
    // 4: Exit
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);       
    }

    // Update is called once per frame
    void Update()
    {
        if (BtnPause.isClickBtnResume)
        {
            Debug.Log("pause");
        }
        else
        {
            Debug.Log("first menu");
        }
    }
    public void PlayGame()
    {
        _MenuState = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
    public void SaveGame()
    {
        _MenuState = 1;
        Debug.Log(ChessBoard.ListPiecesInfoToJson.player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame()
    {
        _MenuState = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
    }
    public void QuitGame()
    {
        _MenuState = 4;
        Debug.Log("quit");
        Application.Quit();
    }
}

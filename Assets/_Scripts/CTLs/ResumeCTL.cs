using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeCTL : MonoBehaviour {

    GameObject audioSource;
    public static int indexMenu = -1;
    private bool isShowing = true;
    //GameObject p;

    void Awake()
    {
        audioSource = GameObject.Find("Music");
        DontDestroyOnLoad(audioSource);
      //  p = GameObject.Find("Canvas_resume");
      //  p.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {           
            if (Time.timeScale == 1)
            {

                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }
    }

    public void NewGame()
    {
        BaseGameCTL.Current.removeAllPiceces();
        ChessBoard.Current.InitChessBoard();       
        ChessBoard.Current.InitChessPieces();
    }
    public void LoadGame()
    {
        BaseGameCTL.Current.removeAllPiceces();
        ChessBoard.Current.LoadGame();
    }
    public void SaveGame()
    {
        ChessBoard.Current.SaveGame();
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}

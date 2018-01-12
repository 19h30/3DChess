using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResumeCTL : MonoBehaviour {

    GameObject audioSource;
    public static int indexMenu = -1;
    //private bool isShowing = true;
    GameObject resume;
    GameObject board;
    GameObject text;
    public Button resumeButton;
    GameObject music;

    void Awake()
    {
        audioSource = GameObject.Find("Music");
        DontDestroyOnLoad(audioSource);
        resume = GameObject.Find("ResumeMenu");
        board = GameObject.Find("ChessBoard");
        text = GameObject.Find("GUI");
    }

    void OnEnable()
    {
        resumeButton.onClick.AddListener(delegate { ResumeGame(); });
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        BaseGameCTL.Current.GameState = EGameState.GAME_OVER;
        music = GameObject.Find("Music");
        Destroy(music);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        resume.SetActive(BaseGameCTL.isShowing);
        board.SetActive(!BaseGameCTL.isShowing);
        text.SetActive(!BaseGameCTL.isShowing);
        BaseGameCTL.isShowing = !BaseGameCTL.isShowing;

    }
    public void SaveGame()
    {
        ChessBoard.Current.SaveGame();
        ResumeGame();
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

}

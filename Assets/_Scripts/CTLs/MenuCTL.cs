using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCTL : MonoBehaviour {

    GameObject audioSource;

    void Awake()
    {
        audioSource = GameObject.Find("Music");
        DontDestroyOnLoad(audioSource);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(ChessBoard.Current);
        string fullPath = Application.persistentDataPath + "/Chessboard.json";
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        File.WriteAllText(fullPath, json);
        
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}

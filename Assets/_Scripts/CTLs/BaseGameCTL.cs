﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseGameCTL : MonoBehaviour
{
    public Text txt_current_player;

    public static BaseGameCTL Current;

    public const float TIME_TO_THINK = 10;
    public Text txt_time;
    public float _time = 0;
    private bool timeOutIsCalled = false;

    private EGameInput _GameInput;
    private EGameState _gameState;
    private EPlayer _CurrentPlayer;
    public EPlayer CurrentPlayer
    {
        get
        {
            return _CurrentPlayer;
        }

        set
        {
            _CurrentPlayer = value;
        }
    }

    #region propetity
    public EGameState GameState
    {
        get { return _gameState; }
        set { _gameState = value; }
    }

    public EGameInput GameInput
    {
        get
        {
            return _GameInput;
        }

        set
        {
            _GameInput = value;
        }
    }
    #endregion

    void Awake()
    {
        Current = this;
        CurrentPlayer = EPlayer.WHITE;
        GameState = EGameState.PLAYING;
        GameInput = EGameInput.KEYBOARD;
    }

    void Start()
    {
       // StartCoroutine(UpdateTime());
    }

    void Update()
    {
        if (GameState == EGameState.PLAYING)
        {
            _time += Time.deltaTime;
            txt_time.text = ((int)_time).ToString();

            if (_time > TIME_TO_THINK && timeOutIsCalled == false)
            {
              
                TimeIsOut();
            }
        }
    }

    private IEnumerator UpdateTime()
    {
       while(true)
       {
           if (GameState == EGameState.PLAYING)
           {
               txt_time.text = ((int)_time).ToString();
           }
           yield return new WaitForSeconds(0.5f);
       }
    }

    private void TimeIsOut()
    {
        timeOutIsCalled = true;
        
        SwitchTurn();
    }

    /// <summary>
    /// Chuyển lượt chơi
    /// </summary>
    public void SwitchTurn()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (ChessBoard.Current.Cells[i][j].State != ECellState.NORMAL)
                    ChessBoard.Current.Cells[i][j].SetCellState(ECellState.NORMAL);
            }
        }

        timeOutIsCalled = false;
        _time = 0;
        CurrentPlayer = CurrentPlayer == EPlayer.WHITE ? EPlayer.BLACK : EPlayer.WHITE;

        switch (CurrentPlayer)
        {
            case EPlayer.BLACK:
                txt_current_player.text = "BLACK";
                txt_current_player.color = Color.grey;
                break;
            case EPlayer.WHITE:
                txt_current_player.text = "WHITE";
                txt_current_player.color = Color.white;
                break;
            default:
                break;
        }
    
    }

    /// <summary>
    /// Kiểm tra trạng thái của bàn cờ, xem đã kết thúc hay chưa
    /// </summary>
    public EGameState CheckGameState()
    {

        return EGameState.PLAYING;
    }

    public void GameOver(EPlayer winPlayer)
    {
        GameState = EGameState.GAME_OVER;
        Debug.Log("WinPlayer : " + winPlayer);
    }
    public void removeAllPiceces()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (ChessBoard.Current.Cells[i][j].CurrentPiece != null)
                {
                    GameObject.Destroy(ChessBoard.Current.Cells[i][j].CurrentPiece.gameObject);
                    ChessBoard.Current.Cells[i][j].SetPiece(null);
                    CurrentPlayer = EPlayer.BLACK;            
                    SwitchTurn();
                }

            }
        }
    }    

}

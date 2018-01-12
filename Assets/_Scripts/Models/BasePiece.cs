using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public abstract class BasePiece : MonoBehaviour
{
    protected List<Cell> _targetedCells = new List<Cell>();
    protected Cell _currentCell;

    [SerializeField]
    private Vector3 offsetPosition;
    public PieceInfo Info { get; private set; }

    [SerializeField]
    protected EPlayer _player;
    public EPlayer Player { get { return _player; } protected set { _player = value; } }

    public CLocation Location { get; private set; }

    //AudioSource audioSource;
    [SerializeField]
    AudioClip attackSound;
    [SerializeField]
    AudioClip beSelectedSound;
    [SerializeField]
    AudioClip moveSound;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume");
    }

    public void playAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void playMoveSound()
    {
        audioSource.PlayOneShot(moveSound);
    }

    public void playSelectedSound()
    {
        audioSource.PlayOneShot(beSelectedSound);
    }

    public void SetInfo(PieceInfo info, Cell newCell)
    {
        this.Info = info;
        SetNewLocation(newCell);
    }
    private int setPosCount = 0;
    protected void SetNewLocation(Cell newCell)
    {
        setPosCount++;
        if(setPosCount > 1)
            this._currentCell.SetPiece(null);
        
        this._currentCell = newCell;
        newCell.SetPiece(this);
        this.Location = newCell.Location;
        
        this.transform.DOJump(offsetPosition + new Vector3(Location.X * ChessBoard.Current.CELL_SIZE, 0,
            Location.Y * ChessBoard.Current.CELL_SIZE), 2, 1, 0.75f);

    }

    /// <summary>
    /// Di chuyển tới ô targeted
    /// </summary>
    public virtual void Move(Cell targetedCell)
    {
        //1. Di chuyển đơn thuần
        this.SetNewLocation(targetedCell);

        //2. Ăn
        BeUnselected();

        playMoveSound();

        //3. Đổi lượt chơi
        BaseGameCTL.Current.SwitchTurn();
    }

    /// <summary>
    /// Thực hiện tấn công vào quân cờ ở trong ô targeted cell
    /// </summary>
    public virtual void Attack(Cell targetedCell)
    {
        ///Với quân địch
        ///- Đánh dấu là đã chết
        targetedCell.CurrentPiece.BeAttackedBy(this);

        ///Với quân ta:
        ///- Di chuyển đến vị trí mới
        _currentCell.SetCellState(ECellState.NORMAL);
        this.SetNewLocation(targetedCell);
        BeUnselected();

        //sound
        playAttackSound();
        BaseGameCTL.Current.SwitchTurn();
    }

    /// <summary>
    /// Thực hiện việc quân này bị ăn
    /// </summary>
    public virtual void BeAttackedBy(BasePiece enemy)
    {
        GameObject.Destroy(this.gameObject);
        _currentCell.SetPiece(null);
    }


    /// <summary>
    /// tìm các ô TARGETED
    /// </summary>
    public abstract void BeSelected();

    /// <summary>
    /// Loại bỏ trạng thái các quân cờ đang ở trạng thái TARGETED
    /// </summary>
    public void BeUnselected()
    {
        foreach (var item in _targetedCells)
            item.SetCellState(ECellState.NORMAL);
        _targetedCells.Clear();
    }
}

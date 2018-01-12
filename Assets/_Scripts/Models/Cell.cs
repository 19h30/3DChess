using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    private Transform cellSelectedObj;

    public BasePiece CurrentPiece { get; private set; }

    private ECellColor _color;
    public ECellColor Color
    {
        get { return _color; }
        set
        {
            _color = value;

            switch (_color)
            {
                case ECellColor.BLACK:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.BlackCellMaterial;
                    break;
                case ECellColor.WHITE:
                    GetComponent<Renderer>().material = ResourcesCTL.Instance.WhiteCellMaterial;
                    break;
                default:
                    break;
            }
        }
    }

    //Vị trí tọa độ trên bàn cờ 8x8
    public CLocation Location { get; private set; }
    private ECellState _state;

    public void SetLocation(CLocation location)
    {
        this.Location = location;
    }

    public ECellState State
    {
        get { return _state; }
        private set
        {
            _state = value;

            switch (_state)
            {
                case ECellState.NORMAL:
                    cellSelectedObj.gameObject.SetActive(false);
                    break;

                case ECellState.SELECTED:
                    cellSelectedObj.gameObject.SetActive(true);
                    CurrentPiece.BeSelected();
                    break;

                case ECellState.TARGETED:
                    cellSelectedObj.gameObject.SetActive(true);
                    break;
            }
        }
    }


    public float SIZE
    {
        get
        {
            return GetComponent<Renderer>().bounds.size.x;
        }
    }

    void Awake()
    {
        cellSelectedObj = this.transform.GetChild(1);
    }

    void Start()
    {
        //  State = ECellState.NORMAL;
    }


    /// <summary>
    /// Chỉ có thể thay đổi được cell state thông qua hàm SetCellState
    /// </summary>
    public void SetCellState(ECellState cellState)
    {
        this.State = cellState;
    }

    /// <summary>
    /// Truyền vào null nếu như quân cờ di chuyển từ ô này đi ra
    /// </summary>
    /// <param name="piece"></param>
    public void SetPiece(BasePiece piece)
    {
        this.CurrentPiece = piece;
    }

    /// <summary>
    /// Thực hiện di chuyển quân cở ở ô hiện tại
    /// </summary>
    public void MakeAMove(Cell targetedCell)
    {
       // Debug.Log("MakeAMove : " + targetedCell.Location);
        CurrentPiece.Move(targetedCell);
        State = ECellState.NORMAL;
        CurrentPiece = null;
    }
}

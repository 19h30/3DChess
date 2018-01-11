using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Current;

    public GameObject cellPrefap;
    public LayerMask CellLayerMask = 0;
    private Cell _currentSelectedCell = null;

    private float cell_size = -1;
    public float CELL_SIZE
    {
        get
        {
            if (cell_size < 0)
                cell_size = cellPrefap.GetComponent<Cell>().SIZE;
            return cell_size;
        }
    }


    private Vector3 basePosition = Vector3.zero;

    private Cell[][] _cells;
    public Cell[][] Cells { get { return _cells; } }

    private List<BasePiece> pieces;

    void Awake()
    {
        Current = this;
    }

    void Start()
    {
        InitChessBoard();
        InitChessPieces();
    }


    void Update()
    {
        if (BaseGameCTL.Current.GameState == EGameState.PLAYING)
        {
            CheckUserInput();
        }
    }



    private void CheckUserInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            ///Chỉ xử lý input trong 2 trường hợp
            ///2. Chỏ vào ô đang được targeted:
            ///2.1 : Ô được targeted trống không
            ///2.2 : Ô được targeted có quân cờ của quân địch
            ///
            ///1. Bắt đầu lượt đi mới của đối thủ:
            ///     CHọn vào 1 ô chứa quân cờ cùng màu với người đang chơi

            if (Physics.Raycast(ray, out hit, 1000, CellLayerMask.value))
            {
                Cell newCell = hit.collider.GetComponent<Cell>();

                switch (newCell.State)
                {
                    ///Tức là chọn vào 1 ô bình thường 
                    ///=> Ô đấy phải là quân của người đang chơi
                    ///Thì mới có thể thực hiện bước chọn quân
                    case ECellState.NORMAL:
                        if (newCell.CurrentPiece != null && newCell.CurrentPiece.Player == BaseGameCTL.Current.CurrentPlayer)
                        {
                            //Nếu có quân cờ được chọn trước đó
                            //Thì chuyển trạng thái của ô đó và quân cờ ở ô đó (nếu có)
                            //về trạng thái bình thường
                            if (_currentSelectedCell != null)
                            {
                                _currentSelectedCell.SetCellState(ECellState.NORMAL);
                                if (_currentSelectedCell.CurrentPiece != null)
                                    _currentSelectedCell.CurrentPiece.BeUnselected();
                            }

                            _currentSelectedCell = newCell;
                            _currentSelectedCell.SetCellState(ECellState.SELECTED);
                        }
                        break;

                    case ECellState.TARGETED:
                        ///1. Nếu trống thì sẽ là di chuyenr bình thường
                        ///2. Còn nếu không truống, mà là quân địch thì sẽ ăn
                        ///3. Các trường hợp còn lại thì ko làm gì
                        //Chạm vào ô được target và không có quân cờ
                        if (newCell.CurrentPiece == null)
                        {
                            //Di chuyển bình thường
                            _currentSelectedCell.MakeAMove(newCell);
                        }
                        else
                        {
                            ///Quân cờ bị targeted là của quân địch
                            if (newCell.CurrentPiece.Player != BaseGameCTL.Current.CurrentPlayer)
                            {
                                _currentSelectedCell.CurrentPiece.Attack(newCell);    
                            }
                        }
                        break;

                }

            }
        }

        //Check vị trí con trỏ chuột

        //Bắt lấy ô hiện tại

        //Khi không có thì thôi
    }

    [ContextMenu("InitChessBoard")]
    public void InitChessBoard()
    {
        basePosition = Vector3.zero + new Vector3(-3.5f * CELL_SIZE, 0, 0);
        _cells = new Cell[8][];
        for (int i = 0; i < 8; i++)
            _cells[i] = new Cell[8];

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject c = GameObject.Instantiate(cellPrefap, CanculatePosition(i, j),
                    Quaternion.identity) as GameObject;

                c.transform.parent = this.transform.GetChild(0);

                _cells[i][j] = c.GetComponent<Cell>();
                _cells[i][j].SetLocation(new CLocation(i, j));

                if ((i + j) % 2 == 0)
                    _cells[i][j].Color = ECellColor.BLACK;
                else _cells[i][j].Color = ECellColor.WHITE;
            }
        }


    }

    [ContextMenu("InitChessPieces")]
    public void InitChessPieces()
    {
        pieces = new List<BasePiece>();

        List<PieceInfo> listInfo = new List<PieceInfo>();

        //White
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_1", Path = "Pieces/W_PAWN", X = 0, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_2", Path = "Pieces/W_PAWN", X = 1, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_3", Path = "Pieces/W_PAWN", X = 2, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_4", Path = "Pieces/W_PAWN", X = 3, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_5", Path = "Pieces/W_PAWN", X = 4, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_6", Path = "Pieces/W_PAWN", X = 5, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_7", Path = "Pieces/W_PAWN", X = 6, Y = 1 });
        listInfo.Add(new PieceInfo() { Name = "W_PAWN_8", Path = "Pieces/W_PAWN", X = 7, Y = 1 });

        listInfo.Add(new PieceInfo() { Name = "W_ROOK_1", Path = "Pieces/W_ROOK", X = 0, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_ROOK_2", Path = "Pieces/W_ROOK", X = 7, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_KNIGHT_1", Path = "Pieces/W_KNIGHT", X = 1, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_KNIGHT_2", Path = "Pieces/W_KNIGHT", X = 6, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_BISHOP_1", Path = "Pieces/W_BISHOP", X = 2, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_BISHOP_2", Path = "Pieces/W_BISHOP", X = 5, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_KING", Path = "Pieces/W_KING", X = 3, Y = 0 });
        listInfo.Add(new PieceInfo() { Name = "W_QUEEN", Path = "Pieces/W_QUEEN", X = 4, Y = 0 });

        //White
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_1", Path = "Pieces/B_PAWN", X = 0, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_2", Path = "Pieces/B_PAWN", X = 1, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_3", Path = "Pieces/B_PAWN", X = 2, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_4", Path = "Pieces/B_PAWN", X = 3, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_5", Path = "Pieces/B_PAWN", X = 4, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_6", Path = "Pieces/B_PAWN", X = 5, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_7", Path = "Pieces/B_PAWN", X = 6, Y = 6 });
        listInfo.Add(new PieceInfo() { Name = "B_PAWN_8", Path = "Pieces/B_PAWN", X = 7, Y = 6 });

        listInfo.Add(new PieceInfo() { Name = "B_ROOK_1", Path = "Pieces/B_ROOK", X = 0, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_ROOK_2", Path = "Pieces/B_ROOK", X = 7, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_KNIGHT_1", Path = "Pieces/B_KNIGHT", X = 1, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_KNIGHT_2", Path = "Pieces/B_KNIGHT", X = 6, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_BISHOP_1", Path = "Pieces/B_BISHOP", X = 2, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_BISHOP_2", Path = "Pieces/B_BISHOP", X = 5, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_KING", Path = "Pieces/B_KING", X = 3, Y = 7 });
        listInfo.Add(new PieceInfo() { Name = "B_QUEEN", Path = "Pieces/B_QUEEN", X = 4, Y = 7 });

        foreach (var info in listInfo)
        {
            GameObject GO = GameObject.Instantiate<GameObject>(ResourcesCTL.Instance.GetGameObject(info.Path));
            GO.transform.parent = this.transform.GetChild(1);
            GO.name = info.Name;

            BasePiece p = GO.GetComponent<BasePiece>();
            p.SetInfo(info, _cells[info.X][info.Y]);
            pieces.Add(p);

        }

    }

    public Vector3 CanculatePosition(int i, int j)
    {
        return basePosition + new Vector3(i * CELL_SIZE, 0, j * CELL_SIZE);
    }
}

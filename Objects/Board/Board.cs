using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The class containing the board and their appropriate helper functions.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The data structure holding all of the data.
        /// </summary>
        public ObservableCollection<ObservableCollection<Square>> pieces = new ObservableCollection<ObservableCollection<Square>>();
        /// <summary>
        /// The maximum xSize. By default, its set to 8.
        /// </summary>
        public int xSize { get; private set; } = 8;
        /// <summary>
        /// The maximum ySize. By default, its set to 8.
        /// </summary>
        public int ySize { get; private set; } = 8;
        /// <summary>
        /// The default constructor. Assumes a normal game, so creates an 8x8 board with a default starting position.
        /// </summary>
        public Board()
        {
            Setup();       
            //Setup default pieces.
            DefaultLayout();
        }
        /// <summary>
        /// Creates a board of a defined size.
        /// </summary>
        /// <param name="x">The maximum xSize.</param>
        /// <param name="y">The maximum ySize.</param>
        /// <exception cref="ArgumentException">Is thrown when xSize or ySize <= 0</exception>
        public Board(int x, int y)
        {
            if (x <= 0 || y <= 0)
            {
                throw new ArgumentException("xSize and ySize should be non-zero, positive integers.");
            }
            this.xSize = x;
            this.ySize = y;
            Setup();
        }
        /// <summary>
        /// The indexer for the data inside the class.
        /// </summary>
        /// <param name="y">The y index of the data to be accessed or changed.</param>
        /// <param name="x">The x index of the data to be accessed or changed</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">Is thrown when the any of the indexers are < 0, or >= the max size for that respective dimension</exception>
        public Square this[int y, int x]
        {
            get
            {

                if (y >= 0 && y < ySize && x >= 0 && x < xSize)
                {
                    //If the index is inside the bounds, we return the value.
                    return pieces[y][x];
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {

                if (y >= 0 && y < ySize && x >= 0 && x < xSize)
                {
                    //If the index is inside the bounds, we allow them to change the value.
                    pieces[y][x] = value;
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }

            }
        }
        public Square this[Vector2 pos]
        {
            get
            {
                if (pos.Y >= 0 && pos.Y < ySize && pos.X >= 0 && pos.X < xSize)
                {
                    //If the index is inside the bounds, we return the value.
                    return pieces[(int)pos.Y][(int)pos.X];
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (pos.Y >= 0 && pos.Y < ySize && pos.X >= 0 && pos.X < xSize)
                {
                    //If the index is inside the bounds, we allow them to change the value.
                    pieces[(int)pos.Y][(int)pos.X] = value;
                }
                else
                {
                    //Else we throw an appropriate exception.
                    throw new IndexOutOfRangeException();
                }
            }
        }

        private void Setup()
        {
            for (int i = 0; i < xSize; i++)
            {
                pieces.Add(new ObservableCollection<Square>());
                for (int j = 0; j < ySize; j++)
                {
                    //Adding elements to our enumerable to simulate array indexing.
                    pieces[i].Add(new Square(new Vector2(j,i)));
                }
            }
        }
        /// <summary>
        /// Adds the default starting position to the board.
        /// </summary>
        /// <exception cref="Exception">This exception will be ran if the xSize and ySize aren't 8.</exception>
        public void DefaultLayout()
        {
            if (xSize == 8 && ySize == 8)
            {
                for (int i = 1; i < 8; i += 5)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        this[i, j].piece = new Pawn(i == 6 ? PieceColour.White : PieceColour.Black);
                    }
                }
                for (int i = 0; i < 8; i += 7)
                {
                    PieceColour pieceColour = (i == 7? PieceColour.White : PieceColour.Black);
                    this[i, 0].piece = new Rook(pieceColour);
                    this[i, 7].piece = new Rook(pieceColour);
                    this[i,1].piece = new Knight(pieceColour);
                    this[i, 6].piece = new Knight(pieceColour);
                    this[i,2].piece = new Bishop(pieceColour);
                    this[i, 5].piece = new Bishop(pieceColour);
                    this[i, 3].piece = new Queen(pieceColour);
                    this[i, 4].piece = new King(pieceColour);
                }
            }
            else
            {
                throw new Exception("The xSize and ySize must be 8 to perform a default setup");
            }
        }
        public void Move(Move move)
        {
            Square startingSquare = this[(int)move.startPosition.Y, (int)move.startPosition.X];
            this[(int)move.endPosition.Y, (int)move.endPosition.X].piece = startingSquare.piece;
            startingSquare.piece = null;
        }
        public bool IsOnBoard(Vector2 pos)
        {
            if (pos.X < 0 || pos.X >= xSize)
            {
                return false;
            }
            else if (pos.Y < 0 || pos.Y >= ySize)
            {
                return false;
            }
            return true;
        }
        public MoveType MoveTypeForMove(PieceColour colour, Vector2 pos)
        {
            PieceColour enemyColour = (colour == PieceColour.White ? PieceColour.Black : PieceColour.White);
            
            if (this[pos].piece is null)
            {
                return MoveType.Default;
            }
            else if (this[pos].piece.colour == enemyColour)
            {
                return MoveType.Capture;
            }
            //Temp
            return MoveType.Invalid;
        }
        public void LinearMove(PieceColour colour, Vector2 pos, Vector2 direction, ref List<Move> validMoves)
        {
            int mag = 1;
            while (true)
            {
                Vector2 targetPos = pos + mag * direction;
                if (!IsOnBoard(targetPos))
                {
                    break;
                }
                MoveType moveType = MoveTypeForMove(colour, targetPos);
                if (moveType == MoveType.Invalid)
                {
                    break;
                }
                validMoves.Add(new Move(pos, targetPos, moveType));
                if (moveType == MoveType.Capture)
                {
                    break;
                }
                mag++;
            }
        }
    
    }
    /// <summary>
    /// The class used as an interface for bindings with the board UI element.
    /// </summary>
    public class Square : INotifyPropertyChanged
    {
        /// <summary>
        /// The piece on the square. It is nullable since a square can contain no pieces.
        /// </summary>
        public Piece? piece
        {
            get
            {
                return _piece;
            }
            set
            {
                _piece = value;
                OnPropertyChanged();
            }
        }

        private Piece? _piece;

        /// <summary>
        /// The position of the square on the parent board.
        /// </summary>
        public Vector2 pos { get; set; }
        /// <summary>
        /// A property used for binding when displaying legal moves.
        /// </summary>
        public Move? targetMove
        {
            get
            {
                return _targetMove;
            }
            set
            {
                _targetMove = value;
                OnPropertyChanged();
            }
        }

        private Move? _targetMove;
        /// <summary>
        /// The constructor used when declaring a square object.
        /// </summary>
        /// <param name="pos"><inheritdoc cref="Square.pos"/></param>
        public Square(Vector2 pos)
        {
            this.pos = pos;
        }
        /// <summary>
        /// An optional constructor used to fully define a square.
        /// </summary>
        /// <param name="pos"><inheritdoc cref="Square.pos"/></param>
        /// <param name="piece"><inheritdoc cref="Square.piece"/></param>
        public Square(Vector2 pos, Piece piece)
        {
            this.piece = piece;
            this.pos = pos;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

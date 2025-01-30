using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The class containing the board and their appropriate helper functions.
    /// </summary>
    public class Board : INotifyPropertyChanged
    {
        /// <summary>
        /// The data structure holding all of the data.
        /// </summary>
        public ObservableCollection<ObservableCollection<Square>> pieces = new ObservableCollection<ObservableCollection<Square>>();

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The maximum xSize. By default, its set to 8.
        /// </summary>
        public int xSize { get; private set; } = 8;
        /// <summary>
        /// The maximum ySize. By default, its set to 8.
        /// </summary>
        public int ySize { get; private set; } = 8;
        /// <summary>
        /// The previously played move. Used internally for tracking en passant.
        /// </summary>
        public Move lastPlayedMove { get;private set; } = new Move(new Vector2(-1,-1),new Vector2(-1,-1),MoveType.Invalid);
        private TimeSpan _whiteTime;
        public TimeSpan whiteTime
        {
            get
            {
                return _whiteTime;
            }
            set
            {
                _whiteTime = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan _blackTime;
        public TimeSpan blackTime 
        {
            
            get
            {
                return _blackTime;
            }
            set
            {
                _blackTime = value;
                OnPropertyChanged();
            }
            
        }
        
        public Vector2 whiteKingPos { get; private set; } = new Vector2(4, 7);
        public Vector2 blackKingPos { get; private set; } = new Vector2(4, 0);

        /// <summary>
        /// The default constructor. Assumes a normal game, so creates an 8x8 board with a default starting position.
        /// </summary>
        public Board()
        {
            Setup();       
            //Setup default pieces.
            DefaultLayout();

            _whiteTime = new TimeSpan(0,0, 5, 0,0);
            _blackTime = new TimeSpan(0,0, 0, 10,0);


            whiteKingPos = new Vector2(4, 7);
            blackKingPos = new Vector2(4, 0);
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

        public Board(Board board)
        {
            xSize = board.xSize;
            ySize = board.ySize;

            for(int i =0; i <  board.xSize; i++)
            {
                pieces.Add(new ObservableCollection<Square>());
                foreach (Square square in board.pieces[i])
                {
                    pieces[i].Add(new Square(square));
                }
            }

            lastPlayedMove = board.lastPlayedMove;
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
        public void Move(Move move,bool testing)
        {
            if (this[move.startPosition].piece is King && !testing)
            {
                if (this[move.startPosition].piece.colour == PieceColour.White)
                {
                    whiteKingPos = move.endPosition;
                }
                else
                {
                    blackKingPos = move.endPosition;
                }
            }
            if (move.moveType == MoveType.ShortCastling)
            {
                Square rookSquare = this[(int)move.startPosition.Y,7];
                this[move.endPosition - new Vector2(1,0)].piece = rookSquare.piece;
                rookSquare.piece = null;
            }
            else if (move.moveType == MoveType.LongCastling)
            { 
                Square rookSquare = this[(int)move.startPosition.Y,0];
                this[move.endPosition + new Vector2(1, 0)].piece = rookSquare.piece;
                rookSquare.piece = null;
            }
            else if (move.moveType == MoveType.EnPassant)
            {
                this[lastPlayedMove.endPosition].piece = null;
            }
            else if((move.moveType == MoveType.Promotion || move.moveType == MoveType.PromotionCapture) && !testing)
            {
                Promotion promotion = new Promotion(this[move.startPosition].piece.colour);
                promotion.ShowDialog();
                (Type, PieceColour) chosenValue = promotion.chosenType;
                this[move.startPosition].piece = PromotionSwitch(chosenValue);
                
            }

            Square startingSquare = this[move.startPosition];
            this[move.endPosition].piece = startingSquare.piece;
            startingSquare.piece = null;
            lastPlayedMove = move;
            
        }
        
        private Piece PromotionSwitch((Type,PieceColour) value)
        {
            Type pieceType = value.Item1;
            if (pieceType == typeof(Queen))
            {
                return new Queen(value.Item2);
            }
            else if (pieceType == typeof(Bishop))
            {
                return new Bishop(value.Item2);
            }
            else if (pieceType == typeof(Knight))
            {
                return new Knight(value.Item2);
            }
            else
            {
                return new Rook(value.Item2);
            }
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
        /// <summary>
        /// Used for the movement of the bishop, rook and queen since they all check along a given direction vector.
        /// </summary>
        /// <param name="colour">The colour of the piece whose moves are being checked.</param>
        /// <param name="pos">The position of the piece whose moves are being checked.</param>
        /// <param name="direction">The direction vector to check along</param>
        /// <param name="validMoves">The valid moves list to append the results to.</param>
        public void LinearMove(PieceColour colour, Vector2 pos, Vector2 direction, ref List<Move> validMoves)
        {
            //Declaring the starting "extension" or magnitude (in the case of vertical or horizontal vectors)
            int mag = 1;
            while (true)
            {
                //Computing the position to check. This is the starting position + the extended direction vector.
                Vector2 targetPos = pos + mag * direction;
                //If this move isn't on the board, there isn't any reason to check anymore in this direction so we terminate the loop here.
                if (!IsOnBoard(targetPos))
                {
                    break;
                }
                //We then compute the MoveType for this move.
                MoveType moveType = MoveTypeForMove(colour, targetPos);
                //If the move targets an allied piece, there isn't any reason to check in this direction anymore since you can't travel through pieces.
                if (moveType == MoveType.Invalid)
                {
                    break;
                }
                //We then add the move, since in all other cases we should.
                validMoves.Add(new Move(pos, targetPos, moveType));
                //However, if the move targets an enemy piece, we should still terminate the loop due to being unable to move through pieces.
                if (moveType == MoveType.Capture)
                {
                    break;
                }
                //Incrementing the magnitude to look further along this direction.
                mag++;
            }
        }
        public bool TurnPlayerInCheck(PieceColour turnColour)
        {
            Square kingSquare = null;
            if (turnColour == PieceColour.White)
            {
                kingSquare = this[whiteKingPos];
            }
            else
            {
                kingSquare = this[blackKingPos];
            }

            (Vector2, Type)[] rayChecks = {(new Vector2(0,1),typeof(Rook)),
                                           (new Vector2(1,0),typeof(Rook)),
                                           (new Vector2(0,-1),typeof(Rook)),
                                           (new Vector2(-1,0),typeof(Rook)),
            
                                           (new Vector2(1,1),typeof(Bishop)),
                                           (new Vector2(-1,1),typeof(Bishop)),
                                           (new Vector2(1,-1),typeof(Bishop)),
                                           (new Vector2(-1,-1),typeof(Bishop))};


            foreach ((Vector2, Type) rayCheck in rayChecks)
            {
                if (CheckRayCast(rayCheck.Item2, rayCheck.Item1, kingSquare.pos, kingSquare.piece.colour))
                {
                    return true;
                }
            }

            for (int xAdd = -1; xAdd <= 1; xAdd += 2)
            {
                for (int yAdd = -1; yAdd <= 1; yAdd += 2)
                {
                    //Then, for each diagonal square, we move by the xAdd again.
                    Vector2 targetPos = new Vector2(kingSquare.pos.X + 2 * xAdd, kingSquare.pos.Y + yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != turnColour)
                        {
                            return true;
                        }
                    }


                    targetPos = new Vector2(kingSquare.pos.X + xAdd, kingSquare.pos.Y + 2 * yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != turnColour)
                        {
                            return true;
                        }
                    }
                }
            }
            int directionSign;
            if (kingSquare.piece.colour == PieceColour.White)
            {
                directionSign = -1;
            }
            else
            {
                directionSign = 1;
            }
            for (int i = -1; i < 2; i += 2)
            {
                Vector2 targetPos = kingSquare.pos + new Vector2(i, directionSign);
                if (IsOnBoard(targetPos) && this[targetPos].piece is Pawn && this[targetPos].piece.colour != turnColour)
                {
                    return true;
                }
            }
            

            return false;


        }
        private bool CheckRayCast(Type targetType, Vector2 dir, Vector2 pos,PieceColour colour)
        {
            int mag = 1;
            while (true)
            {
                Vector2 targetPos = pos + mag * dir;
                if (!IsOnBoard(targetPos))
                {
                    return false;
                }

                MoveType moveType = MoveTypeForMove(colour, targetPos);
                //If there is a piece blocking
                if (moveType == MoveType.Invalid)
                {
                    return false;
                }
 

                if (moveType == MoveType.Capture && (this[targetPos].piece.GetType() == targetType || this[targetPos].piece.GetType() == typeof(Queen)))
                {
                    return true;
                }
                else if (moveType == MoveType.Capture)
                {
                    return false;
                }
                mag++;
            }

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public bool isSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        private bool _isSelected;


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
        public Square(Square square)
        {
            piece = square.piece;
            this.pos = square.pos;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

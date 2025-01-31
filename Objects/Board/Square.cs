using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    
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

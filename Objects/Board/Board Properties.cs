using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public partial class Board : INotifyPropertyChanged
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
        public Move lastPlayedMove { get; private set; } = new Move(new Vector2(-1, -1), new Vector2(-1, -1), MoveType.Invalid);
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

        public bool isInCheck { get; set; }
        public PieceColour turnColour { get; set; }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

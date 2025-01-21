using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    /// <summary>
    /// The enum used for representing move types.
    /// </summary>
    public enum MoveType
    {
        Default,
        Capture,
        Check,
        EnPassant,
        Castling,
        Invalid
    }
    /// <summary>
    /// The data structure used to hold information about moves. 
    /// </summary>
    public record Move
    {
        /// <summary>
        /// The position that the piece is moving from. 
        /// </summary>
        public Vector2 startPosition { get; set; }
        /// <summary>
        /// The position that the piece is moving to.
        /// </summary>
        public Vector2 endPosition { get; set;}
        /// <summary>
        /// The type of move.
        /// </summary>
        public MoveType moveType { get; } = MoveType.Default;

        /// <summary>
        /// The default constructor. Automatically assigns a default move type to the move.
        /// </summary>
        /// <param name="startPosition"><inheritdoc cref="startPosition"></inheritdoc>/></param>
        /// <param name="endPosition"></param>

        public Move(Vector2 startPosition, Vector2 endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }
        /// <summary>
        /// A constructor which can be used to 
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="endPosition"></param>
        /// <param name="moveType"></param>
        public Move(Vector2 startPosition, Vector2 endPosition, MoveType moveType) : this(startPosition, endPosition)
        {
            this.moveType = moveType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Educational_Project
{
    public partial class Board
    {
        /// <summary>
        /// Checks whether the player of the provided colour is in check.
        /// </summary>
        /// <param name="colourToCheck"><inheritdoc cref="CheckLogic(Vector2, PieceColour)"/></param>
        /// <returns><inheritdoc cref="CheckLogic(Vector2, PieceColour)"/></returns>
        public bool PlayerInCheck(PieceColour colourToCheck)
        {
            Vector2 kingPos;
            if (colourToCheck == PieceColour.White)
            {
                kingPos = whiteKingPos;
            }
            else
            {
                kingPos = blackKingPos;
            }
            

            return CheckLogic(kingPos, colourToCheck); 
        }
        /// <summary>
        /// Checks if a player would be in check, if the king is at a specified dummy position.
        /// </summary>
        /// <param name="dummyPos">The position to be checked.</param>
        /// <param name="colourToCheck"><inheritdoc cref="CheckLogic(Vector2, PieceColour)"/></param>
        /// <returns><inheritdoc cref="CheckLogic(Vector2, PieceColour)"/></returns>
        public bool PlayerInCheck(Vector2 dummyPos, PieceColour colourToCheck)
        {
            return CheckLogic(dummyPos, colourToCheck);
        }
        /// <summary>
        /// The internal logic to determine if the king is in check.
        /// </summary>
        /// <param name="kingPos">The position of the king.</param>
        /// <param name="colourToCheck">The player colour to check.</param>
        /// <returns>True if the player is in check, false else</returns>
        internal bool CheckLogic(Vector2 kingPos, PieceColour colourToCheck)
        {
            (Vector2, Type)[] rayChecks = {(new Vector2(0,1),typeof(Rook)),
                                           (new Vector2(1,0),typeof(Rook)),
                                           (new Vector2(0,-1),typeof(Rook)),
                                           (new Vector2(-1,0),typeof(Rook)),

                                           (new Vector2(1,1),typeof(Bishop)),
                                           (new Vector2(-1,1),typeof(Bishop)),
                                           (new Vector2(1,-1),typeof(Bishop)),
                                           (new Vector2(-1,-1),typeof(Bishop))};

            //Performing each of the rayChecks.
            foreach ((Vector2, Type) rayCheck in rayChecks)
            {
                //If any of them return true, then the king is in check.
                if (CheckRayCast(rayCheck.Item2, rayCheck.Item1, kingPos, colourToCheck))
                {
                    return true;
                }
            }
            //Effectively the code for the movement of the knight.
            //Determines if there are any knights checking the king.
            for (int xAdd = -1; xAdd <= 1; xAdd += 2)
            {
                for (int yAdd = -1; yAdd <= 1; yAdd += 2)
                {
                    //Then, for each diagonal square, we move by the xAdd again.
                    Vector2 targetPos = new Vector2(kingPos.X + 2 * xAdd, kingPos.Y + yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != colourToCheck)
                        {
                            return true;
                        }
                    }


                    targetPos = new Vector2(kingPos.X + xAdd, kingPos.Y + 2 * yAdd);
                    if (IsOnBoard(targetPos))
                    {
                        if (this[targetPos].piece is Knight && this[targetPos].piece.colour != colourToCheck)
                        {
                            return true;
                        }
                    }
                }
            }

            //The section of the check logic for pawns.
            int directionSign;

            //If we are checking white's king, then we need to check for black pawns that are +-1,-1, so directionSign is -1.
            if (colourToCheck == PieceColour.White)
            {
                directionSign = -1;
            }
            else
            {
                directionSign = 1;
            }

            for (int i = -1; i < 2; i += 2)
            {
                //Checks the two possible positions where a pawn could be and check the king.
                Vector2 targetPos = kingPos + new Vector2(i, directionSign);
                if (IsOnBoard(targetPos) && this[targetPos].piece is Pawn && this[targetPos].piece.colour != colourToCheck)
                {
                    //If that square contains an enemy pawn, the king is in check.
                    return true;
                }
            }

            //Even though the king cannot check the other king directly, this code will be used for validating legal moves.
            //Therefore, it is important to check if the kings are too close in order to remove the move from the list of legal moves.
            if ((whiteKingPos - blackKingPos).LengthSquared() <= 2)
            {
                //Checks if the kings are within one square of each other (and therefore the difference between these two vectors have a magnitude of 1 or root 2).
                //If so, the move that placed the king here is not legal.
                return true;
            }

            return false;
        }
        /// <summary>
        /// A function that determines if an enemy piece on a given direction vector could capture the king.
        /// </summary>
        /// <param name="targetType">The piece to be checked for.</param>
        /// <param name="dir">The direction vector to check along.</param>
        /// <param name="pos">The position of the king.</param>
        /// <param name="colour">The colour of the king being checked.</param>
        /// <returns></returns>
        internal bool CheckRayCast(Type targetType, Vector2 dir, Vector2 pos, PieceColour colour)
        {
            //Initialising the magnitude.
            int mag = 1;
            while (true)
            {
                //Calculating our new position to check, by adding the scaled direction vector to the initial position
                Vector2 targetPos = pos + mag * dir;
                //If this vector is not on the board, we don't need to check anymore.
                if (!IsOnBoard(targetPos))
                {
                    return false;
                }

                //Getting the moveType for this "move". Allows us to easily check what its targeting.
                MoveType moveType = MoveTypeForMove(colour, targetPos);
                //If there is a piece blocking.
                if (moveType == MoveType.Invalid)
                {
                    return false;
                }
                //If an enemy piece is there, and matches our target type.
                if (moveType == MoveType.Capture && (this[targetPos].piece.GetType() == targetType || this[targetPos].piece.GetType() == typeof(Queen)))
                {
                    //The king is in check, so we return true.
                    return true;
                }
                //If an allied piece is there, its "blocking" so we don't need to check this direction anymore.
                else if (moveType == MoveType.Capture)
                {
                    return false;
                }
                mag++;
            }
        }
    }
    
}

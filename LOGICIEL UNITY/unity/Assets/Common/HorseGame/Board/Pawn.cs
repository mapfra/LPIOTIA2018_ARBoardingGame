using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Common.HorseGame.Board
{
    public class Pawn
    {
        protected Square actualSquare;

        protected Player owner;

        public virtual Square ActualSquare()
        {
            return actualSquare;
        }

        public void SetActualSquare(Square square)
        {
            //actualSquare = square;
            Pawn temp = GameContext.Occupied(square);

            if (temp != null)
            {
                if (temp is VisiblePawn)
                {
                    GameContext.Submit("VisiblePawnKilled", ((VisiblePawn)temp).horse.transform.GetSiblingIndex());
                }
                else
                {
                    GameContext.Submit("PawnKilled", temp);
                }
            }

            if (square.index == 80)
            {
                Pawn me = Owner().Pawns.FirstOrDefault(p => p == this);
                Owner().Pawns.Remove(me);
                if (Owner().Pawns.Count == 0)
                {
                    GameContext.Victory = true;
                }
            }
        }

        public void PawnKilled(Object pawn)
        {
            if (this == pawn)
            {
                if (GameContext.Occupied(GameContext.Stables[owner.boardIndex]) != null)
                {
                    SetActualSquare(GameContext.Stables[owner.boardIndex].next);
                }
                else
                {
                    SetActualSquare(GameContext.Stables[owner.boardIndex]);
                }
            }
        }

        public virtual Player Owner()
        {
            return owner;
        }

        public Pawn(Player Owner, Square ActualSquare)
        {
            actualSquare = ActualSquare;
            owner = Owner;
            GameContext.Subscribe("PawnKilled", PawnKilled);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Common.HorseGame.Board;

namespace Assets.Common.HorseGame
{
    public class Player
    {
        List<Pawn> myPawns = new List<Pawn>();
        public List<Pawn> Pawns
        {
            get { return myPawns; }
            set { Pawns = value; }
        }
        public int index { get; set; }

        public int boardIndex { get; set; }

        public PlayerMode PlayerMode { get; set; }
    }
}

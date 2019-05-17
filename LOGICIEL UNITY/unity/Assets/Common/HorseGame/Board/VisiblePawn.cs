using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Common.HorseGame.Board
{
    public class VisiblePawn : Pawn
    {
        public Horse horse;

        public override Square ActualSquare()
        {
            return horse.ActualSquare;
        }


        public override Player Owner()
        {
            return horse.Owner;
        }

        public VisiblePawn(Player Owner, Square ActualSquare) : base(null, null)
        {
            var newHorse = GameObject.Instantiate(GameContext.Horse, GameContext.BoardTransform);
            newHorse.GetComponent<Horse>().Init(ActualSquare, Owner);
            horse = newHorse.GetComponent<Horse>();
            //Owner.Pawns.Add(this);
        }
    }
}

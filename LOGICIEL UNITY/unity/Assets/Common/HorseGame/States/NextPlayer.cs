using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Common.HorseGame.States
{
    class NextPlayer : IState
    {
        public void Execute()
        {
            GameContext.Players.NextTurn();
            GameContext.ActualState = new PlayTurn();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Common.HorseGame.States
{
    class WaitForAGame : IState
    {
        public void Execute()
        {
            GameContext.Subscribe("MessageSent", GameStarted);
            GameContext.DisplayLabel.text = "Wait for the game to begin";
        }

        public void GameStarted(System.Object Msg)
        {
            GameContext.UnSubscribe("MessageSent");
            string msg = (string)Msg;

            if (msg.Equals("start=true"))
            {
                GameContext.DisplayLabel.text = "Game started";
                GameContext.ActualState = new PlayTurn();
                
            }
            else
            {
                GameContext.Subscribe("MessageSent", GameStarted);
            }
        }
    }
}

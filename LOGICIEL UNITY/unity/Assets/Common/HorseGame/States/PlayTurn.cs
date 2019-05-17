using Assets.Common.HorseGame.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Common.HorseGame.States
{
    class PlayTurn : IState
    {
        int diceRolled;
        public void Execute()
        {
            if (GameContext.Players.GetPlayer().PlayerMode != PlayerMode.ARBoard)
            {
                GameContext.DisplayLabel.text = "Waiting for Player " + GameContext.Players.GetPlayer().index;
                GameContext.Subscribe("MessageSent", TurnPlayed);
            }
            else
            {
                GameContext.DisplayLabel.text = "Your turn: roll the dice";
                GameContext.Subscribe("DiceRolled", DiceRolled);
            }
        }

        public void TurnPlayed(System.Object Msg)
        {
            GameContext.UnSubscribe("MessageSent");
            string msg = (string)Msg;
            string[] parameters = msg.Split(';');

            int.TryParse(parameters.FirstOrDefault(p => p.StartsWith("dice")).Substring(5), out diceRolled);

            if (parameters.Any(p => p.StartsWith("horse")))
            {
                int horse;
                int.TryParse(parameters.FirstOrDefault(p => p.StartsWith("horse")).Split('=')[1], out horse);

                Pawn targetHorse = GameContext.Players.GetPlayer().Pawns[horse];
                

                Square destination = GameContext.GoTo(targetHorse.ActualSquare(), diceRolled);

                Square stableSquare = GameContext.Stables[GameContext.Players.GetPlayer().boardIndex];

                if (destination != null)
                {
                    if (GameContext.isBothHorsesInStable(GameContext.Players.GetPlayer()) && diceRolled == 6)
                    {
                        GameContext.MovePawn(GameContext.Occupied(GameContext.Stables[GameContext.Players.GetPlayer().boardIndex].next), HorseMoveType.SetOnStart);
                    }
                        
                    else if ((targetHorse.ActualSquare().index == stableSquare.index || targetHorse.ActualSquare().index == stableSquare.next.index) && diceRolled == 6)
                        GameContext.MovePawn(targetHorse, HorseMoveType.SetOnStart);
                    else
                        GameContext.MovePawn(targetHorse, HorseMoveType.Move, destination);

                    GameContext.Subscribe("MoveDone", MoveDone);
                }
                
            }
            else
            {
                MoveDone();
            }


            
        }

        public void DiceRolled(System.Object Dice)
        {
            GameContext.UnSubscribe("DiceRolled");
            diceRolled = (int)Dice;

            bool isMovePossible = false;
            GameContext.Players.GetPlayer().Pawns.ForEach(p => { isMovePossible = isMovePossible || GameContext.GoTo(p.ActualSquare(), diceRolled) != null;});

            if (!isMovePossible)
            {
                GameContext.MQTT.Publish("player=2;dice=" + diceRolled, "player/general");
                if (diceRolled == 6) GameContext.ActualState = new PlayTurn();
                else GameContext.ActualState = new NextPlayer();
                return;
            }


            GameContext.DisplayLabel.text = "Your turn: move a horse";
            GameContext.Subscribe("HorseMoved", MoveHorse);
        }

        public void MoveHorse(System.Object HorseIndex)
        {
            GameContext.UnSubscribe("HorseMoved");
            GameContext.DisplayLabel.text = "You moved";
            int horseIndex = (int)HorseIndex;
            Pawn targetHorse = GameContext.Players.GetPlayer().Pawns[horseIndex];

            Square destination = GameContext.GoTo(targetHorse.ActualSquare(), diceRolled);

            Square stableSquare = GameContext.Stables[GameContext.Players.GetPlayer().boardIndex];

            if ((targetHorse.ActualSquare().index == stableSquare.index || targetHorse.ActualSquare().index == stableSquare.next.index) && diceRolled == 6 && destination != null)
            {
                GameContext.MovePawn(targetHorse, HorseMoveType.SetOnStart);
                
            }
            else if (destination != null)
            {
                GameContext.MovePawn(targetHorse, HorseMoveType.SetOnStart, destination);
            }

            GameContext.MQTT.Publish("player=2;horse=" + horseIndex + ";dice=" + diceRolled, "player/general");

            if (diceRolled == 6) GameContext.ActualState = new PlayTurn();
            else GameContext.ActualState = new NextPlayer();
        }

        public void MoveDone(System.Object parameter = null)
        {
            GameContext.UnSubscribe("MoveDone");
            if (diceRolled == 6) GameContext.ActualState = new PlayTurn();
            else GameContext.ActualState = new NextPlayer();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Common.HorseGame.States;
using Assets.Common.HorseGame.Board;
using M2MqttUnity.Examples;

namespace Assets.Common  {
    namespace HorseGame
    {
        public sealed class GameContext
        {
            public static bool Victory = false;
            public static List<Square> Squares;
            public static CList<Player> Players;
            public static List<Square> Stables;

            public static Transform BoardTransform;
            public static GameObject Horse;

            public static int Dice;

            public static float SquareSize;

            public static CoroutineLauncher CoroutineLauncher;

            public static MovePawn movePawn;

            public static Text DisplayLabel;

            public static M2MqttUnityTest MQTT;

            public static void MovePawn(Pawn pawn, HorseMoveType horseMoveType, Square destination = null)
            {
                //if (pawn.Owner().index == 3 && movePawn != null) Debug.Log("bah voila pourquoi ça bouge pas");
                movePawn.DynamicInvoke(pawn, horseMoveType, destination);
            }

            public static Pawn Occupied(Square square)
            {
                return Players.List.SelectMany(player => player.Pawns).FirstOrDefault(pawn => pawn.ActualSquare().index == square.index);
            }

            public static Square GoTo(Square startSquare, int dice)
            {
                Square retSquare = null;

                Square stableSquare = GameContext.Stables[GameContext.Players.GetPlayer().boardIndex];
                Square sprintSquare = stableSquare.next.next.back;


                if (startSquare.index < 56)
                {
                    for (int i=0; i<dice; i++)
                    {
                        if (retSquare != null && !(Occupied(retSquare) != null && dice - i > 0)) retSquare = retSquare.ChangeDirection(HorseDirection.next);
                        else if (retSquare == null) retSquare = startSquare.next;
                    }
                }
                else if (startSquare.index == sprintSquare.index && dice == 6 && Occupied(startSquare.right)!=null)
                {
                    Debug.Log("passe par le sprint");
                    retSquare = startSquare.ChangeDirection(HorseDirection.right);
                }
                else if (startSquare.index > sprintSquare.index && sprintSquare.index + 6 < startSquare.index && startSquare.index - sprintSquare.right.index + 1 == dice && Occupied(startSquare.next) != null)
                {
                    Debug.Log("passe DANS le sprint");
                    retSquare = startSquare.ChangeDirection(HorseDirection.next);
                }
                else if ((startSquare.index == stableSquare.index || startSquare.index == stableSquare.next.index) && dice == 6 && Occupied(stableSquare.next.next) == null)
                {
                    //Debug.Log();
                    retSquare = stableSquare.next.next;
                }

                return retSquare;
            }

            public static bool isBothHorsesInStable(Player player)
            {
                
                bool retValue = true;
                

                player.Pawns.ForEach(p =>
                {
                    
                    retValue = retValue && (p.ActualSquare().index == Stables[player.boardIndex].index || p.ActualSquare().index == Stables[player.boardIndex].next.index);
                });


                return retValue;
            }

            //public static bool TryNavigateTo

            private static List<KeyValuePair<string, trigger>> events = new List<KeyValuePair<string, trigger>>();

            public static void Submit(string eventName, System.Object parameters = null)
            {

                foreach (var trigger in events)
                {
                    if (trigger.Key.Equals(eventName)) trigger.Value.DynamicInvoke(parameters);
                }
                
            }

            public static void Subscribe(string eventName, trigger trigger)
            {
                var newEvents = events.Select(e => e).ToList();
                newEvents.Add(new KeyValuePair<string, trigger>(eventName, trigger));

                events = newEvents;
            }

            public static void UnSubscribe(string eventName, trigger trigger = null)
            {
                if (trigger == null) events = events.Where(pair => !pair.Key.Equals(eventName)).ToList();
                else events = events.Where(pair => !(pair.Key.Equals(eventName) && pair.Value.Equals(trigger))).ToList();
            }

            private static IState actualState;
            public static IState ActualState
            {
                get
                {
                    return actualState;
                }
                set
                {
                    actualState = value;
                    GameContext.Submit("NextStep");
                }
            }

            public static void StartCoroutine(coroutine coroutine)
            {
                CoroutineLauncher.LaunchCoroutine(coroutine);
            }
            
        }
    }
}

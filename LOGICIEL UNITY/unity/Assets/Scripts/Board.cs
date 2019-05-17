using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Common.HorseGame;
using Assets.Common.HorseGame.Board;

//C'est un script pour qu'il puisse avoir des GameObjects fils
public class Board : MonoBehaviour {

    public List<Square> Squares;


    


    public void Init()
    {
        GenerateBoard();
        GenerateCoordonates();
        CreateStables();
        GameContext.BoardTransform = transform;
        GameContext.movePawn = LaunchTravel;
    }

    #region BoardCreation
    void GenerateBoard()
    {
        
        List<Square> GeneratedBoard = new List<Square>();

        for (int i = 0; i < 90; i++) GeneratedBoard.Add(new Square());

        GeneratedBoard.ForEach(b =>
        {
            b.index = GeneratedBoard.IndexOf(b);

            //Mapping de la croix
            if (b.index < 56)
            {
                b.next = (b.index < 55) ? GeneratedBoard[b.index + 1] : GeneratedBoard[0];
                b.back = (b.index > 0) ? GeneratedBoard[b.index - 1] : GeneratedBoard[55];

                switch (b.index)
                {
                    case 55:
                        b.right = GeneratedBoard[56];
                        break;
                    case 13:
                        b.right = GeneratedBoard[62];
                        break;
                    case 27:
                        b.right = GeneratedBoard[68];  
                        break;
                    case 41:
                        b.right = GeneratedBoard[74]; 
                        break;
                }
            }
            //Mapping des sprints
            else if (b.index < 81)
            {
                
                if (b.index < 62)
                {
                    b.next = (b.index < 61) ? GeneratedBoard[b.index + 1] : GeneratedBoard[80];
                    b.back = (b.index > 56) ? GeneratedBoard[b.index - 1] : GeneratedBoard[55];
                }
                else if (b.index < 68)
                {
                    b.next = (b.index < 67) ? GeneratedBoard[b.index + 1] : GeneratedBoard[80];
                    b.back = (b.index > 62) ? GeneratedBoard[b.index - 1] : GeneratedBoard[13];
                }
                else if (b.index < 74)
                {
                    b.next = (b.index < 73) ? GeneratedBoard[b.index + 1] : GeneratedBoard[80];
                    b.back = (b.index > 68) ? GeneratedBoard[b.index - 1] : GeneratedBoard[27];
                }
                else if (b.index < 80)
                {
                    b.next = (b.index < 79) ? GeneratedBoard[b.index + 1] : GeneratedBoard[80];
                    b.back = (b.index > 74) ? GeneratedBoard[b.index - 1] : GeneratedBoard[41];
                }
            }
            //Mapping des écuries
            else
            {
                switch (b.index)
                {
                    case 81:
                        b.next = GeneratedBoard[82];
                        break;
                    case 82:
                        b.next = GeneratedBoard[0];
                        b.back = GeneratedBoard[81];
                        break;
                    case 83:
                        b.next = GeneratedBoard[84];
                        break;
                    case 84:
                        b.next = GeneratedBoard[14];
                        b.back = GeneratedBoard[83];
                        break;
                    case 85:
                        b.next = GeneratedBoard[86];
                        break;
                    case 86:
                        b.next = GeneratedBoard[28];
                        b.back = GeneratedBoard[85];
                        break;
                    case 87:
                        b.next = GeneratedBoard[88];
                        break;
                    case 88:
                        b.next = GeneratedBoard[42];
                        b.back = GeneratedBoard[87];
                        break;
                }
            }
        });

        Squares = GeneratedBoard;

    }

    void GenerateCoordonates()
    {
        int nb = 0;

        Squares.ForEach(square => {
            nb++;

            //Le parcours en croix du plateau
            if (square.index < 7)
            {
                square.position = new Vector3(7, 0, square.index + 1);
            }
            else if (square.index < 13)
            {
                square.position = new Vector3(13 - square.index, 0, 7);
            }
            else if (square.index < 15)
            {
                square.position = new Vector3(1, 0, square.index - 5);
            }
            else if (square.index < 21)
            {
                square.position = new Vector3(square.index - 13, 0, 9);
            }
            else if (square.index < 27)
            {
                square.position = new Vector3(7, 0, square.index - 11);
            }
            else if (square.index < 29)
            {
                square.position = new Vector3(square.index - 19, 0, 15);
            }
            else if (square.index < 35)
            {
                square.position = new Vector3(9, 0, 43 - square.index);
            }
            else if (square.index < 41)
            {
                square.position = new Vector3(square.index - 25, 0, 9);
            }
            else if (square.index < 43)
            {
                square.position = new Vector3(15, 0, 49 - square.index);
            }
            else if (square.index < 49)
            {
                square.position = new Vector3(57 - square.index, 0, 7);
            }
            else if (square.index < 55)
            {
                square.position = new Vector3(9, 0, 55 - square.index);
            }
            else if (square.index == 55)
            {
                square.position = new Vector3(8, 0, 1);
            }
            //Les chemins de sprint
            else if (square.index < 62)
            {
                square.position = new Vector3(8, 0, square.index - 54);
            }
            else if (square.index < 68)
            {
                square.position = new Vector3(square.index - 60, 0, 8);
            }
            else if (square.index < 74)
            {
                square.position = new Vector3(8, 0, 82 - square.index);
            }
            else if (square.index < 80)
            {
                square.position = new Vector3(88 - square.index, 0, 8);
            }
            //La case d'arrivée
            else if (square.index == 80)
            {
                square.position = new Vector3(8, 0, 8);
            }
            //Les écuries
            switch (square.index)
            {
                case 81:
                    square.position = new Vector3(5, 0, 1);
                    break;
                case 82:
                    square.position = new Vector3(6, 0, 1);
                    break;
                case 83:
                    square.position = new Vector3(1, 0, 11);
                    break;
                case 84:
                    square.position = new Vector3(1, 0, 10);
                    break;
                case 85:
                    square.position = new Vector3(11, 0, 15);
                    break;
                case 86:
                    square.position = new Vector3(10, 0, 15);
                    break;
                case 87:
                    square.position = new Vector3(15, 0, 5);
                    break;
                case 88:
                    square.position = new Vector3(15, 0, 6);
                    break;
            }

            square.position *= GameContext.SquareSize;
        });
    }

    void CreateStables()
    {
        var Stables = new List<Square>();
        Stables.Add(Squares[81]);
        Stables.Add(Squares[83]);
        Stables.Add(Squares[85]);
        Stables.Add(Squares[87]);

        GameContext.Stables = Stables;
    }

    #endregion

    public void CreateHorses(IEnumerable<Player> players)
    {
        foreach (var player in players)
        {
            if (player.PlayerMode != PlayerMode.ARBoard)
            {
                var firstPoney = new VisiblePawn(player, GameContext.Stables[player.boardIndex]);
                var secondPoney = new VisiblePawn(player, GameContext.Stables[player.boardIndex].next);

                

                player.Pawns.Add(firstPoney);
                player.Pawns.Add(secondPoney);

                
            }
            else
            {

                player.Pawns.Add(new Pawn(player, GameContext.Stables[player.boardIndex]));
                player.Pawns.Add(new Pawn(player, GameContext.Stables[player.boardIndex].next));
                //player.Pawns.Add(secondPawn);
            }

            //if (player.index == 3) Debug.Log(player.Pawns.Count);
        }

    }
    

    public void LaunchTravel(Pawn pawn, HorseMoveType move, Square destination = null)
    {
        if (pawn is VisiblePawn)
        {
            var parsedHorse = ((VisiblePawn)pawn).horse;
            if (parsedHorse == null) Debug.Log("Pas de parsedHorse");
            switch (move)
            {
                case HorseMoveType.SetOnStart:

                    if (parsedHorse.ActualSquare.index % 2 == 1)
                    {
                        AddMove(pawn, HorseDirection.next);
                    }
                    AddMove(pawn, HorseDirection.next);
                    break;

                case HorseMoveType.Move:

                    if (parsedHorse.ActualDestination.index < 56 && destination != null && destination.index < 56)
                    {
                        int nbMove = countMovesAroundCross(pawn, destination);
                        
                        for (int i = 0; i < nbMove; i++)
                        {
                            AddMove(pawn, HorseDirection.next);
                        }
                    }
                    else if (parsedHorse.ActualDestination.index < 56 && destination != null && destination.index >= 56 && destination.index < 80)
                    {
                        Square whereToTurn = GameContext.Stables[pawn.Owner().boardIndex].next.next.back;
                        
                        int nbMove = countMovesAroundCross(pawn, whereToTurn);
                        if (destination.index - whereToTurn.right.index < 6)
                        {
                            for (int i= 0; i < nbMove; i++)
                            {
                                AddMove(pawn, HorseDirection.next);
                            }
                            AddMove(pawn, HorseDirection.right);

                            while(parsedHorse.ActualDestination.index < destination.index)
                            {
                                AddMove(pawn, HorseDirection.next);
                            }
                        }
                    }


                    break;

                
            }
        }
        else
        {
            pawn.SetActualSquare(destination);
        }
    }

    public void AddMove(Pawn pawn, HorseDirection direction)
    {
        if (pawn is VisiblePawn)
        {
            var parsedHorse = ((VisiblePawn)pawn).horse;
            var tasks = (parsedHorse.WorkQueue != null) ? parsedHorse.WorkQueue.Select(p => p).ToList() : new List<HorseDirection>();
            tasks.Add(direction);

            parsedHorse.WorkQueue = tasks;
        }
        else
        {
            pawn.SetActualSquare(pawn.ActualSquare().ChangeDirection(direction));
        }
        
    }

    public int countMovesAroundCross(Pawn horse, Square destination)
    {
        if (horse is VisiblePawn)
        {
            var parsedHorse = ((VisiblePawn)horse).horse;
            int actualDestinationIndex = parsedHorse.ActualDestination.index;

            
            int destinationIndex = destination.index;
            if (destination.index < actualDestinationIndex)
                destinationIndex = destination.index + 56;

            return destinationIndex - actualDestinationIndex;

                
            
        }
        return 0;
    }


    #region MapExplorationTools


    void FillStables(IEnumerable<Player> players)
    {

    }

    #endregion
}


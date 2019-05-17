using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Common.HorseGame;
using Assets.Common.HorseGame.Board;
using Assets.Common.HorseGame.States;
using Assets.Common;
using M2MqttUnity.Examples;
public class Game : MonoBehaviour {

    Board Board;
    public CList<Player> Players;
    public List<Pawn> Pawns;

    public GameObject Horse;
    

    public Vector3 gravity = new Vector3(0, -950, 0);
    public float SquareSize = 150;

    // Use this for initialization
    void Start () {
        Init();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameContext.Submit("DiceRolled", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameContext.Submit("DiceRolled", 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameContext.Submit("DiceRolled", 3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameContext.Submit("DiceRolled", 4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GameContext.Submit("DiceRolled", 5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            GameContext.Submit("DiceRolled", 6);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            GameContext.Submit("HorseMoved", 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameContext.Submit("HorseMoved", 1);
        }
    }

    void Init()
    {
        Physics.gravity = gravity;
        GameContext.Horse = Horse;
        GameContext.SquareSize = SquareSize;
        GameContext.CoroutineLauncher = GetComponent<CoroutineLauncher>();
        GameContext.DisplayLabel = GameObject.Find("DisplayLabel").GetComponent<Text>();
        GameContext.MQTT = GetComponent<M2MqttUnityTest>();

        //Init des joueurs
        CreatePlayers();

        //Init du plateau
        Board = GetComponentInChildren<Board>();
        Board.Init();

        //Init des poneys
        Board.CreateHorses(Players.List);

        //Active l'automate
        GameContext.Subscribe("NextStep", CheckNextStep);

        GameContext.ActualState = new ConnectToGame();
        
    }

    public void CheckNextStep(System.Object parameters = null)
    {
        if (!GameContext.Victory)
        {
            GameContext.ActualState.Execute();
        }
        else
        {
            //Script de victoire
        }
    }
    
    public void CreatePlayers()
    {
        //Données en dur pour le moment

        Players = new CList<Player>();
        Players.List.Add(new Player
        {
            index = 1,
            boardIndex = 3,
            PlayerMode = PlayerMode.WebAccess
        });
        Players.List.Add(new Player
        {
            index = 2,
            boardIndex = 0,
            PlayerMode = PlayerMode.ARBoard
        });
        Players.List.Add(new Player
        {
            index = 3,
            boardIndex = 1,
            PlayerMode = PlayerMode.WebAccess
        });
        Players.List.Add(new Player
        {
            index = 4,
            boardIndex = 2,
            PlayerMode = PlayerMode.WebAccess
        });
        

        GameContext.Players = Players;
    }

}


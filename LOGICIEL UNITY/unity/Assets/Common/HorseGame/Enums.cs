using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Common.HorseGame.Board;

namespace Assets.Common.HorseGame
{
    public delegate IEnumerator coroutine(System.Object parameters = null);

    public delegate void trigger(System.Object parameters = null);

    public delegate void MovePawn(Pawn pawn, HorseMoveType horseMoveType, Square destination = null);

    public enum PlayerMode
    {
        ARBoard, WebAccess
    }

    public enum HorseMoveType
    {
        SetOnStart, Move
    }

    public enum HorseDirection
    {
        next, back, right
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Common.HorseGame;

public class Square {

    public int index { get; set; }
    public Square back { get; set; }
    public Square next { get; set; }
    public Square right { get; set; }

    public Vector3 position { get; set; }

    public Square ChangeDirection(HorseDirection direction)
    {
        switch (direction)
        {
            case HorseDirection.next:
                return next;
            case HorseDirection.right:
                return right;
            case HorseDirection.back:
                return back;
            default:
                return null;
        }
    }

}



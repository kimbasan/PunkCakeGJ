using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

[Serializable]
public class CloneState
{
    public Queue<Movements> moves;

    public CloneState(Queue<Movements> moves)
    {
        this.moves = moves;
    }
}

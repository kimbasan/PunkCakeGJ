using System;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;

[Serializable]
public class CloneState
{
    public Queue<Movements> moves;
    public SerializableVector3 startingPosition;

    public CloneState(Queue<Movements> moves, Vector3 startingPosition)
    {
        this.moves = moves;
        this.startingPosition = new SerializableVector3(startingPosition);
    }
}
